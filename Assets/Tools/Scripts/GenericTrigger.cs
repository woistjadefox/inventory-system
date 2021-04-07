using UnityEngine;
using UnityEngine.Events;

public class GenericTrigger : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private bool ignoreIncomingTrigger = true;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Collider[] specificCollider;

    [Header("Events")]
    [SerializeField] private UnityEvent onEnter;
    [SerializeField] private UnityEvent onExit;

    [Header("Key Events")]
    [SerializeField] private KeyCode onEnterKey = KeyCode.None;
    [SerializeField] private UnityEvent onEnterKeyDown;

    private bool isActive = false;

    private void OnTriggerEnter(Collider other)
    {
        if (CheckConditions(other))
        {
            isActive = true;
            onEnter.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (CheckConditions(other))
        {
            isActive = false;
            onExit.Invoke();
        }
    }

    private void Update()
    {
        if (isActive && onEnterKey != KeyCode.None && Input.GetKeyDown(onEnterKey))
        {
            onEnterKeyDown.Invoke();
        }
    }

    private bool CheckConditions(Collider other)
    {
        if (ignoreIncomingTrigger && other.isTrigger)
        {
            return false;
        }

        if (CheckSpecificCollider(other) == false)
        {
            return false;
        }

        if (IsInLayerMask(other.gameObject.layer, layerMask) == false)
        {
            return false;
        }

        return true;
    }

    private bool CheckSpecificCollider(Collider targetCollider)
    {
        if (specificCollider.Length > 0)
        {

            bool foundSpecificCollider = false;

            foreach (Collider col in specificCollider)
            {
                if (col == targetCollider) foundSpecificCollider = true;
            }

            return foundSpecificCollider;

        }
        else
        {
            return true;
        }
    }

    private bool IsInLayerMask(int layer, LayerMask layermask)
    {
        return layermask == (layermask | (1 << layer));
    }
}
