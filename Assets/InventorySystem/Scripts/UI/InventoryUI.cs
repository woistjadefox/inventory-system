using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Zhdk.Gamelab.InventorySystem
{

    public class InventoryUI : MonoBehaviour
    {

        public delegate void OnDropInventoryUIObject(InventoryUIObject inventoryUIObject);

        [Header("References")]
        [SerializeField]
        private Inventory inventory;
        [SerializeField]
        private InventoryUISlot slotSpecial;
        [SerializeField]
        private GameObject panel;
        [SerializeField]
        private GameObject panelDescription;
        [SerializeField]
        private Text textTitle;
        [SerializeField]
        private Text textDescription;

        [Header("Templates")]
        [SerializeField]
        private InventoryUISlot inventoryUISlotTemplate;
        [SerializeField]
        private InventoryUIObject inventoryUIObjectTemplate;

        [SerializeField]
        private List<InventoryUISlot> slots;

        private InventoryUIObject currentSelectedInventoryUIObject;
        private event OnDropInventoryUIObject onDropInventoryUIObject;

		private void OnDestroy()
		{
            inventory.RemoveOnInventoryUpdateListener(OnInventoryUpdate);
		}

        private void OnInventoryUpdate(Inventory.InventoryUpdateType type, InventoryObject inventoryObject)
        {
            if (type == Inventory.InventoryUpdateType.Add) {
                AddInventoryObject(inventoryObject);
            }

            if (type == Inventory.InventoryUpdateType.Remove) {
                RemoveInventoryObject(inventoryObject);
            }
        }

        public void OnSlotChange(InventoryUISlot newSlot, InventoryUIObject inventoryUIObject) {

            bool isSpecial = inventoryUIObject.GetInventoryObject() == inventory.GetSpecialInventoryObject();

            // normal object to special
            if(isSpecial == false && newSlot == slotSpecial) {
                inventory.MoveNormalInventoryObjectToSpecial(inventoryUIObject.GetInventoryObject());
            }

            // special object to normal
            if(isSpecial && newSlot != slotSpecial) {
                inventory.MoveSpecialInventoryObjectToNormal(inventoryUIObject.GetInventoryObject());
            }

            // set wish pos
            if(newSlot != slotSpecial) {
                inventoryUIObject.GetInventoryObject().SetCurrentPos(slots.IndexOf(newSlot));
            } else {
                inventoryUIObject.GetInventoryObject().SetCurrentPos(0);
            }
        }

		private void Start()
        {

            if(inventory == null) {
                throw new System.Exception("You need to link an inventory object to the this component");
            }

            Init();

        }

		private void Init()
		{
            // add update listener
            inventory.AddOnInventoryUpdateListener(OnInventoryUpdate);

            // hide templates
            inventoryUISlotTemplate.gameObject.SetActive(false);
            inventoryUIObjectTemplate.gameObject.SetActive(false);

            // instantiate all slots according to inventory
            slotSpecial.Init(this);
            InstantiateNewSlots(inventory.GetSlotCount());

            // add all existing inventory objects
            foreach (InventoryObject inventoryObject in inventory.GetInventoryObjects()) {
                AddInventoryObject(inventoryObject);
            }

            // cleanup existing scene inventory objects which are already stored in the inventory
            inventory.CleanupExistingSceneInventoryObjects();

            // add special inventory object if existing
            if (inventory.GetSpecialInventoryObject() != null) {
                AddInventoryObject(inventory.GetSpecialInventoryObject(), true);
            }
		}
       
       
        public void TogglePanel() 
        {
            if(InventoryUIObject.currentObject == null) {
                if (panel.activeSelf) panelDescription.SetActive(false);
                panel.SetActive(!panel.activeSelf);
            }

        }

        public InventoryUISlot GetNextFreeSlot(int wishPos) 
        {

            if(wishPos != -1 && slots.Count > wishPos && slots[wishPos] != null) {
                if (slots[wishPos].IsEmpty()) return slots[wishPos];
            }

            foreach (InventoryUISlot slot in slots){
                if (slot.IsEmpty()) return slot;
            }

            return null;
        }

        public void InstantiateNewSlots(int amount) 
        {
            for (int i = 0; i < amount; i++) {
                
                GameObject templateClone = Instantiate(inventoryUISlotTemplate.gameObject, Vector3.zero, Quaternion.identity);
                templateClone.name = "Slot" + i;
                InventoryUISlot slot = templateClone.GetComponent<InventoryUISlot>();
                slot.SetInventoryUIObject(null);
                slot.GetTransform().SetParent(inventoryUISlotTemplate.GetTransform().parent);
                slot.GetTransform().localScale = inventoryUISlotTemplate.GetTransform().localScale;
                slot.gameObject.SetActive(true);
                slot.Init(this);

                slots.Add(slot);
            }
        }

        public bool AddInventoryObject(InventoryObject inventoryObject, bool isSpecial = false) {

            InventoryUISlot nextFreeSlot;

            // get next free slot
            if(isSpecial && slotSpecial.IsEmpty()) {
                nextFreeSlot = slotSpecial;
            } else {
                nextFreeSlot = GetNextFreeSlot(inventoryObject.GetCurrentPos());
            }

            if(nextFreeSlot != null) {

                // create new inventory ui object from template
                GameObject templateClone = Instantiate(inventoryUIObjectTemplate.gameObject, Vector3.zero, Quaternion.identity);
                templateClone.SetActive(true);
                templateClone.name = inventoryObject.GetTitle();

                // link it with the slot
                InventoryUIObject newInventoryUIObject = templateClone.GetComponent<InventoryUIObject>();
                newInventoryUIObject.SetInventoryObject(inventoryObject);
                newInventoryUIObject.SetCurrentSlot(nextFreeSlot);
                newInventoryUIObject.GetTransform().localScale = inventoryUIObjectTemplate.GetTransform().localScale;

                nextFreeSlot.Init(this);

                return true;
            }

            return false;
        }

        public bool RemoveInventoryObject(InventoryObject inventoryObject)
        {

            bool success = false;

            if(slotSpecial.IsEmpty() == false && slotSpecial.GetInventoryUIObject().GetInventoryObject() == inventoryObject) {

                Destroy(slotSpecial.GetInventoryUIObject().gameObject);
                slotSpecial.SetInventoryUIObject(null);
                success = true;

            } else {

                foreach (InventoryUISlot slot in slots) {
                    if (slot.IsEmpty()) continue;

                    if (slot.GetInventoryUIObject().GetInventoryObject() == inventoryObject) {
                        Destroy(slot.GetInventoryUIObject().gameObject);
                        slot.SetInventoryUIObject(null);
                        success = true;
                    }
                }
            }
            
            return success;
        }

        public void SelectInventoryUIObject(InventoryUIObject inventoryUIObject) {

            textTitle.text = inventoryUIObject.GetInventoryObject().GetTitle();
            textDescription.text = inventoryUIObject.GetInventoryObject().GetDescription();
            panelDescription.SetActive(true);

            currentSelectedInventoryUIObject = inventoryUIObject;
        }

        public void DropCurrentSelectedInventoryUIObject() {
            inventory.RemoveInventoryObject(currentSelectedInventoryUIObject.GetInventoryObject());
            if(onDropInventoryUIObject != null) onDropInventoryUIObject(currentSelectedInventoryUIObject);
            panelDescription.SetActive(false);
        }

        public void AddOnDropInventoryUIObjectListener(OnDropInventoryUIObject listener) {
            onDropInventoryUIObject += listener;
        }

        public void RemoveOnDropInventoryUIObjectListener(OnDropInventoryUIObject listener) {
            onDropInventoryUIObject -= listener;
        }
    }
}