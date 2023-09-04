using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayShop : MonoBehaviour
{
    public int Y_LENGTH;
    public GameObject ItemDisplayPrefab;
    public GameObject CraftOnePrefab;
    public GameObject CraftTwoPrefab;
    public GameObject CraftThreePrefab;

    public void CreateSlots(List<ItemsForSale> inventory, int shopLevel){
        for (int i = 0; i < inventory.Count; i++)
        {
            if(inventory[i].requiredLevel<=shopLevel){
                GameObject obj = null;
                if(inventory[i].neededItems.Count==0){
                    obj = Instantiate(ItemDisplayPrefab, Vector3.zero, Quaternion.identity,transform);
                }
                if(inventory[i].neededItems.Count==1){
                    obj = Instantiate(CraftOnePrefab, Vector3.zero, Quaternion.identity,transform);
                }
                if(inventory[i].neededItems.Count==2){
                    obj = Instantiate(CraftTwoPrefab, Vector3.zero, Quaternion.identity,transform);
                }
                if(inventory[i].neededItems.Count==3){
                    obj = Instantiate(CraftThreePrefab, Vector3.zero, Quaternion.identity,transform);
                }
                obj.GetComponent<RectTransform>().anchoredPosition = GetPosition(i);
                ItemsForSale item = inventory[i];
                obj.GetComponent<ItemDisplay>().item = item;
                obj.transform.GetChild(0).GetComponent<Image>().sprite = item.product.uiDisplay;
                obj.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.product.name;
                obj.transform.GetChild(2).GetComponentInChildren<TextMeshProUGUI>().text = item.price.ToString();
                if(inventory[i].neededItems.Count>=1){
                    obj.transform.GetChild(3).GetComponent<Image>().sprite = item.neededItems[0].item.uiDisplay;
                    obj.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "x" + item.neededItems[0].amount;
                }
                if(inventory[i].neededItems.Count>=2){
                    obj.transform.GetChild(5).GetComponent<Image>().sprite = item.neededItems[1].item.uiDisplay;
                    obj.transform.GetChild(6).GetComponent<TextMeshProUGUI>().text = "x" + item.neededItems[1].amount;
                }
                if(inventory[i].neededItems.Count>=3){
                    obj.transform.GetChild(7).GetComponent<Image>().sprite = item.neededItems[2].item.uiDisplay;
                    obj.transform.GetChild(8).GetComponent<TextMeshProUGUI>().text = "x" + item.neededItems[2].amount;
                }
            }
            

        }
    }

    public void ClearSlots(){
         foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }
    }
    public Vector3 GetPosition(int i){
        return new Vector3(0f, ((-Y_LENGTH/2f) - i*Y_LENGTH),0f);
    }
}
