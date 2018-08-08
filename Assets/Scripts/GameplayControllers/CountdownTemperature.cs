using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTemperature : MonoBehaviour
{


    public float TemperatureMax = 32f;
    public float temperatureLossRate = 15f;
    public GameObject temperatureSlider;
    public float lerpSpeed = 6f;
    [SerializeField]
    private float currentTemperatureSupply;
    private float desiredTemperatureSupply;

    void Start()
    {
        currentTemperatureSupply = TemperatureMax;  
        desiredTemperatureSupply = TemperatureMax;
        temperatureSlider.GetComponent<Slider>().maxValue = TemperatureMax;
        BeginTemperatureCountdown();
    }


    void BeginTemperatureCountdown()
    {
        StartCoroutine("TemperatureCountdown");
    }

    IEnumerator TemperatureCountdown()
    {
        while (true)
        {
            yield return new WaitForSeconds(temperatureLossRate);
            desiredTemperatureSupply--;
        }
    }

    public void Replenish()
    {
        //currentTemperatureSupply++;
        //currentTemperatureSupply = Mathf.Lerp(currentTemperatureSupply, TemperatureMax+1,0.1f);
        //desiredTemperatureSupply = TemperatureMax+1f;
        desiredTemperatureSupply ++;
        Debug.Log("replenishing temperature");
    }

    void FixedUpdate()
    {
        if (currentTemperatureSupply > TemperatureMax)
        {
            currentTemperatureSupply = TemperatureMax;
        }

        if (desiredTemperatureSupply > TemperatureMax)
        {
            desiredTemperatureSupply = TemperatureMax;
        }
        else if (currentTemperatureSupply < 0)
        {
            Debug.Log("you ded");
            StopCoroutine("TemperatureCountdown");
        }

        currentTemperatureSupply = Mathf.SmoothStep(currentTemperatureSupply, desiredTemperatureSupply, lerpSpeed * Time.deltaTime);
        temperatureSlider.GetComponent<Slider>().value = currentTemperatureSupply;

    }
}
