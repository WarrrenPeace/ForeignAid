using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
            //Debug.Log("Checked home");
            return false;
        }
        else
        return false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Country"))
        {
            //Debug.Log("Entered" + collision.name);
            selectedCountries.Add(collision.gameObject.GetComponent<Country>());
        }
        
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Country"))
        {
            //Debug.Log("left" + collision.name);
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
            targetCountry = selectedCountries[0];
            UpdateTargetCountryVisual();
        }
        else
        {
            targetCountry = null;
            targetCountrytext.text = "";
        }
        
    }
    void UpdateTargetCountryVisual()
    {
        targetCountrytext.text = targetCountry.name;
    }
}
