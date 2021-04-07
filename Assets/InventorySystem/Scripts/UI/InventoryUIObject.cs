using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Zhdk.Gamelab.InventorySystem
{
    public class InventoryUIObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public static InventoryUIObject currentObject;

        [SerializeField] private InventoryObject inventoryObject;
        [SerializeField] private Image image;

        private InventoryUISlot currentSlot;
        private Transform emptyParent;
        private InventoryUISlot startSlot;
        private new Transform transform;

        public void Init()
        {
            emptyParent = GetTransform().parent.parent.parent;

            if (GetInventoryObject() != null)
            {
                image.sprite = inventoryObject.GetSprite();
            }
        }

        public Transform GetTransform()
        {
            if (transform == null) transform = GetComponent<Transform>();
            return transform;
        }

        public InventoryObject GetInventoryObject()
        {
            return inventoryObject;
        }

        public void SetInventoryObject(InventoryObject inventoryObject)
        {
            this.inventoryObject = inventoryObject;
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
            // this is true if we are outside the UI
            /*
            if (EventSystem.current.IsPointerOverGameObject(eventData.pointerId) == false)
            {
                // do interesting stuff
            }
            */

            if (startSlot == GetCurrentSlot())
            {
                GetTransform().SetParent(GetCurrentSlot().GetTransform());
                GetTransform().localPosition = Vector3.zero;
            }

            currentObject = null;
        }
    }
}