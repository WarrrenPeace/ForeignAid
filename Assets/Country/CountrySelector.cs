using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class CountrySelector : MonoBehaviour
{
    public static CountrySelector instance;
    [SerializeField] List<Country> selectedCountries;
    [SerializeField] Country targetCountry;

    [SerializeField] TextMeshProUGUI targetCountrytext;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        InvokeRepeating("DetermineTick",0,0.05f);
    }
    public Country GetTargetCountry()
    {
        if(targetCountry)
        {
            return targetCountry;
        }
        else
        return null;
    }
    public bool CheckValidCountry(Country country)
    {
        if(country.tag == "OtherCountry")
        {
            return true;
        }
        else if (country.tag == "HomeCountry")
        {
            return false;
        }
        else
        return false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Country"))
        {
            selectedCountries.Add(collision.gameObject.GetComponent<Country>());
        }
        
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Country"))
        {
            selectedCountries.Remove(collision.gameObject.GetComponent<Country>());
        }
        
    }
    void DetermineTick()
    {
        DetermineCountryPlayerIsStandingIn();
    }
    void DetermineCountryPlayerIsStandingIn()
    {
        if(selectedCountries.Count >= 1)
        {
            if(targetCountry != null) {targetCountry.ToggleBorder(false);}
            targetCountry = selectedCountries[0];
            UpdateTargetCountryVisual();
        }
        else
        {
            if(targetCountry != null) {targetCountry.ToggleBorder(false);}
            targetCountry = null;
            targetCountrytext.text = "";
        }
        
    }
    void UpdateTargetCountryVisual()
    {
        targetCountry.ToggleBorder(true);
        targetCountrytext.text = targetCountry.name;
    }
}
