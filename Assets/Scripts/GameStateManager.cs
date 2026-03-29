using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;
    [SerializeField] GameObject victoryScreen;
    [SerializeField] GameObject deathScreen;


    void Awake()
    {
        instance = this;
    }
    public void OnVictory()
    {
        victoryScreen.SetActive(true);
        Invoke("ResetToMain",10);
    }
    
    public void OnDeath()
    {
        deathScreen.SetActive(true);
        Invoke("ResetToMain",10);
    }
    void ResetToMain()
    {
        SceneManager.LoadScene(0);
    }
}
