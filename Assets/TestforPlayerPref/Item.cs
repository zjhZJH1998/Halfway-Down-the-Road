using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    // Start is called before the first frame update
    public enum ItemType
    {
        Weapon,
        Potion,
        Stone
    }
    public ItemType itemType;
    public int amount;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Weapon:       return ItemAssets.Instance.Sword;
            case ItemType.Potion:       return ItemAssets.Instance.Potion;
            case ItemType.Stone:        return ItemAssets.Instance.Stone;
        }
    }
    public bool IsStackable()
    {
        switch (itemType)
        {
            default :
            case ItemType.Stone:
            case ItemType.Potion:
                return true;
            case ItemType.Weapon:
                return false;
        }
    }
}

