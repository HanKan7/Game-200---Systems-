using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class GameTracker : MonoBehaviour
{
    [Header("Intial attributes")]
    [SerializeField] int health = 5;
    [SerializeField] int hunger = 10;
    [SerializeField] int cleanliness = 10;
    [SerializeField] int playMeter = 10;
    [SerializeField] int day = 1;
    [SerializeField] int actionPoints = 2;
    [SerializeField] GameObject progressbar;

    int randomNumberOfDaysCreatureGoneMissing = 0;
    int probabilityNumber = 0;
    bool egg = true;
    int lastevent;

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
    [SerializeField] string creatureGoneMissingString;
    [SerializeField] string powerCutString;
    [SerializeField] string creatureWantsToPlayString;
    [SerializeField] string creatureDoesntWantToPlayString;

    [Header("Animation Objects")]
    [SerializeField] GameObject IdleAnimationEggGameObject;
    [SerializeField] GameObject EggDeadAnimationGameObject;
    [SerializeField] GameObject IdleAnimationAnimationGameObject;
    [SerializeField] GameObject WitchGoingOutAnimationGameObject;
    [SerializeField] GameObject WitchDeathAnimationGameObject;
    [SerializeField] GameObject FeedWitchAnimationGameObject;
    [SerializeField] GameObject CleaningWitchAnimationGameObject;
    [SerializeField] GameObject PlayingWithWitchAnimationGameObject;

    [Header("Game Buttons")]
    public Button eatButton;
    public Button cleanButton;
    public Button playWithCatButton;
    public Button nextButton;
    public Button playAgainButton;

    [SerializeField] enum dayStatus {Morning, Afternoon, Evening};
    [SerializeField] enum eventsInTheGame {None,CatGoneMissing,PowerCut,CreatureWantsToPlay, CreatureDoesntWantToPlay};

    [SerializeField] dayStatus statusOfTheDay;
    [SerializeField] eventsInTheGame events;

    public AudioService audioService;




    // Start is called before the first frame update
    void Start()
    {
        healthValueText.text = health.ToString();
        hungerValueText.text = hunger.ToString();
        cleanlinessValueText.text = cleanliness.ToString();
        playMeterValueText.text = playMeter.ToString();
        dayValueText.text = day.ToString();
        actionPointsValueText.text = actionPoints.ToString();
        narrationText.text = "You have found an egg, What a lovely day!";

        DisableAnimationGameObjects();
        IdleAnimationEggGameObject.SetActive(true);

        statusOfTheDay = dayStatus.Morning;
        events = eventsInTheGame.None;      
    }

    #region playerTraits
    public void feedCat()
    {
        audioService.PlayFood();
        hunger += 2;
        hungerValueText.text = hunger.ToString();
        narrationText.text = hungerText;
        DisableAnimationGameObjects();
        FeedWitchAnimationGameObject.SetActive(true);
        a_points();
        checkStats();
    }

    public void cleanCat()
    {
        audioService.PlayBath();
        cleanliness += 2;
        cleanlinessValueText.text = cleanliness.ToString();
        narrationText.text = cleanlinessText;
        DisableAnimationGameObjects();
        CleaningWitchAnimationGameObject.SetActive(true);
        a_points();
        checkStats();
    }

    public void playWithCat()
    {
        audioService.PlayPlay();
        playMeter += 2;
        playMeterValueText.text = playMeter.ToString();
        narrationText.text = playText;
        DisableAnimationGameObjects();
        PlayingWithWitchAnimationGameObject.SetActive(true);
        a_points();
        checkStats();
    }

    void decrementStats()
    {
        
        hunger = hunger - 2;
        cleanliness = cleanliness - 2;
        playMeter = playMeter - 2;
        hungerValueText.text = hunger.ToString();
        cleanlinessValueText.text = cleanliness.ToString();
        playMeterValueText.text = playMeter.ToString();
    }


    public void a_points()
    {

        actionPoints--;
        if (actionPoints == 0)
        {
            actionPoints = 1;
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
            narrationText.text = "Your creature has died, :(";            
            DisableButtons();
            progressbar.GetComponent<ProgressBar>().Reset();
            playAgainButton.gameObject.SetActive(true);
            DisableAnimationGameObjects();
            if(egg == true)
            {
                EggDeadAnimationGameObject.SetActive(true);
            }
            else
            {
                WitchDeathAnimationGameObject.SetActive(true);
            }  
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
            EnableButtons();
            DisableAnimationGameObjects();
            if(egg == true)
            {
                IdleAnimationEggGameObject.SetActive(true);
            }
            else
            {
                IdleAnimationAnimationGameObject.SetActive(true);
            }
            
            day++;
            decrementStats();
            progressbar.GetComponent<ProgressBar>().incrementProgress(.25f);
            if (progressbar.GetComponent<ProgressBar>().lvl == 2)
            {
                egg = false;
                IdleAnimationEggGameObject.SetActive(false);
                IdleAnimationAnimationGameObject.SetActive(true);
                narrationText.text = "Your egg has hatched!";
            }
            dayValueText.text = day.ToString();

            int whichEventToOccur = Random.Range(1, 4);
            
            if(whichEventToOccur == 1 && egg == false && lastevent != whichEventToOccur) // Creature Goes Missing
            {
                probabilityNumber = Random.Range(1, 101);
                if (probabilityNumber <= 30)
                {
                    creatureGoesMissing();
                }
            }

            else if(whichEventToOccur == 2 && egg == false && lastevent != whichEventToOccur) //Power Cut
            {
                probabilityNumber = Random.Range(1, 101);
                if (probabilityNumber <= 70)
                {
                    powerCut();
                }
            }

            else if(whichEventToOccur == 3 && egg == false && lastevent != whichEventToOccur) //Creature Only Wants To Play
            {
                probabilityNumber = Random.Range(1, 101);
                if (probabilityNumber <= 70)
                {
                    creatureOnlyWantsToPlay();
                }
            }

            else if(whichEventToOccur == 4 && egg == false && lastevent != whichEventToOccur)
            {
                probabilityNumber = Random.Range(1, 101);
                if (probabilityNumber <= 70)
                {
                    creatureDoesNotWantToPlay();
                }
            }

            lastevent = whichEventToOccur;
            
        }
    }

    void creatureGoesMissing()
    {
        DisableButtons();
        DisableAnimationGameObjects();
        WitchGoingOutAnimationGameObject.SetActive(true);
        events = eventsInTheGame.CatGoneMissing;

        randomNumberOfDaysCreatureGoneMissing = Random.Range(1, 3);
        day += randomNumberOfDaysCreatureGoneMissing;
        dayValueText.text = day.ToString();
        narrationText.text = creatureGoneMissingString + " " + randomNumberOfDaysCreatureGoneMissing + " days. You were unable to feed, play, or clean your creature. Click Next to continue";
        nextButton.gameObject.SetActive(true);
    }

    void powerCut()
    {
        cleanButton.interactable = false;
        events = eventsInTheGame.PowerCut;
        narrationText.text = powerCutString;
    }

    void creatureOnlyWantsToPlay()
    {
        cleanButton.interactable = false;
        eatButton.interactable = false;
        events = eventsInTheGame.CreatureWantsToPlay;
        narrationText.text = creatureWantsToPlayString;
    }


    void creatureDoesNotWantToPlay()
    {
        playAgainButton.interactable = false;
        events = eventsInTheGame.CreatureDoesntWantToPlay;
        narrationText.text = creatureDoesntWantToPlayString;
    }

    #endregion

    #region ButtonAndAnimationEvents
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

    private void DisableAnimationGameObjects()
    {
        IdleAnimationEggGameObject.SetActive(false);
        EggDeadAnimationGameObject.SetActive(false);
        IdleAnimationAnimationGameObject.SetActive(false);
        WitchGoingOutAnimationGameObject.SetActive(false); 
        WitchDeathAnimationGameObject.SetActive(false); 
        FeedWitchAnimationGameObject.SetActive(false); 
        CleaningWitchAnimationGameObject.SetActive(false); 
        PlayingWithWitchAnimationGameObject.SetActive(false); 
    }

    public void PlayAgain()
    {
        playAgainButton.gameObject.SetActive(false);
        narrationText.text = "";
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
        DisableAnimationGameObjects();
        IdleAnimationEggGameObject.SetActive(true);
        egg = true;
        events = eventsInTheGame.None;
    }

    public void onNextButtonClicked()
    {
        onNextButtonOnClicked(randomNumberOfDaysCreatureGoneMissing - 1);
    }

    public void onNextButtonOnClicked(int randomNumberOfDaysCatGoneMissing)
    {
        events = eventsInTheGame.None;
        DisableAnimationGameObjects();
        IdleAnimationAnimationGameObject.SetActive(true);
        nextButton.gameObject.SetActive(false);
        narrationText.text = "Your creature has comeback with reduced stats. Please take care of it";

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
