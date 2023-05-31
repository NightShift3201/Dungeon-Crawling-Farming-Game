using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class DisplayHotbar : MonoBehaviour
{
    public InventoryObject inventory;
    public GameObject inventoryPrefab;

    public int X_START;
    public int Y_START;
    public int X_SPACE_BETWEEN_ITEMS;
    public int INVENTORY_SLOTS;

    public InventorySlot currentItem;

    private int previousIndex;

    Dictionary<GameObject, InventorySlot> itemsDisplayed = new Dictionary<GameObject, InventorySlot>();
    // Start is called before the first frame update
    void Start()
    {
        CreateSlots();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSlots();
        for (int i = 0; i < INVENTORY_SLOTS; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                SelectItem(i);
                break;
            }
        }

    }


    public void CreateSlots(){
        itemsDisplayed = new Dictionary<GameObject, InventorySlot>();
        for (int i = 0; i < INVENTORY_SLOTS; i++)
        {
            var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity,transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);

            itemsDisplayed.Add(obj,inventory.Container.Items[i]);
        }
    }

    public void UpdateSlots(){
        foreach(KeyValuePair<GameObject, InventorySlot> _slot in itemsDisplayed){
            if(_slot.Value.ID >= 0){
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.GetItem[_slot.Value.item.Id].uiDisplay;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1,1,1,1);
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = _slot.Value.amount == 1 ? "" : _slot.Value.amount.ToString("n0");
            }
            else{
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1,1,1,0);
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        }
    }

    public void SelectItem(int slotIndex)
    {

        GameObject previousItem = itemsDisplayed.ElementAt(previousIndex).Key;
        if (previousItem != null)
        {
            Image previousItemImage = previousItem.GetComponent<Image>();
            previousItemImage.color = new Color(1,1,1,0);;
        }

        if (slotIndex >= 0 && slotIndex < INVENTORY_SLOTS)
        {
            previousIndex = slotIndex;
            currentItem = inventory.Container.Items[slotIndex];
            itemsDisplayed.ElementAt(slotIndex).Key.GetComponent<Image>().color = new Color(1,1,1,1);
        }
    }

    

    public Vector3 GetPosition(int i){
        return new Vector3(X_START+(X_SPACE_BETWEEN_ITEMS*i), Y_START,0f);
    }
}

