using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameTracker : MonoBehaviour
{
    [Header("Intial attributes")]
    [SerializeField] int health = 3;
    [SerializeField] int hunger = 10;
    [SerializeField] int cleanliness = 10;
    [SerializeField] int playMeter = 10;
    [SerializeField] int day = 1;
    [SerializeField] int actionPoints = 5;

    [Header("Text UI Attributes")]
    public TMP_Text healthValueText;
    public TMP_Text hungerValueText;
    public TMP_Text cleanlinessValueText;
    public TMP_Text playMeterValueText;
    public TMP_Text dayValueText;
    public TMP_Text actionPointsValueText;




    // Start is called before the first frame update
    void Start()
    {
        healthValueText.text = health.ToString();
        hungerValueText.text = hunger.ToString();
        cleanlinessValueText.text = cleanliness.ToString();
        playMeterValueText.text = playMeter.ToString();
        dayValueText.text = day.ToString();
        actionPointsValueText.text = actionPoints.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void feedCat()
    {
        hunger++;
        hungerValueText.text = hunger.ToString();
        a_points();
    }

    public void cleanCat()
    {
        cleanliness++;
        cleanlinessValueText.text = cleanliness.ToString();
        a_points();
    }

    public void playWithCat()
    {
        playMeter++;
        playMeterValueText.text = playMeter.ToString();
        a_points();
    }

    public void changeDay()
    {
            day++;
            dayValueText.text = day.ToString();  
    }

    public void a_points()
    {

        actionPoints--;
        if (actionPoints == 0)
        {
            actionPoints = 5;
            actionPointsValueText.text = actionPoints.ToString();
            changeDay();

            return;
        }
        actionPointsValueText.text = actionPoints.ToString();
    }

    void checkStats()
    {
        //Need to know the conditions

    }

}
