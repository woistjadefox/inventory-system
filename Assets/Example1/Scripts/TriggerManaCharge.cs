using UnityEngine;
using UnityEngine.Events;
using Zhdk.Gamelab.InventorySystem;

public class TriggerManaCharge : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] Inventory targetInventory;
    [SerializeField] InventoryUI targetInventoryUI;
    [SerializeField] float increaseSpeed = 2;

    [Header("Events")]
    [SerializeField] UnityEvent onStartCharging;
    [SerializeField] UnityEvent onStopCharging;

    private ItemMana targetManaObject;


    public void ChargeManaEnter()
    {
        var specialObject = targetInventory.GetSpecialInventoryObject();

        if (specialObject != null && specialObject is ItemMana)
        {
            targetManaObject = specialObject as ItemMana;
            onStartCharging.Invoke();
        }
    }

    public void ChargeManaExit()
    {
        targetManaObject = null;
        onStopCharging.Invoke();
    }

    private void Update()
    {
        if(targetManaObject != null)
        {
            targetManaObject.IncreaseMana(increaseSpeed * Time.deltaTime);
            targetInventoryUI.UpdateCurrentlySelectedUIObjectInfo();
        }
    }
}
