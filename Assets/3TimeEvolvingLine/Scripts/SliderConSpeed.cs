using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class SliderConSpeed : MonoBehaviour
{

    public Slider slider;

    // Use this for initialization
    void Awake()
    {
       ChangeSpeed();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeSpeed()
    {
        slider.minValue = 0.03f;
        slider.maxValue = 0.3f;
        Time.timeScale = slider.value;
        
    }
}

