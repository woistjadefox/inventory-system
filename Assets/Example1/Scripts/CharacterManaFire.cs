using UnityEngine;
using UnityEngine.Events;
using Zhdk.Gamelab.InventorySystem;

public class CharacterManaFire : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] Inventory targetInventory;
    [SerializeField] InventoryUI targetInventoryUI;
    [SerializeField] float fireCost = 5;

    [Header("Events")]
    [SerializeField] UnityEvent onFireMana;

    private void Update()
    {
        if(targetInventory.GetSpecialInventoryObject() is ItemMana itemMana)
        {
            if(Input.GetMouseButtonDown(0))
            {
                FireMana(itemMana);
            }
        }
    }

    private void FireMana(ItemMana itemMana)
    {
        if(itemMana.Mana > fireCost)
        {
            itemMana.DecreaseMana(fireCost);
            onFireMana.Invoke();
            targetInventoryUI.UpdateCurrentlySelectedUIObjectInfo();
        }
    }
}
