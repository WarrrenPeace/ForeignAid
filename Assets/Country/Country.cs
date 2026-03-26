using NUnit.Framework;
using UnityEngine;

public class Country : MonoBehaviour
{
    public PolygonCollider2D PC;
    public SpriteRenderer SR;
    public Animator AM;

    [SerializeField] float currentFUNDING; //how many coins county has
    [SerializeField] float budgetMult = 0.25f; //amount multiplied to budget to control how fast funding is decreased
    bool isOutOfFunding;

    int FundingRangeMin = -25;
    int FundingRangeMax = 25;



    void Start()
    {
        CountryStart();
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
        currentFUNDING = Random.Range(15,26);
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
            }
            else
            {
                isOutOfFunding = true;
                KillCountry();
            }
        }
    }
    public void SupplyFunding(int amount)
    {
        currentFUNDING += amount;
    }
    void KillCountry()
    {
        AM.SetTrigger("Kill");
    }
}
