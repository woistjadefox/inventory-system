using UnityEngine;

namespace Zhdk.Gamelab.InventorySystem {

    public class InventoryObjectCheck : MonoBehaviour
    {
        [Header("References")]
        [SerializeField]
        private InventoryObject inventoryObject;
        [SerializeField]
        private Inventory inventory;


        public void Check()
        {
            if(inventory.IsInInventory(inventoryObject)){
                
                inventory.RemoveInventoryObject(inventoryObject);
                Instantiate(inventoryObject.GetPrefab(), transform.position, Quaternion.identity);
            }
        }
       
    }

}

