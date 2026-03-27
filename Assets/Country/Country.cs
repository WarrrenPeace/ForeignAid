using UnityEngine;
using TMPro;
using System;

public class Country : MonoBehaviour
{
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
        CountryStart();
        SetUPGUI();
        
    }
    public void SetUPGUI()
    {
        //CountryUI = Instantiate(CountryUIPrefab,GameObject.FindGameObjectWithTag("WorldCanvas").transform);
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


        SetUpRandomStartBudget();
    }
    void SetUpRandomStartBudget()
    {
        currentFUNDING = UnityEngine.Random.Range(15,26);
        budgetMult = UnityEngine.Random.Range(0.25f,2);
        GUICountryFunding.text = FundingToString();
    }

    // Update is called once per frame
    void Update()
    {
        CountryUpdate();
    }
    void CountryUpdate()
    {
        TickDownToSpendFunding();
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
                isOutOfFunding = true;
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

}
