using System.Collections.Generic;
using UnityEngine;

public class CountryCrisisManager : MonoBehaviour
{
    public static CountryCrisisManager instance;
    [SerializeField] List<Country> listOfCountries;
    public CountryHome home;
    [SerializeField] float gracePeriodTimer = 5;
    [SerializeField] float crisisTimerMin = 4;

    void Awake()
    {
        instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("StartGame",gracePeriodTimer);
        
    }
    public void AddCountryToList(Country country)
    {
        listOfCountries.Add(country);
    }
    public void RemoveCountryFromList(Country country)
    {
        listOfCountries.Remove(country);
    }
    public void AssignHomeCountry(CountryHome country)
    {
        home = country;
        home.ForceSpawnTaxes(15);
    }
    void StartGame()
    {
        //Pick random country to put in crisis
        InflictCrisis();

        Invoke("DetermineNextCrisis",Random.Range(crisisTimerMin,11));
    }

    void DetermineNextCrisis()
    {
        if(Random.Range(0,101) >= 30) //chance to start crisis
        {
            InflictCrisis();
        }
        else
        {
            if(Random.Range(0,101) >= 50) {home.ForceSpawnTaxes(Random.Range(0,11));} //50% chance no crisis will grant bonus coins
        }
        Invoke("DetermineNextCrisis",Random.Range(crisisTimerMin,10)); //Repeats only after actually doing crisis
    }
    void InflictCrisis()
    {
        int InCrisis = 0;
        Shuffle(listOfCountries);
        for (int i = 0; i < listOfCountries.Count; i++)
        {
            if(listOfCountries[i].isInCrisis()) //Country already needs help skip it
            {
                //Skip it and find another
                Debug.Log(listOfCountries[i].name + " Already in crisis, skipping it now");
                InCrisis += 1;
                if(InCrisis >= listOfCountries.Count)
                {
                    Debug.Log("Everyone is in crisis");
                }
                continue;
            }
            else 
            {
                //Apply crisis
                Debug.Log(listOfCountries[i].name +" Has no crisis, adding it now");
                float localDiff;
                localDiff = YearTimer.instance.getTotalTimePassed() * 0.1f;
                listOfCountries[i].SetUpRandomCrisis(Random.Range(-10 -(int)localDiff, -4),-Random.Range(-50, -12));
                break;
            }
        }
        
    }
    void Shuffle<Country>(List<Country> list)
    {
        int length = list.Count;
        for (int i = 0; i < length; i++)
        {
            int r = i + Random.Range(0, length - i);
            Country c = list[r];
            list[r] = list[i];
            list[i] = c;
        }
    }
}
