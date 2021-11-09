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
    private int lvl = 0;

    private void levelup()
    {
        lvl++;
        lvltext.GetComponent<UnityEngine.UI.Text>().text = "Lvl " + lvl.ToString();
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
        
    }

    // Update is called once per frame
    void Update()
    {
        if (slider.value < targetprogress)
        {
            slider.value += fillspeed * Time.deltaTime;
        }
        if (slider.value >= 1)
        {
            levelup();
            slider.value = 0;
            targetprogress = 0;

        }
    }
}
