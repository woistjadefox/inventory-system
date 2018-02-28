using UnityEngine;
using UnityEngine.EventSystems;

namespace Zhdk.Gamelab.InventorySystem {

    public class InventoryUISlotObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {

        public static InventoryUISlotObject currentObject;

        private InventoryUISlot currentSlot;
        private Transform emptyParent;
        private InventoryUISlot startSlot;

        private new Transform transform;

        private void Awake()
        {
            GetTransform();
            emptyParent = transform.parent.parent.parent;
        }

        public Transform GetTransform() 
        {
            if (transform == null) transform = GetComponent<Transform>();
            return transform;
        }

        public InventoryUISlot GetCurrentSlot() 
        {
            return currentSlot;
        }

        public void SetCurrentSlot(InventoryUISlot slot) 
        {
            currentSlot = slot;
            GetTransform().SetParent(slot.GetTransform());
            GetTransform().localPosition = Vector3.zero;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            currentObject = this;
            startSlot = currentObject.GetCurrentSlot();
            currentObject.GetTransform().SetParent(emptyParent);
            currentObject.GetTransform().SetAsFirstSibling();
        }

        public void OnDrag(PointerEventData eventData)
        {
            GetTransform().position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {

            if(startSlot == GetCurrentSlot()) {
                GetTransform().SetParent(GetCurrentSlot().GetTransform());
                GetTransform().localPosition = Vector3.zero;
            }

            currentObject = null;
        }
    }
}