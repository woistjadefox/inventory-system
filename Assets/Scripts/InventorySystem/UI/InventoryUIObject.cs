using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Zhdk.Gamelab.InventorySystem {

    public class InventoryUIObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {

        public static InventoryUIObject currentObject;

        [SerializeField]
        private InventoryObject inventoryObject;
        [SerializeField]
        private InventoryHolder holder;
        [SerializeField]
        private Image image;

        private InventoryUISlot currentSlot;
        private Transform emptyParent;
        private InventoryUISlot startSlot;
        private new Transform transform;

        public void Init()
        {
            emptyParent = GetTransform().parent.parent.parent;
            image.sprite = inventoryObject.GetSprite();

        }

        public Transform GetTransform() 
        {
            if (transform == null) transform = GetComponent<Transform>();
            return transform;
        }

        public void SetHolder(InventoryHolder holder) 
        {
            this.holder = holder;
            inventoryObject = holder.GetInventoryObject();
        }

        public InventoryObject GetInventoryObject()
        {
            return inventoryObject;
        }

        public InventoryHolder GetHolder () 
        {
            return holder;
        }

        public InventoryUISlot GetCurrentSlot() 
        {
            return currentSlot;
        }

        public void SetCurrentSlot(InventoryUISlot slot) 
        {
            currentSlot = slot;
            currentSlot.SetInventoryUIObject(this);
            GetTransform().SetParent(slot.GetTransform());
            GetTransform().localPosition = Vector3.zero;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            currentObject = this;
            currentObject.GetTransform().SetParent(emptyParent);
            currentObject.GetTransform().SetAsFirstSibling();
            startSlot = currentObject.GetCurrentSlot();
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