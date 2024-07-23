using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SliderBar : MonoBehaviour
{
    public Slider sliderbar;

    public void setSlider(int amount)
    {
        sliderbar.value = amount;
    }
    public void setmaxSlider(int amount)
    {
        sliderbar.maxValue = amount;
        setSlider(amount);
    }
}
