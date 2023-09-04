using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public abstract class UserInterface : MonoBehaviour
{

    public InventoryObject inventory;


    public Dictionary<GameObject, InventorySlot> slotsOnInterface = new Dictionary<GameObject, InventorySlot>();
    // Start is called before the first frame update

    public GameObject TooltipPrefab;

    void Start()
    {
        for (int i = 0; i < inventory.Container.Items.Length; i++)
        {
            inventory.Container.Items[i].parent = this;
        }

        CreateSlots();
        AddEvent(gameObject, EventTriggerType.PointerEnter, delegate { OnEnterInterface(gameObject); });
        AddEvent(gameObject, EventTriggerType.PointerExit, delegate { OnExitInterface(gameObject); });
    }



    // Update is called once per frame
    void Update()
    {
        UpdateSlots();
        //click and drop
        //attaches to mouse code
        if(MouseData.isItemAttachedToMouse&&MouseData.tempItemBeingDragged != null){
            MouseData.tempItemBeingDragged.GetComponent<RectTransform>().position = Input.mousePosition;
        }
    }





    public abstract void CreateSlots();

    public void UpdateSlots(){
        foreach(KeyValuePair<GameObject, InventorySlot> _slot in slotsOnInterface){
                if(_slot.Value.item.Id >= 0){
                    _slot.Key.transform.GetChild(1).GetComponentInChildren<Image>().sprite = _slot.Value.ItemObject.uiDisplay;
                    _slot.Key.transform.GetChild(1).GetComponentInChildren<Image>().color = new Color(1,1,1,1);
                    _slot.Key.transform.GetChild(2).GetComponentInChildren<TextMeshProUGUI>().text = _slot.Value.amount == 1 ? "" : _slot.Value.amount.ToString("n0");
                }
                else{
                    _slot.Key.transform.GetChild(1).GetComponentInChildren<Image>().sprite = null;
                    _slot.Key.transform.GetChild(1).GetComponentInChildren<Image>().color = new Color(1,1,1,0);
                    _slot.Key.transform.GetChild(2).GetComponentInChildren<TextMeshProUGUI>().text = "";
                }

        }
    }

    protected void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action){
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }
    
    public void OnEnter(GameObject obj){
        MouseData.SlotHoveredOver = obj;

    }

    public IEnumerator showTooltip(GameObject obj){
        yield return new WaitForSecondsRealtime(0.5f);
        var tooltip = Instantiate(TooltipPrefab, Vector3.zero, Quaternion.identity);
        
    } 

    
    public void OnExit(GameObject obj){
        MouseData.SlotHoveredOver = null;

    }
    public void OnEnterInterface(GameObject obj)
    {
        MouseData.interfaceMouseIsOver = obj.GetComponent<UserInterface>();
    }
    public void OnExitInterface(GameObject obj)
    {
        MouseData.interfaceMouseIsOver = null;
    }
    public void OnDragStart(GameObject obj){
        
        MouseData.tempItemBeingDragged = CreateTempItem(obj);


    }

    public GameObject CreateTempItem(GameObject obj){
        GameObject tempItem = null;
        if(slotsOnInterface[obj].item.Id>=0){
            tempItem = new GameObject();
            var rt = tempItem.AddComponent<RectTransform>();
            rt.sizeDelta = new Vector2(50,50);
            tempItem.transform.SetParent(transform.parent);
            var img = tempItem.AddComponent<Image>();
            img.sprite = slotsOnInterface[obj].ItemObject.uiDisplay;
            img.raycastTarget = false;
        }
        return tempItem;

    }
    public void OnDragEnd(GameObject obj){


        Destroy(MouseData.tempItemBeingDragged);

        if(MouseData.SlotHoveredOver){
            InventorySlot mouseHoverSlotData = MouseData.interfaceMouseIsOver.slotsOnInterface[MouseData.SlotHoveredOver];
            inventory.SwapItems(slotsOnInterface[obj], mouseHoverSlotData);
        }



    }
    public void OnDrag(GameObject obj){
        if(MouseData.tempItemBeingDragged != null){
            MouseData.tempItemBeingDragged.GetComponent<RectTransform>().position = Input.mousePosition;
        }
    }



    


    public void OnSlotClicked(GameObject obj)
    {
        InventorySlot clickedSlotData = slotsOnInterface[obj];

        if (!MouseData.isItemAttachedToMouse)
        {
            if (clickedSlotData.item != null)
            {
                MouseData.isItemAttachedToMouse = true;
                MouseData.clickedSlot = clickedSlotData;
                

                // Create a temporary item being dragged
                MouseData.tempItemBeingDragged = CreateTempItem(obj);
            }
        }
        else if (MouseData.SlotHoveredOver)
        {

            // Clicked on the same slot to drop the item
            InventorySlot mouseHoverSlotData = MouseData.interfaceMouseIsOver.slotsOnInterface[MouseData.SlotHoveredOver];
            if(inventory.SwapItems(MouseData.clickedSlot, mouseHoverSlotData)){
                            Debug.Log("here");
                MouseData.isItemAttachedToMouse = false;
                MouseData.clickedSlot = null;

                // Destroy the temporary item being dragged
                if (MouseData.tempItemBeingDragged != null)
                {
                    Destroy(MouseData.tempItemBeingDragged);
                    MouseData.tempItemBeingDragged = null;
                }
            }


        }

    }



}

public static class MouseData{
    public static UserInterface interfaceMouseIsOver;
    public static GameObject tempItemBeingDragged;
    public static GameObject SlotHoveredOver;
    public static bool isItemAttachedToMouse = false;
    public static InventorySlot clickedSlot;

}


