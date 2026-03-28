using UnityEngine;
using TMPro;
using System;
using System.Diagnostics;

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

    [SerializeField] float currentFUNDING; //how many coins county has
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
    public void SetUpRandomCrisis(int funding, float mult)
    {
        StateCondition = Condition.InCrisis;
        currentFUNDING -= funding;
        budgetMult = mult;
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
            TickDownToSpendFunding();

            break;


            case Condition.Dead:

            break;
        }
    }
    void TickDownToSpendFunding()
    {
        if(!isOutOfFunding)
        {
            if(currentFUNDING - 1 >= FundingRangeMin)
            {
                currentFUNDING -= 1 * budgetMult * Time.deltaTime;
                GUICountryFunding.text = FundingToString();
            }
            else
            {
                KillCountry();
            }
        }
    }
    public void DonateFunding(int amount)
    {
        currentFUNDING += amount;
    }
    void KillCountry()
    {
        StateCondition = Condition.Dead;
        isOutOfFunding = true;
        AM.SetTrigger("Kill");
        GUICountryFunding.text = "";
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
        if(StateCondition == Condition.InCrisis)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
