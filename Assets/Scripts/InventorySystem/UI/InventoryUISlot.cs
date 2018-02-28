using UnityEngine;
using UnityEngine.EventSystems;

namespace Zhdk.Gamelab.InventorySystem {

    public class InventoryUISlot : MonoBehaviour, IDropHandler
    {

        private new Transform transform;

        private void Awake()
        {
            GetTransform();
        }

        public Transform GetTransform()
        {
            if (transform == null) transform = GetComponent<Transform>();
            return transform;
        }

        public void OnDrop(PointerEventData eventData)
        {
            InventoryUISlotObject.currentObject.SetCurrentSlot(this);
        }
    }
}