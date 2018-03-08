using UnityEngine;
using UnityEngine.Events;

public class GenericTrigger : MonoBehaviour {

    [Header("Settings")]
    [SerializeField]
    private bool ignoreIncomingTrigger = true;
    [SerializeField]
    private Collider[] specificCollider;

    [Header("Events")]
    [SerializeField]
    private UnityEvent onEnter;
    [SerializeField]
    private UnityEvent onExit;


    private void OnTriggerEnter(Collider other)
    {

        if(ignoreIncomingTrigger && other.isTrigger) {
            return;
        }

        if(CheckSpecificCollider(other) == false) {
            return;
        }

        onEnter.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (ignoreIncomingTrigger && other.isTrigger) {
            return;
        }

        if (CheckSpecificCollider(other) == false) {
            return;
        }

        onExit.Invoke();
    }

    private bool CheckSpecificCollider(Collider targetCollider) {

        if (specificCollider.Length > 0) {

            bool foundSpecificCollider = false;

            foreach (Collider col in specificCollider)
            {
                if (col == targetCollider) foundSpecificCollider = true;
            }

            return foundSpecificCollider;

        } else {
            return true;
        }
    }

}
