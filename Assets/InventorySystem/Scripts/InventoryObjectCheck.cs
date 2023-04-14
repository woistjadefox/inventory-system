using UnityEngine;
using UnityEngine.Events;

namespace Zhdk.Gamelab.InventorySystem
{
    public class InventoryObjectCheck : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private InventoryObject[] inventoryObjects;
        [SerializeField] private Inventory inventory;

        [Header("Settings")]
        [SerializeField] private bool mustHaveAllObjects;
        [SerializeField] private bool oneIsInSpecialSlot;


        [Header("Events")]
        [SerializeField] private UnityEvent onIsInventoryEvent;
        [SerializeField] private UnityEvent onIsNotInventoryEvent;

        public void Check()
        {
            int existsCounter = 0;
            bool specialObject = false;

            foreach (InventoryObject inventoryObject in inventoryObjects)
            {
                if ((inventory.IsInInventory(inventoryObject) == inventoryObject))
                {
                    existsCounter++;

                    if (oneIsInSpecialSlot && inventory.GetSpecialInventoryObject() == inventoryObject)
                    {
                        specialObject = true;
                    }
                }
            }

            if(mustHaveAllObjects && existsCounter == inventoryObjects.Length || mustHaveAllObjects == false && existsCounter > 0)
            {
                if(oneIsInSpecialSlot && specialObject || oneIsInSpecialSlot == false)
                {
                    onIsInventoryEvent.Invoke();
                }
                else
                {
                    onIsNotInventoryEvent.Invoke();
                }

            } else
            {
                onIsNotInventoryEvent.Invoke();
            }
            
        }
    }
}




