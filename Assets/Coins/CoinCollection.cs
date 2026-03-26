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
    public void RemoveCoinFromCollection(int amount)
    {
        if(coinAmount - amount >= 0)
        {
            coinAmount -= amount;
            Debug.Log("removed coin");
        }
        else
        {
            Debug.Log("not enough coins to remove");
        }
    }
}
