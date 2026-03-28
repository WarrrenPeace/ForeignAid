using Unity.Multiplayer.Center.Common.Analytics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CountryFunding : MonoBehaviour
{
    [SerializeField] InputActionReference interact;
    private bool isDonating;
    private float donationSpeedUpTimer = 1f;
    private float speedUpMult = 1f;
    private float timerToSpendCoin = 1f;
     
    void Update()
    {
        PlayerInput();
        DonationTimer();
    }
    void PlayerInput()
    {
        if(interact.action.triggered) //initial click
        {
            timerToSpendCoin = 0.25f;
            donationSpeedUpTimer = 1;
            isDonating = true;
        }
        if(interact.action.IsPressed()) // bool for holding down spacebar
        {
            //Debug.Log("Pressed");
        }
        else
        {
            isDonating = false;
            donationSpeedUpTimer = 1;
        }
    }

    void DonationTimer()
    {
        if(isDonating)
        {
            if(donationSpeedUpTimer + speedUpMult <= 12)
            {
                donationSpeedUpTimer += speedUpMult * Time.deltaTime;
            }

            if(timerToSpendCoin - 1 >= 0)
            {
                timerToSpendCoin -= 1 * donationSpeedUpTimer * Time.deltaTime;
            }
            else
            {
                timerToSpendCoin = 1.5f;
                CheckConditionsForDonatingCoin(1);
            }
        }
        
        
    }
    void CheckConditionsForDonatingCoin(int amount)
    {
        //Check if eligible country
        if(!CountrySelector.instance.CheckValidCountry(CountrySelector.instance.GetTargetCountry()))
        {return;}
        //Check if country is alive
        if(!CountrySelector.instance.GetTargetCountry().canRecieveFunding())
        {return;}
        //Check if enough coins in collection
        if(!CoinCollection.instance.HasEnoughCoins())
        {return;}
        //After all of these donate the coin
        
        DonateCoinToCountry(amount);
    }
    void DonateCoinToCountry(int amountToDonate)
    {
        CoinCollection.instance.RemoveCoinFromCollection();
        CountrySelector.instance.GetTargetCountry().DonateFunding(amountToDonate);

        //Visualize this somehow?
    }
}
