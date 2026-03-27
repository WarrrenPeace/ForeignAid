using System.Collections.Generic;
using UnityEngine;

public class CountryCrisisManager : MonoBehaviour

{

    [SerializeField] List<Country> listOfCountries;
    [SerializeField] float gracePeriodTimer = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("StartGame",gracePeriodTimer);
    }
    void StartGame()
    {
        //Pick random country to put in crisis
        Random.Range(0,listOfCountries.Count);
        Debug.Log(Random.Range(0,listOfCountries.Count));

        //listOfCountries[Random.Range(0,listOfCountries.Count)].
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
