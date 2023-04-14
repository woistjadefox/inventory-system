using UnityEngine;
using UnityEngine.Events;
using Zhdk.Gamelab.InventorySystem;

public class TriggerManaCheck : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] Inventory targetInventory;
    [SerializeField] float targetMana = 80;

    [Header("Events")]
    [SerializeField] UnityEvent onManaCheckOk;
    [SerializeField] UnityEvent onManaCheckFail;


    public void Check()
    {
        var specialObject = targetInventory.GetSpecialInventoryObject();

        if (specialObject is ItemMana itemMana)
        {

            if(itemMana.Mana >= targetMana)
            {
                onManaCheckOk.Invoke();
            }
            else
            {
                onManaCheckFail.Invoke();
            }
            
        }
    }
}
