using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System.Diagnostics.Tracing;

public class YearTimer : MonoBehaviour
{
    public static YearTimer instance;
    [SerializeField] TextMeshProUGUI timePassedText;
    private float timePassed;
    private float timeRemaining;
    private float timeToWinGame = 310;

    [Header("TimeValue")]
    float SecondToMonthRatio = 0.2f;
    int MonthsPerYear = 12;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        timeRemaining = timeToWinGame;
    }
    public float getTotalTimePassed()
    {
        return timePassed;
    }

    // Update is called once per frame
    void Update()
    {
        TickUpTimer();
    }
    void TickUpTimer()
    {
        if(timePassed < timeToWinGame)
        {
            timePassed += 1 * Time.deltaTime;
            timeRemaining -= 1 * Time.deltaTime;
            StructureTimePassedText();
        }
        
    }
    public string StructureTime(float timeInSeconds)
    {
        //5 minuites is equal to 5 years
        //Thats 300 seconds for 5 years
        //12 months is 1 minuite

        //5 seconds would be 1 month
        //I need to convert every 5 seconds to 1 month
        //If more then 12 months i need to Add "1 year and...."

        int months = (int)(Mathf.FloorToInt(timeInSeconds * SecondToMonthRatio));
        int years = (int)(Mathf.FloorToInt(months/MonthsPerYear));
        int remainingMonths = months % 12;

        if(years != 0 && remainingMonths != 0)
        {
            return $"{years} Years, {remainingMonths} months";
        }
        else if(years != 0)
        {
            return $"{years} Years";
        }
        else if(remainingMonths != 0)
        {
            return $"{remainingMonths} months";
        }
        else
        {
            return "s";
        }

        
    }
    void StructureTimePassedText()
    {
        timePassedText.text = StructureTime(timeRemaining) + " remaining";
    }
}
