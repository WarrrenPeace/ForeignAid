using Unity.Mathematics;
using UnityEngine;
using TMPro;

public class CountryHome : Country
{
    [SerializeField] float IncomeTimer = 3; //Counter to spawn another coin
    [SerializeField] float IncomeMult = 1; //amount multiplied to income to control how fast coins spawn
    [SerializeField] GameObject coin;
    [SerializeField] GameObject koin;
    [SerializeField] int amountOfCoinsToSpawn = 1;

    [SerializeField] float InvestTimer = 5; //Counter to spawn another coin
    [SerializeField] TextMeshProUGUI GUIIncomeMult;

    void Start()
    {
        CountryCrisisManager.instance.AssignHomeCountry(this);
        CountryStart();
        SetUPGUI();
    }

    // Update is called once per frame
    void Update()
    {
        TickDownGenerateTaxes();
        TickDownInvestCoin();
    }
    void TickDownGenerateTaxes()
    {
        if(IncomeTimer - 1 >= 0)
        {
            IncomeTimer -= 1 * IncomeMult * Time.deltaTime;
        }
        else
        {
            IncomeTimer = 3;
            for (int i = 0; i < amountOfCoinsToSpawn; i++)
            {
                SpawnCoin();
            }
            
        }
        
    }
    public void ForceSpawnTaxes(int amount)
    {
        for (int i = 0; i < amount; i++)
            {
                SpawnCoin();
            }
    }
    void SpawnCoin()
    {
        if(UnityEngine.Random.Range(0,101) >= 90)
        {
            Instantiate(koin,WhereToSpawnCoin(),quaternion.identity);
        }
        else
        {Instantiate(coin,WhereToSpawnCoin(),quaternion.identity);}
    }
    Vector2 WhereToSpawnCoin()
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

    void TickDownInvestCoin() //Runs on its own timer for consistancy
    {
        if(HasEnoughFunding(1)) //If has atleast 1 coin it will count to 5 and convert it to mult
        {
            if(InvestTimer - 1 >= 0)
            {
                InvestTimer -= 1 * Time.deltaTime;
            }
            else
            {
                InvestTimer = 5;
                InvestCoinIntoMult();

            }
        }
        
    }
    //Investment Logic
    void InvestCoinIntoMult() //Call this to convert a coin into 0.05 added mult;
    {
        if(HasEnoughFunding(1))
        {
            ConsumeFunding(1);
            IncomeMult = IncomeMult + 0.05f;
            GUIIncomeMult.text = "IncomeMult = " + IncomeMult.ToString();
        }
        
    }
}
