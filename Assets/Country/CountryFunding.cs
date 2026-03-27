using Unity.Multiplayer.Center.Common.Analytics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CountryFunding : MonoBehaviour
{
    [SerializeField] InputActionReference interact;
    private bool isDonating;
    private float timerToSpendCoin = 0.25f;
     
    void Update()
    {
        PlayerInput();
        if(isDonating)DonationTimer();
    }
    void PlayerInput()
    {
        if(interact.action.triggered) //initial click
        {
            timerToSpendCoin = 0.25f;
            isDonating = true;
        }
        if(interact.action.IsPressed()) // bool for holding down spacebar
        {
            //Debug.Log("Pressed");
        }
        else
        {
            isDonating = false;
        }
    }

    void DonationTimer()
    {
        if(timerToSpendCoin - 1 >= 0)
        {
            timerToSpendCoin -= 1 * Time.deltaTime;;
        }
        else
        {
            timerToSpendCoin = 1.5f;
            CheckConditionsForDonatingCoin(1);
        }
        
    }
    void CheckConditionsForDonatingCoin(int amount)
    {
        //Check if eligible country
        if(!CountrySelector.instance.CheckValidCountry(CountrySelector.instance.GetTargetCountry()))
        {Debug.Log("NOT VALID COUNTRY"); return;}
        //Check if country is alive
        if(!CountrySelector.instance.GetTargetCountry().canRecieveFunding())
        {Debug.Log("COUNTRY NOT ACCEPTING"); return;}
        //Check if enough coins in collection
        if(!CoinCollection.instance.HasEnoughCoins())
        {Debug.Log("NOT ENOUGH COINS"); return;}
        //After all of these donate the coin
        
        DonateCoinToCountry(amount);
    }
    void DonateCoinToCountry(int amountToDonate)
    {
    Debug.Log("DONATED COIN");
        CoinCollection.instance.RemoveCoinFromCollection();
        CountrySelector.instance.GetTargetCountry().DonateFunding(amountToDonate);

        //Visualize this somehow?
    }
}
