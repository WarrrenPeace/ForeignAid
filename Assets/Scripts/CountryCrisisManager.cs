using System.Collections.Generic;
using UnityEngine;

public class CountryCrisisManager : MonoBehaviour
{
    public static CountryCrisisManager instance;
    [SerializeField] List<Country> listOfCountries;
    private CountryHome home;
    [SerializeField] float gracePeriodTimer = 5;
    [SerializeField] float crisisTimer = 10;

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
        //Repeat this every 5 seconds
        InvokeRepeating("DetermineNextCrisis",crisisTimer,crisisTimer);
    }

    void DetermineNextCrisis()
    {
        if(Random.Range(0,101) >= 50)
        {
            InflictCrisis();
        }
        else
        {
            Debug.Log("skipped");
        }
    }
    void InflictCrisis()
    {
        for (int i = 0; i < listOfCountries.Count; i++)
        {
            Shuffle(listOfCountries);
            if(listOfCountries[i].isInCrisis()) //Country already in need
            {
                //Skip it and find another
                continue;
            }
            else 
            {
                //Apply crisis
                listOfCountries[i].SetUpRandomCrisis(Random.Range(-10, -4),-Random.Range(-30, -9));
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
