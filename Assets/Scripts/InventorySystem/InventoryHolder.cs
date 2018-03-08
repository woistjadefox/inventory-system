using UnityEngine;

namespace Zhdk.Gamelab.InventorySystem {

    public class InventoryHolder : MonoBehaviour
    {
        [Header("References")]
        [SerializeField]
        private InventoryObject inventoryObject;
        [SerializeField]
        private InventoryManager inventoryManager;

        [Header("Actor Settings")]
        [SerializeField]
        private bool pickupOnEnter = true;
        [SerializeField]
        private KeyCode pickUpKey = KeyCode.None;

        private bool actorIsClose = false;
        private new Transform transform;

        public Transform GetTransform()
        {
            if (transform == null) transform = GetComponent<Transform>();
            return transform;
        }

        private void Awake()
        {
            GetTransform();
        }

        public void ActorIsClose(bool state)
        {
            actorIsClose = state;

            if (state && pickupOnEnter) PickUp();
        }

        private void Update()
        {
            if(actorIsClose && pickUpKey != KeyCode.None) {

                if(Input.GetKeyDown(pickUpKey)) {
                    PickUp();
                }
            }
        }

        public void PickUp () {
            inventoryManager.AddInventoryObject(this);
        }

        public InventoryObject GetInventoryObject()
        {
            return inventoryObject;
        }

    }

}

