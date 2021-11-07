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
    [SerializeField] int actionPoints = 2;

    [Header("Text UI Attributes")]
    public TMP_Text healthValueText;
    public TMP_Text hungerValueText;
    public TMP_Text cleanlinessValueText;
    public TMP_Text playMeterValueText;
    public TMP_Text dayValueText;
    public TMP_Text actionPointsValueText;
    public TMP_Text statusOfTheDayText;

    [SerializeField] enum dayStatus {Morning, Afternoon, Evening};
    [SerializeField] dayStatus statusOfTheDay;




    // Start is called before the first frame update
    void Start()
    {
        healthValueText.text = health.ToString();
        hungerValueText.text = hunger.ToString();
        cleanlinessValueText.text = cleanliness.ToString();
        playMeterValueText.text = playMeter.ToString();
        dayValueText.text = day.ToString();
        actionPointsValueText.text = actionPoints.ToString();

        statusOfTheDay = dayStatus.Morning;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region playerTraits
    public void feedCat()
    {
        hunger++;
        hungerValueText.text = hunger.ToString();
        a_points();
        checkStats();
    }

    public void cleanCat()
    {
        cleanliness++;
        cleanlinessValueText.text = cleanliness.ToString();
        a_points();
        checkStats();
    }

    public void playWithCat()
    {
        playMeter++;
        playMeterValueText.text = playMeter.ToString();
        a_points();
        checkStats();
    }

    void decrementStats()
    {
        hunger--;
        cleanliness--;
        playMeter--;
        hungerValueText.text = hunger.ToString();
        cleanlinessValueText.text = cleanliness.ToString();
        playMeterValueText.text = playMeter.ToString();
    }

    #endregion


    public void changeStatusOfTheDay()
    {
        statusOfTheDay = (dayStatus)((((int)statusOfTheDay) + 1) % 3);
        statusOfTheDayText.text = statusOfTheDay.ToString();
        if((int)statusOfTheDay == 0)
        {
            day++;
            decrementStats();
            dayValueText.text = day.ToString();
        }
    }

    public void a_points()
    {

        actionPoints--;
        if (actionPoints == 0)
        {
            actionPoints = 2;
            actionPointsValueText.text = actionPoints.ToString();
            changeStatusOfTheDay();

            return;
        }
        actionPointsValueText.text = actionPoints.ToString();
    }

    void checkStats()
    {
        if(hunger < 2 || cleanliness < 2 || playMeter < 2)
        {
            health--;
            healthValueText.text = health.ToString();
        }

    }

}
