using UnityEngine;
using System.Collections.Generic;

namespace Zhdk.Gamelab.InventorySystem
{
    [CreateAssetMenu(fileName = "NewInventory", menuName = "Inventory System/Inventory", order = 0)]
    public class Inventory : ScriptableObject
    {
        public enum InventoryUpdateType { Add, Remove }
        public delegate void OnInventoryUpdate(InventoryUpdateType type, InventoryObject inventoryObject);

        [Header("Settings")]
        [SerializeField] private string title = "";
        [SerializeField] private string description = "";
        [SerializeField] private int inventorySlots = 15;
        [Tooltip("Cleanup existing scene inventory objects which are already stored in this inventory.")]
        [SerializeField] private bool sceneCleanUp;
        [SerializeField] private bool logWarnings = false;

        [Header("Pool")]
        [SerializeField] private InventoryObject startSpecialInventoryObject;
        [SerializeField] private List<InventoryObject> startInventoryObjects = new List<InventoryObject>();

        [System.NonSerialized] private InventoryObject specialInventoryObject;
        [System.NonSerialized] private List<InventoryObject> inventoryObjects;
        [System.NonSerialized] private bool initDone = false;
        private event OnInventoryUpdate onInventoryUpdate;

        public string GetTitle()
        {
            return title;
        }

        public string GetDescription()
        {
            return description;
        }

        public int GetSlotCount()
        {
            return inventorySlots;
        }

        public InventoryObject GetSpecialInventoryObject()
        {
            return specialInventoryObject;
        }

        public List<InventoryObject> GetInventoryObjects()
        {
            if (initDone == false)
            {
                inventoryObjects = new List<InventoryObject>();

                foreach (InventoryObject iObj in startInventoryObjects)
                {
                    if (iObj == null) continue;
                    inventoryObjects.Add(iObj);
                }

                specialInventoryObject = startSpecialInventoryObject;

                initDone = true;
            }

            return inventoryObjects;
        }

        public bool AddInventoryObject(InventoryObject inventoryObject, bool isSpecial = false)
        {
            bool success = false;

            // handle special inventory object
            if (isSpecial)
            {
                if (specialInventoryObject == null)
                {
                    specialInventoryObject = inventoryObject;
                    success = true;
                }
                else
                {
                    if (logWarnings) Debug.LogWarning(name + ": specialInventoryObject is already set! Remove it first!");
                }
            }
            else
            {
                // handle normal inventory objects
                if (inventoryObjects.Count < inventorySlots)
                {
                    if (inventoryObjects.Contains(inventoryObject) == false && inventoryObject != specialInventoryObject)
                    {
                        inventoryObjects.Add(inventoryObject);
                        inventoryObject.OnAddToInventory(this);
                        success = true;
                    }
                    else
                    {
                        if (logWarnings) Debug.LogWarning(name + ":" + inventoryObject.name + " is already in inventory!");
                    }
                }
                else
                {
                    if (logWarnings) Debug.LogWarning(name + ": inventory is full!");
                }
            }

            if (success && onInventoryUpdate != null) onInventoryUpdate(InventoryUpdateType.Add, inventoryObject);
            return success;
        }

        public bool RemoveInventoryObject(InventoryObject inventoryObject)
        {
            bool success = false;

            if (specialInventoryObject == inventoryObject)
            {
                specialInventoryObject = null;
                success = true;
            }
            else
            {
                if (inventoryObjects.Contains(inventoryObject))
                {
                    inventoryObjects.Remove(inventoryObject);
                    inventoryObject.OnRemoveFromInventory(this);
                    success = true;
                }
                else
                {
                    if (logWarnings) Debug.LogWarning(name + ":" + inventoryObject.name + " is not set in this inventory!");
                    success = false;
                }
            }

            if (success && onInventoryUpdate != null) onInventoryUpdate(InventoryUpdateType.Remove, inventoryObject);

            return success;
        }

        public void MoveSpecialInventoryObjectToNormal(InventoryObject inventoryObject)
        {
            specialInventoryObject = null;
            inventoryObjects.Add(inventoryObject);
        }

        public void MoveNormalInventoryObjectToSpecial(InventoryObject inventoryObject)
        {
            inventoryObjects.Remove(inventoryObject);
            specialInventoryObject = inventoryObject;
        }

        public bool IsInInventory(InventoryObject inventoryObject)
        {
            if (inventoryObject == specialInventoryObject) return true;

            foreach (InventoryObject iObject in inventoryObjects)
            {
                if (iObject == inventoryObject) return true;
            }

            return false;
        }

        public void CleanupExistingSceneInventoryObjects()
        {
            if (sceneCleanUp)
            {
                foreach (InventoryHolder holder in FindObjectsOfType<InventoryHolder>())
                {
                    if (holder.GetInventory() == this && IsInInventory(holder.GetInventoryObject()))
                    {
                        holder.Delete();
                    }
                }
            }
        }

        public void AddOnInventoryUpdateListener(OnInventoryUpdate listener)
        {
            onInventoryUpdate += listener;
        }

        public void RemoveOnInventoryUpdateListener(OnInventoryUpdate listener)
        {
            onInventoryUpdate -= listener;
        }
    }
}