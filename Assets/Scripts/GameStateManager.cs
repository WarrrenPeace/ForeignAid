using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;
    [SerializeField] GameObject victoryScreen;
    [SerializeField] GameObject deathScreen;
    private int countriesLost = 0;
    private bool isGameOver;


    void Awake()
    {
        instance = this;
    }
    public void OnCountryDeath()
    {
        countriesLost += 1;
        if(countriesLost >= 2)
        {OnDeath();}
    }
    public bool IsGameOver()
    {
        return isGameOver;
    }
    public void OnVictory()
    {
        if(!isGameOver)
        {
            isGameOver = true;
            victoryScreen.SetActive(true);
        }
        
    }
    
    public void OnDeath()
    {
        if(!isGameOver)
        {
            isGameOver = true;
            deathScreen.SetActive(true);
            Destroy(MusicManager.instance);
            Invoke("ResetToMain",10);
        }
        
    }
    void ResetToMain()
    {
        SceneManager.LoadScene(0);
    }
}
