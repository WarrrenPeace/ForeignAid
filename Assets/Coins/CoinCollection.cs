using UnityEngine;
using TMPro;

public class CoinCollection : MonoBehaviour
{
    public static CoinCollection instance;
    [SerializeField] TextMeshProUGUI coinAmountText;
    [SerializeField] int amountOfCoins;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        instance = this;
    }
    public bool HasEnoughCoins()
    {
        if(amountOfCoins -1 >= 0)
        {
            return true;
        }
        else
        {
            coinAmountText.text = "";
            return false;
        }
    }
    // Update is called once per frame
    public void AddCoinToCollection(int amount)
    {
        amountOfCoins += amount;
        coinAmountText.text = amountOfCoins.ToString();
    }
    public void RemoveCoinFromCollection()
    {
        if(amountOfCoins - 1 >= 0)
        {
            amountOfCoins -= 1;
            if(amountOfCoins > 0)
            {
                coinAmountText.text = amountOfCoins.ToString();
            }
            else
            {
                coinAmountText.text = "";
            }
            
        }
        else
        {
            Debug.Log("Spent coin you didnt have");
        }
    }
}
