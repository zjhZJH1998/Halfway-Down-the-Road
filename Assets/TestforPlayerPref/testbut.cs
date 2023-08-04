using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testbut : MonoBehaviour
{
    // Start is called before the first frame update
    public UI_Inventory uiInventory;
    public void additem()
    {
        uiInventory.inventory.AddItem(new Item { itemType = Item.ItemType.Stone, amount = 1 });
    }
}
