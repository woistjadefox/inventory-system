using UnityEngine;

namespace Zhdk.Gamelab.InventorySystem {

    public class InventoryObjectCheck : MonoBehaviour
    {
        [Header("References")]
        [SerializeField]
        private InventoryObject inventoryObject;
        [SerializeField]
        private InventoryManager inventoryManager;


        public void Check()
        {
            if(inventoryManager.IsInInventory(inventoryObject)){

                InventoryHolder holder = inventoryManager.RemoveInventoryObject(inventoryObject);
                holder.gameObject.SetActive(true);
                holder.GetTransform().position = transform.position;
            }
        }
       
    }

}

