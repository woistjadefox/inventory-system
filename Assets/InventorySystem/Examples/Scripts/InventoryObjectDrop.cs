using UnityEngine;
using UnityEngine.Events;
using Zhdk.Gamelab.InventorySystem;


public class InventoryObjectDrop : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private InventoryUI inventoryUI;

    [Header("Events")]
    [SerializeField]
    private UnityEvent onDrop;

    private void OnDestroy() {
        if(inventoryUI != null) {
            inventoryUI.RemoveOnDropInventoryUIObjectListener(OnDrop);
        }
    }

    private void Start() {
        inventoryUI.AddOnDropInventoryUIObjectListener(OnDrop);
    }

    public void OnDrop(InventoryUIObject inventoryUIObject)
    {
        Instantiate(inventoryUIObject.GetInventoryObject().GetPrefab(), transform.position, Quaternion.identity);
        onDrop.Invoke();
    }
       
}



