using Unity.Mathematics;
using UnityEngine;

public class CountryHome : Country
{
    [SerializeField] float IncomeTimer = 3; //Counter to spawn another coin
    [SerializeField] float IncomeMult = 1; //amount multiplied to income to control how fast coins spawn
    [SerializeField] GameObject coin;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CountryStart();
    }

    // Update is called once per frame
    void Update()
    {
        TickDownGenerateTaxes();
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
            Instantiate(coin,WhereToSpawnCoin(),quaternion.identity);
        }
        
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
}
