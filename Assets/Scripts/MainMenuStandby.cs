using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenuStandby : MonoBehaviour
{
    [SerializeField] InputActionReference interact;

    void Update()
    {
        if(interact.action.triggered) //initial click
        {
            SceneManager.LoadScene(1);
        }
    }
}
