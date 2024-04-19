using UnityEngine;
using Zhdk.Gamelab.InventorySystem;

[CreateAssetMenu(fileName = "NewItemCounter", menuName = "Inventory System/Item Mana", order = 99)]
public class ItemMana : InventoryObject
{
    [SerializeField] private float mana = 0;

    public float Mana => mana;

    public void IncreaseMana(float value)
    {
        mana += value;
    }

    public void DecreaseMana(float value)
    {
        mana -= value;
    }

    public override string GetTitle()
    {
        return title + " (Value: " + Mathf.RoundToInt(mana) +")";
    }
}
