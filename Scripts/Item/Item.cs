using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject
{
    public string itemName; //아이템이름
    [TextArea]
    public string itemDesc; // 아이템설명
    public ItemType itemType; // 아이템 종류
    public Sprite itemImage; //아이템이미지
    public GameObject itemPrefab;

    public string weaponType; // 무기 종류

    public enum ItemType
    {
        Equipment,
        Used,
        Ingredient,
        ETC
    }

}
