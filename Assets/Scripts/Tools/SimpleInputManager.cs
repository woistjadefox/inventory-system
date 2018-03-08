using UnityEngine;
using UnityEngine.Events;

public class SimpleInputManager : MonoBehaviour
{
    [SerializeField]
    private UnityEvent onPressTabDown;
   
    private void Update()
    {
       
        if(Input.GetKeyDown(KeyCode.Tab)) {
            onPressTabDown.Invoke();
        }
    }
}



