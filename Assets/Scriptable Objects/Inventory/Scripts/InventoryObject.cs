using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;
using System.Runtime.Serialization;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public ItemDatabaseObject database;

    public string savePath;
    public Inventory Container;
    public bool AddItem(Item _item, int _amount){



        InventorySlot slot = FindItemOnInventory(_item);
        //overflow check
        if(slot!=null && slot.amount+_amount>99 && _amount>1){
            slot.AddAmount(99-slot.amount);
            SetEmptySlot(_item,_amount+slot.amount-99);
            return true;
        }
        //looks for existing slot
        if(slot != null && database.Items[_item.Id].stackable && slot.amount!=99){
            slot.AddAmount(_amount);
            return true;
        }
        //finds any slot
        if(EmptySlotCount>0){
            SetEmptySlot(_item, _amount);
            return true;
        }
        return false;

    }

    public int EmptySlotCount{
        get{
            int counter = 0;
            for (int i = 0; i < Container.Items.Length; i++)
            {
                if(Container.Items[i].item.Id <= -1)
                    counter++;
            }
            return counter;
        }
    }

    public InventorySlot FindItemOnInventory(Item _item){
        for(int i =0;i<Container.Items.Length;i++){
            if(Container.Items[i].item.Id == _item.Id){
                return Container.Items[i];
            }
        }
        return null;
    }


    public InventorySlot SetEmptySlot(Item _item, int _amount){
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if(Container.Items[i].item.Id <= -1){
                Container.Items[i].UpdateSlot(_item, _amount);
                return Container.Items[i];
            }
        }
        return null;
    }


    public bool SwapItems(InventorySlot item1, InventorySlot item2){
        if(item2.CanPlaceInSlot(item1.ItemObject)&&item1.CanPlaceInSlot(item2.ItemObject)){
            InventorySlot temp = new InventorySlot(item2.item, item2.amount);
            item2.UpdateSlot(item1.item,item1.amount);
            item1.UpdateSlot(temp.item,temp.amount);
            return true;
        }
        return false;

    }
    [ContextMenu("Save")]
    public void Save(){
        //string saveData = JsonUtility.ToJson(this, true);
        //BinaryFormatter bf = new BinaryFormatter();
        //FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
        //bf.Serialize(file, saveData);
        //file.Close();

        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create,FileAccess.Write);
        formatter.Serialize(stream,Container);
        stream.Close();
    }

    [ContextMenu("Load")]
    public void Load(){
        if(File.Exists(string.Concat(Application.persistentDataPath, savePath))){
            //BinaryFormatter bf = new BinaryFormatter();
            //FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath),FileMode.Open);
            //JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
            //file.Close();

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open,FileAccess.Read);
            Inventory newContainer = (Inventory)formatter.Deserialize(stream);
            for (int i = 0; i < Container.Items.Length; i++)
            {
                Container.Items[i].UpdateSlot(newContainer.Items[i].item, newContainer.Items[i].amount);
            }
            stream.Close();
        }
    }

    [ContextMenu("Clear")]
    public void Clear(){
        Container.Clear();
    }
    

}

[System.Serializable]
public class Inventory{
    public InventorySlot[] Items = new InventorySlot[32];
    public void Clear()
    {
        for (int i = 0; i < Items.Length; i++)
        {
            Items[i].item = new Item();
            Items[i].amount = 0;
        }
    }
}

[System.Serializable]
public class InventorySlot{
    public EquipmentType[] AllowedItems = new EquipmentType[0];
    [System.NonSerialized]
    public UserInterface parent;

    public Item item;
    public int amount;

    public ItemObject ItemObject{
        get
        {
            if(item.Id>=0){
                return parent.inventory.database.Items[item.Id];
            }
            return null;
        }
    }
    public InventorySlot(){
        item = new Item();
        amount= 0;

    }
    public InventorySlot(Item _item, int _amount){
        item = _item;
        amount= _amount;
    }
    public void UpdateSlot(Item _item, int _amount){

        item = _item;
        amount= _amount;
    }
    public void RemoveItem()
    {
        item = new Item();
        amount = 0;
    }

    public void AddAmount(int value){
        amount+=value;
    }

    public bool CanPlaceInSlot(ItemObject _itemObject){
        if(AllowedItems.Length <=0 || _itemObject == null || _itemObject.data.Id<0)
            return true;
        for (int i = 0; i < AllowedItems.Length; i++)
        {
            if(_itemObject.type == ItemType.Equipment){
                EquipmentObject equipmentObject = _itemObject as EquipmentObject;
                if(equipmentObject.equipment == AllowedItems[i]){
                    return true;
                }
            }
        }
        return false;
    }
}