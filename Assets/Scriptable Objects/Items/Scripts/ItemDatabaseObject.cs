using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory System/Items/Database")]
public class ItemDatabaseObject : ScriptableObject
{
    public ItemObject[] Items;

    public void OnValidate()
    {
        for (int i = 0; i < Items.Length; i++)
        {
            Items[i].data.Id = i;
        }
    }
}
