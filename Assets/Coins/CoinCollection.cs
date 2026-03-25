using UnityEngine;

public class CoinCollection : MonoBehaviour
{
    public static CoinCollection instance;
    [SerializeField] int coinAmount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    public void AddCoinToCollection(int amount)
    {
        coinAmount += amount;
        Debug.Log("Added coin");
    }
}
