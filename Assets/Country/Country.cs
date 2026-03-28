using UnityEngine;
using TMPro;
using System;
using UnityEditor.PackageManager;

public class Country : MonoBehaviour
{
    public enum Condition
    {
        Alive, InCrisis, Dead
    }
    [SerializeField] Condition StateCondition;
    public PolygonCollider2D PC;
    public SpriteRenderer SR;
    public Animator AM;
    [SerializeField] TextMeshProUGUI GUICountryName;
    [SerializeField] TextMeshProUGUI GUICountryFunding;
    [SerializeField] TextMeshProUGUI GUICountryCollapse;
    [SerializeField] Color postiveBalance;
    [SerializeField] Color negativeBalance;

    [SerializeField] float currentFUNDING; //how many coins county has
    [SerializeField] float deathTimer; //how much time until death
    bool isOutOfFunding;



    void Start()
    {
        CountryCrisisManager.instance.AddCountryToList(this); //dont do with home country
        CountryStart();
        SetUPGUI();
    }
    public void SetUPGUI()
    {
        GUICountryName.text = gameObject.name;
    }
    String FundingToString()
    {
        int temp = (int)currentFUNDING;
        return temp.ToString();
    }
    public void CountryStart()
    {
        PC = GetComponent<PolygonCollider2D>();
        SR = GetComponent<SpriteRenderer>();
        AM = GetComponent<Animator>();


        //SetUpRandomStartBudget();
    }
    public void SetUpRandomCrisis(int funding, float timeLeft)
    {
        //Need to account for country having enough funding already to cover it instantly
        if(currentFUNDING + funding <= -1)
        {
            StateCondition = Condition.InCrisis;
            
            deathTimer += timeLeft;
            //Set color of GUICountryFunding to grey when negative value
            GUICountryFunding.color = negativeBalance;
        }
        else //saved from crisis!
        {
            
            GUICountryFunding.color = postiveBalance;
        }

        currentFUNDING += funding; 
        GUICountryFunding.text = FundingToString();
    }
        

    void Update()
    {
        CountryUpdate();
    }
    void CountryUpdate()
    {
        
        switch (StateCondition)
        {
            case Condition.Alive:

            break;


            case Condition.InCrisis:
                TickDownToDeath();

            break;


            case Condition.Dead:

            break;
        }
    }
    void TickDownToDeath()
    {
        if(deathTimer - 1 >= 0f)
        {
            deathTimer -= 1 * Time.deltaTime;
            //GUICountryFunding.text = FundingToString();
            GUICountryCollapse.text = "Will Collapse in " + YearTimer.instance.StructureTime(deathTimer);
        }
        else
        {
            KillCountry();
        }
    }
    public void DonateFunding(int amount)
    {
        
        switch (StateCondition) 
        {
            case Condition.Alive: //Set color of GUICountryFunding to gold when positive value
                currentFUNDING += amount;
                GUICountryFunding.text = FundingToString();
            break;


            case Condition.InCrisis: 
                currentFUNDING += amount;
                GUICountryFunding.text = FundingToString();
                if(currentFUNDING >= 0)
                {
                    SavedFromCrisis();
                }
            break;


            case Condition.Dead:

            break;
        }
    }
    void SavedFromCrisis()
    {
        StateCondition = Condition.Alive;

        GUICountryFunding.color = postiveBalance;
        GUICountryCollapse.text = "";
    }
    void KillCountry()
    {
        StateCondition = Condition.Dead;
        CountryCrisisManager.instance.RemoveCountryFromList(this);
        isOutOfFunding = true;
        AM.SetTrigger("Kill");
        GUICountryFunding.text = "";
        GUICountryCollapse.text = "";
    }
    public bool canRecieveFunding()
    {
        if(isOutOfFunding)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public bool isInCrisis()
    {
        switch (StateCondition)
        {
            case Condition.Alive:
                return false;
            case Condition.InCrisis:
                return false;
            case Condition.Dead:
                return false;
        }
        Debug.Log("false");
        return false;
    }
    int EasyInt(float value) //Solution for converting to in in the same line im converting it to string
    {
        return (int)value;
    }

}
