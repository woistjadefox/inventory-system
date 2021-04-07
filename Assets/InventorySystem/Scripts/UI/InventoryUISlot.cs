using UnityEngine;
using UnityEngine.EventSystems;

namespace Zhdk.Gamelab.InventorySystem
{
    public class InventoryUISlot : MonoBehaviour, IDropHandler
    {
        [SerializeField] private InventoryUIObject inventoryUIObject;

        private InventoryUI inventoryUI;
        private new Transform transform;

        public void Init(InventoryUI inventoryUI)
        {
            this.inventoryUI = inventoryUI;

            if (inventoryUIObject != null)
            {
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

        public InventoryUIObject GetInventoryUIObject()
        {
            return inventoryUIObject;
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (inventoryUIObject == null)
            {
                InventoryUIObject.currentObject.GetCurrentSlot().SetInventoryUIObject(null);
                SetInventoryUIObject(InventoryUIObject.currentObject);
                InventoryUIObject.currentObject.SetCurrentSlot(this);
                inventoryUI.OnSlotChange(this, GetInventoryUIObject());
            }
        }
    }
}