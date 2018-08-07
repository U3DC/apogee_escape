using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTemperature : MonoBehaviour
{

    public float coldSteps = 32f;
    public float coldLossRate = 15f;
    public GameObject coldSlider;


    void Start()
    {
        BeginColdCountdown();
        coldSlider.GetComponent<Slider>().maxValue = coldSteps;

    }

    void BeginColdCountdown()
    {
        StartCoroutine("ColdCountdown");
    }

    IEnumerator ColdCountdown()
    {
        while (true)
        {
            yield return new WaitForSeconds(coldLossRate);
            coldSteps = coldSteps - 1;
            coldSlider.GetComponent<Slider>().value = coldSteps;
        }
    }

}
