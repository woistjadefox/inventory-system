using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SimpleInputManager : MonoBehaviour
{
    [SerializeField] private UnityEvent onPressTabDown;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            onPressTabDown.Invoke();
        }

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}



