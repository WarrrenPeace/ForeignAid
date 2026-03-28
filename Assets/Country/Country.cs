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

    [SerializeField] float currentFUNDING; //how many coins county has
    [SerializeField] float deathTimer; //how much time until death
    [SerializeField] float budgetMult = 0.25f; //amount multiplied to budget to control how fast funding is decreased
    bool isOutOfFunding;

    int FundingRangeMin = -25;
    int FundingRangeMax = 25;



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
    void SetUpRandomStartBudget()
    {
        currentFUNDING = UnityEngine.Random.Range(15,26);
        budgetMult = UnityEngine.Random.Range(0.25f,2);
        GUICountryFunding.text = FundingToString();
    }
    public void SetUpRandomCrisis(int funding, float mult, float timeLeft)
    {
        //Need to account for country having enough funding already to cover it instantly
        if(currentFUNDING + funding <= -1)
        {
            StateCondition = Condition.InCrisis;
            currentFUNDING += funding; 
            
            deathTimer = timeLeft;
            budgetMult = mult;
            GUICountryFunding.text = FundingToString();
            //Set color of GUICountryFunding to grey when negative value
        }
        else
        {
            //saved from crisis!
            Debug.Log(name + " Saved from crisis");
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
            GUICountryCollapse.text = "Will Collapse in " + EasyInt(deathTimer).ToString();
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
