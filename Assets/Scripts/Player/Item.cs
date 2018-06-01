using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[System.Serializable]
public class Item {
    public string itemName;
    public int itemID;
    public string itemDescription;
    public Sprite itemIcon;
    public int itemReward;
    public int itemsNumero = 0;
    public int vit;
    public ItemType itemType;
    public ItemType itemType2;

    public enum ItemType{
        Consumable,
        ArrowMaterial,
        Herbs,
        Bones,
        Food,
        Leather,
        Special,
		Nothing
    }

    public Item(string name, int id, string description, int reward,int vit,
        ItemType type1, ItemType type2, int itemsNumero) {

        itemName = name;
        itemID = id;
        itemDescription = description;
        itemReward = reward;
        itemIcon = Resources.Load<Sprite>("itemsIcons/" + name);
        this.vit = vit;
        itemType = type1;
        itemType2 = type2;
        this.itemsNumero = itemsNumero;
        
    }

    public void addItem1() {
        itemsNumero++;
    }

    public void substractItem1() {
        itemsNumero--;
    }

    public Item() { }

}
