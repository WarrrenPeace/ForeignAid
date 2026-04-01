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
    [SerializeField] GameObject OfficePrefab;
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
    bool OnCrisisCooldown = false;
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


        //SpawnStructures(5);
    }
    void SpawnStructures(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(OfficePrefab,WhereToSpawnObjectInCountry(),Quaternion.identity);
        }
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
            CountryCrisisManager.instance.home.ForceSpawnTaxes(7); //Free coins if country saved itself
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
    public void ConsumeFunding(int amount)
    {
        if(currentFUNDING - amount >= 0)
        {
            currentFUNDING -= amount;
        }
        else
        {
            Debug.Log("not enough");
        }
        
    }
    public bool HasEnoughFunding(int amount)
    {
        if(currentFUNDING - amount >= 0)
        {return true;}
        else
        return false;
    }
    void SavedFromCrisis()
    {
        StateCondition = Condition.Alive;

        GUICountryFunding.color = postiveBalance;
        GUITimeLeft.text = "";

        CountryCrisisManager.instance.home.ForceSpawnTaxes(4); //Free coins after saving country

        OnCrisisCooldown = true;
        Invoke("CrisisCooldown", 8);
    }
    void CrisisCooldown() //This is to stop country from going into crisis right after being fixed
    {
        OnCrisisCooldown = false;
    }
    void KillCountry()
    {
        StateCondition = Condition.Dead;
        GameStateManager.instance.OnCountryDeath();
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
        if((StateCondition == Condition.InCrisis || StateCondition == Condition.Dead) || OnCrisisCooldown)
        {return true;}
        else return false;  
    }
    int EasyInt(float value) //Solution for converting to in in the same line im converting it to string
    {
        return (int)value;
    }




    Vector2 WhereToSpawnObjectInCountry()
    {
        Vector2 randomPoint;
        float margin = 0.35f;
        do {
        randomPoint = new Vector3(
            UnityEngine.Random.Range(PC.bounds.min.x + margin, PC.bounds.max.x - margin), //WOWOWOWOWWW WOW THIS CRASHES GAME IF I ADD TO MIN MAX... HOW?
            UnityEngine.Random.Range(PC.bounds.min.y + margin, PC.bounds.max.y - margin),
            UnityEngine.Random.Range(PC.bounds.min.z, PC.bounds.max.z)
        );
    } while (!PC.OverlapPoint(randomPoint));

    return randomPoint;
    }

}
