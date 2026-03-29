using UnityEngine;
using TMPro;
using System;

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
    [SerializeField] TextMeshProUGUI GUITimeLeft;
    [SerializeField] Color postiveBalance;
    [SerializeField] Color negativeBalance;
    [SerializeField] Color PositiveFillColor;
    [SerializeField] Color negativeFillColor;

    [SerializeField] float currentFUNDING; //how many coins county has
    [SerializeField] float deathTimer; //how much time until death
    bool isOutOfFunding;
    [SerializeField] SpriteRenderer borderSR;



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
    public void ToggleBorder(bool on)
    {
        if(on)
        {borderSR.color = Color.white; borderSR.sortingOrder = 5;}
        else {borderSR.color = Color.black; borderSR.sortingOrder = 0;}

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

            currentFUNDING += funding; 
            GUICountryFunding.text = FundingToString();

            
            deathTimer = timeLeft;
            //Set color of GUICountryFunding to grey when negative value
            GUICountryFunding.color = negativeBalance;

            
        }
        
        else //saved from crisis!
        {
            currentFUNDING += funding; 
            GUICountryFunding.text = FundingToString();
            GUICountryFunding.color = postiveBalance;
            CountryCrisisManager.instance.home.ForceSpawnTaxes(3); //Free coins if country saved itself
        }
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
            GUITimeLeft.text = "Will Collapse in " + YearTimer.instance.StructureTime(deathTimer);
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
        GUITimeLeft.text = "";
    }
    void KillCountry()
    {
        StateCondition = Condition.Dead;
        CountryCrisisManager.instance.RemoveCountryFromList(this);
        isOutOfFunding = true;
        AM.SetTrigger("Kill");
        GUICountryFunding.text = "";
        GUITimeLeft.text = "";
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
        if(StateCondition == Condition.InCrisis || StateCondition == Condition.Dead)
        {return true;}
        else return false;  
    }
    int EasyInt(float value) //Solution for converting to in in the same line im converting it to string
    {
        return (int)value;
    }

}
