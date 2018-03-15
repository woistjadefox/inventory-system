using UnityEngine;
using UnityEngine.Events;


namespace Zhdk.Gamelab.InventorySystem {

    public class InventoryObjectCheck : MonoBehaviour {
        [Header("References")]
        [SerializeField]
        private InventoryObject[] inventoryObjects;
        [SerializeField]
        private Inventory inventory;

        [Header("Events")]
        [SerializeField]
        private UnityEvent onIsInventoryEvent;
        [SerializeField]
        private UnityEvent onIsNotInventoryEvent;

        public void Check() {
            bool oneExists = false;

            foreach (InventoryObject inventoryObject in inventoryObjects) {
                if (inventory.IsInInventory(inventoryObject)) {
                    oneExists = true;
                }
            }

            if (oneExists) {
                onIsInventoryEvent.Invoke();
            } else {
                onIsNotInventoryEvent.Invoke();
            }
        }

    }
}




