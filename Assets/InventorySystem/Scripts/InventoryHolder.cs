using UnityEngine;

namespace Zhdk.Gamelab.InventorySystem {

    public class InventoryHolder : MonoBehaviour {
        [Header("References")]
        [SerializeField]
        private InventoryObject inventoryObject;
        [SerializeField]
        private Inventory inventory;

        [Header("Pickup Settings")]
        [SerializeField]
        private bool deleteOnPickup = true;

        public Inventory GetInventory()
        {
            return inventory;
        }

        public InventoryObject GetInventoryObject()
        {
            return inventoryObject;
        }

        public void PickUp() {

            if (inventory.AddInventoryObject(inventoryObject)) {
                if (deleteOnPickup) Destroy(gameObject);
            }
        }

        public void Delete()
        {
            Destroy(gameObject);
        }

    }
}


