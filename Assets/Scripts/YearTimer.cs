using UnityEngine;
using TMPro;

public class YearTimer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timePassedText;
    private float timePassed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TickUpTimer();
    }
    void TickUpTimer()
    {
        timePassed += 1 * Time.deltaTime;
        StructureTimePassedText();
    }
    void StructureTimePassedText()
    {
        timePassedText.text = timePassed.ToString();
    }
}
