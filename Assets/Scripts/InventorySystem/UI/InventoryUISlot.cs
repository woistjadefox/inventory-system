using UnityEngine;
using UnityEngine.EventSystems;

namespace Zhdk.Gamelab.InventorySystem {

    public class InventoryUISlot : MonoBehaviour, IDropHandler
    {
        [SerializeField]
        private InventoryUIObject inventoryUIObject;

        private new Transform transform;

        public void Init()
        {
            if(GetTransform().childCount > 0) {
                inventoryUIObject = GetTransform().GetChild(0).GetComponent<InventoryUIObject>();
            }

            if(inventoryUIObject != null) {
                inventoryUIObject.SetCurrentSlot(this);
                inventoryUIObject.Init();
            }

        }

        public Transform GetTransform()
        {
            if (transform == null) transform = GetComponent<Transform>();
            return transform;
        }

        public bool IsEmpty() 
        {
            if (inventoryUIObject == null) return true;
            return false;
        }

        public void SetInventoryUIObject(InventoryUIObject obj) 
        {
            inventoryUIObject = obj;
        }

        public InventoryUIObject GetInventoryUIObject() {
            return inventoryUIObject;
        }

        public void OnDrop(PointerEventData eventData)
        {

            if(inventoryUIObject == null) {
                InventoryUIObject.currentObject.GetCurrentSlot().SetInventoryUIObject(null);
                SetInventoryUIObject(InventoryUIObject.currentObject);
                InventoryUIObject.currentObject.SetCurrentSlot(this);

            }
        }
    }
}