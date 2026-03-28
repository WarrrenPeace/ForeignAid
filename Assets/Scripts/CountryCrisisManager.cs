using System.Collections.Generic;
using UnityEngine;

public class CountryCrisisManager : MonoBehaviour
{
    public static CountryCrisisManager instance;
    [SerializeField] List<Country> listOfCountries;
    private CountryHome home;
    [SerializeField] float gracePeriodTimer = 5;
    [SerializeField] float crisisTimer = 5;

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
        if(Random.Range(0,101) >= 30)
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
        Country nextTarget;
        nextTarget = listOfCountries[Random.Range(0,listOfCountries.Count)];
        if(nextTarget.isInCrisis())
        {
            nextTarget.SetUpRandomCrisis(Random.Range(-75, -15),Random.Range(0.25f, 0.51f));
        }
        else
        {
            InflictCrisis();
        }
    }
}
