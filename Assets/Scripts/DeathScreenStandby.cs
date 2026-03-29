using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DeathScreenStandby : MonoBehaviour
{
    [SerializeField] InputActionReference interact;

    void Update()
    {
        if(interact.action.triggered) //initial click
        {
            SceneManager.LoadScene(0);
        }
    }
}
