using UnityEngine;
using System.Collections.Generic;

namespace Zhdk.Gamelab.InventorySystem
{

    public class InventoryManager : MonoBehaviour
    {

        [Header("References")]
        [SerializeField]
        private GameObject uiPanel;
        [SerializeField]
        private List<InventoryUISlot> slots;
        [SerializeField]
        private InventoryUISlot slotHand;
        [SerializeField]
        private InventoryUIObject inventoryUIObjectTemplate;

        private void Start()
        {
            // init all existing slots and objects
            foreach (InventoryUISlot slot in slots) {
                slot.Init();
            }
        }

        public void TogglePanel() 
        {
            if(InventoryUIObject.currentObject == null) {
                uiPanel.SetActive(!uiPanel.activeSelf);
            }

        }

        public InventoryUISlot GetNextFreeSlot() 
        {
            foreach (InventoryUISlot slot in slots){
                if (slot.IsEmpty()) return slot;
            }

            return null;
        }

        public void AddInventoryObject(InventoryHolder holder) {

            // check if there is a free slot
            // get next free slot
            InventoryUISlot nextFreeSlot = GetNextFreeSlot();

            if(nextFreeSlot != null) {

                // create new ui object from template
                GameObject templateClone = Instantiate(inventoryUIObjectTemplate.gameObject, Vector3.zero, Quaternion.identity);
                templateClone.SetActive(true);
                templateClone.name = holder.GetInventoryObject().GetTitle();

                InventoryUIObject newInventoryUIObject = templateClone.GetComponent<InventoryUIObject>();

                newInventoryUIObject.SetHolder(holder);
                newInventoryUIObject.SetCurrentSlot(nextFreeSlot);
                nextFreeSlot.Init();

                holder.GetTransform().SetParent(gameObject.transform);
                holder.gameObject.SetActive(false);


            } else {
                Debug.LogWarning("Inventory is full!");
            }
        }

        public InventoryHolder RemoveInventoryObject(InventoryObject inventoryObject)
        {

            InventoryHolder holder = null;

            foreach (InventoryUISlot slot in slots)
            {

                if (slot.IsEmpty()) continue;

                if (slot.GetInventoryUIObject().GetInventoryObject() == inventoryObject) {
                    holder = slot.GetInventoryUIObject().GetHolder();
                    Destroy(slot.GetInventoryUIObject().gameObject);
                    slot.SetInventoryUIObject(null);
                }
            }

            return holder;
        }

        public InventoryUISlot IsInInventory(InventoryObject inventoryObject)
        {
            foreach (InventoryUISlot slot in slots)
            {
                if (slot.IsEmpty()) continue;

                if (slot.GetInventoryUIObject().GetInventoryObject() == inventoryObject)
                {
                    return slot;
                }
            }

            return null;
        }

    }
}