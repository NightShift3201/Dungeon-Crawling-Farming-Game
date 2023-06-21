using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayShop : MonoBehaviour
{
    public int Y_LENGTH;
    public GameObject ItemDisplayPrefab;

    public void CreateSlots(List<ItemsForSale> inventory){
        for (int i = 0; i < inventory.Count; i++)
        {
            //if(inventory[i].neededItems.Count==0){
                var obj = Instantiate(ItemDisplayPrefab, Vector3.zero, Quaternion.identity,transform);
                obj.GetComponent<RectTransform>().anchoredPosition = GetPosition(i);
                obj.GetComponent<ItemDisplay>().item = inventory[i];
                obj.transform.GetChild(0).GetComponent<Image>().sprite = inventory[i].product.uiDisplay;
                obj.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = inventory[i].product.name;
                obj.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = inventory[i].price.ToString();
            //}

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
