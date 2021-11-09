using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    private Slider slider;

    public float fillspeed = .5f;
    private float targetprogress = 0;
    public GameObject lvltext;
    public int lvl = 1;

    private void levelup()
    {
        lvl++;
        lvltext.GetComponent<UnityEngine.UI.Text>().text = "Lvl " + lvl.ToString();
    }

    public void Reset()
    {
        lvl = 1;
        lvltext.GetComponent<UnityEngine.UI.Text>().text = "Lvl " + lvl.ToString();
        targetprogress = 0;
        slider.value = 0;
    }
    private void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
    }

    public void incrementProgress(float newprogress)
    {
        targetprogress = slider.value + newprogress;
    }
    // Start is called before the first frame update
    void Start()
    {
        lvltext.GetComponent<UnityEngine.UI.Text>().text = "Lvl " + lvl.ToString();
        targetprogress = 9;
    }

    // Update is called once per frame
    void Update()
    {
        if (slider.value < targetprogress)
        {
            slider.value += fillspeed * Time.deltaTime;
        }
        if (slider.value >= 1 || targetprogress >= .9)
        {
            levelup();
            slider.value = 0;
            targetprogress = 0;

        }
    }
}
