using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameTracker : MonoBehaviour
{
    [Header("Intial attributes")]
    [SerializeField] int health = 3;
    [SerializeField] int hunger = 10;
    [SerializeField] int cleanliness = 10;
    [SerializeField] int playMeter = 10;
    [SerializeField] int day = 1;
    [SerializeField] int actionPoints = 2;

    int randomNumberOfDaysCatGoneMissing = 0;
    int probabilityNumber = 0;

    [Header("Text UI Attributes")]
    public TMP_Text healthValueText;
    public TMP_Text hungerValueText;
    public TMP_Text cleanlinessValueText;
    public TMP_Text playMeterValueText;
    public TMP_Text dayValueText;
    public TMP_Text actionPointsValueText;
    public TMP_Text statusOfTheDayText;
    public TMP_Text narrationText;

    [Header("NarrationText")]
    [SerializeField] string hungerText;
    [SerializeField] string cleanlinessText;
    [SerializeField] string playText;

    [Space(10)]
    [Header("EventsText")]
    [SerializeField] string catGoneMissingString;
    [SerializeField] string powerCutString;
    [SerializeField] string catWantsToPlayString;

    [Header("Game Buttons")]
    public Button eatButton;
    public Button cleanButton;
    public Button playWithCatButton;
    public Button nextButton;
    public Button playAgainButton;

    [SerializeField] enum dayStatus {Morning, Afternoon, Evening};
    [SerializeField] enum eventsInTheGame {None,CatGoneMissing,PowerCut,CatWantsToPlay};

    [SerializeField] dayStatus statusOfTheDay;
    [SerializeField] eventsInTheGame events;





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
        events = eventsInTheGame.None;      
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
        narrationText.text = hungerText;
        a_points();
        checkStats();
    }

    public void cleanCat()
    {
        cleanliness++;
        cleanlinessValueText.text = cleanliness.ToString();
        narrationText.text = cleanlinessText;
        a_points();
        checkStats();
    }

    public void playWithCat()
    {
        playMeter++;
        playMeterValueText.text = playMeter.ToString();
        narrationText.text = playText;
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
        
        if (hunger < 2 || cleanliness < 2 || playMeter < 2)
        {
            health--;
            healthValueText.text = health.ToString();
        }

        if (health <= 0)
        {
            health = 0;
            healthValueText.text = health.ToString();
            narrationText.text = "Your cat has died, Sad";
            DisableButtons();
            playAgainButton.gameObject.SetActive(true);

        }

    }

    #endregion

    #region eventsRegion
    public void changeStatusOfTheDay()
    {
        statusOfTheDay = (dayStatus)((((int)statusOfTheDay) + 1) % 3);
        statusOfTheDayText.text = statusOfTheDay.ToString();
        if ((int)statusOfTheDay == 0)
        {
            day++;
            decrementStats();
            dayValueText.text = day.ToString();
            probabilityNumber = Random.Range(1, 11);
            if(probabilityNumber <= 7)
            {
                catGoesMissing();
            }
        }
    }

    void catGoesMissing()
    {
        DisableButtons();
        events = eventsInTheGame.CatGoneMissing;

        randomNumberOfDaysCatGoneMissing = Random.Range(2, 6);
        day += randomNumberOfDaysCatGoneMissing;
        dayValueText.text = day.ToString();
        narrationText.text = catGoneMissingString + " " + randomNumberOfDaysCatGoneMissing + " days. You cannot feed/play/clean your creature. Click Next to continue";
        nextButton.gameObject.SetActive(true);
    }




    #endregion

    #region ButtonEvents
    private void DisableButtons()
    {
        playWithCatButton.interactable = false;
        eatButton.interactable = false;
        cleanButton.interactable = false;
    }
    private void EnableButtons()
    {
        playWithCatButton.interactable = true;
        eatButton.interactable = true;
        cleanButton.interactable = true;
    }

    public void PlayAgain()
    {
        playAgainButton.gameObject.SetActive(false);
        EnableButtons();
        health = 3;
        hunger = 5;
        cleanliness = 5;
        playMeter = 5;
        day = 1;
        actionPoints = 2;
        healthValueText.text = health.ToString();
        hungerValueText.text = hunger.ToString();
        cleanlinessValueText.text = cleanliness.ToString();
        playMeterValueText.text = playMeter.ToString();
        dayValueText.text = day.ToString();
        statusOfTheDay = dayStatus.Morning;
        statusOfTheDayText.text = statusOfTheDay.ToString();
        events = eventsInTheGame.None;
    }

    public void onNextButtonClicked()
    {
        onNextButtonOnClicked(randomNumberOfDaysCatGoneMissing - 1);
    }

    public void onNextButtonOnClicked(int randomNumberOfDaysCatGoneMissing)
    {
        events = eventsInTheGame.None;
        nextButton.gameObject.SetActive(false);
        narrationText.text = "Your cat has comeback with reduced stats. Please take care of it";

        //Decrease Action Points
        hunger -= randomNumberOfDaysCatGoneMissing;
        cleanliness -= randomNumberOfDaysCatGoneMissing;
        playMeter -= randomNumberOfDaysCatGoneMissing;
        hungerValueText.text = hunger.ToString();
        cleanlinessValueText.text = cleanliness.ToString();
        playMeterValueText.text = playMeter.ToString();
        EnableButtons();
        checkStats();

    }
    #endregion



}
