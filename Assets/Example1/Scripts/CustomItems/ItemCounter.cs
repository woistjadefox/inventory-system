using UnityEngine;
using Zhdk.Gamelab.InventorySystem;

[CreateAssetMenu(fileName = "NewItemCounter", menuName = "Inventory System/Item Counter", order = 99)]
public class ItemCounter : InventoryObject
{
    [SerializeField] private int counter = 0;

    public override void OnAddToInventory(Inventory inventory)
    {
        counter++;
    }
}
