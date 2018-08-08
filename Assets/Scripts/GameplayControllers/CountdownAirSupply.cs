using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownAirSupply : MonoBehaviour
{


    public float airSupplyMax = 32f;
    public float airLossRate = 1f;
    public GameObject airSlider;
    public float lerpSpeed = 6f;
    [SerializeField]
    private float currentAirSupply;
    private float desiredAirSupply;

    void Start()
    {
        currentAirSupply = airSupplyMax;  
        desiredAirSupply = airSupplyMax;
        airSlider.GetComponent<Slider>().maxValue = airSupplyMax;
        BeginAirCountdown();
    }


    void BeginAirCountdown()
    {
        StartCoroutine("AirCountdown");
    }

    IEnumerator AirCountdown()
    {
        while (true)
        {
            yield return new WaitForSeconds(airLossRate);
            desiredAirSupply--;
        }
    }

    public void Replenish()
    {
        //currentAirSupply++;
        //currentAirSupply = Mathf.Lerp(currentAirSupply, airSupplyMax+1,0.1f);
        //desiredAirSupply = airSupplyMax+1f;
        desiredAirSupply ++;
        Debug.Log("replenishing air");
    }

    void FixedUpdate()
    {
        if (currentAirSupply > airSupplyMax)
        {
            currentAirSupply = airSupplyMax;
        }

        if (desiredAirSupply > airSupplyMax)
        {
            desiredAirSupply = airSupplyMax;
        }
        else if (currentAirSupply < 0)
        {
            Debug.Log("you ded");
            StopCoroutine("AirCountdown");
        }


        //desiredAirSupply;

//        currentAirSupply = Mathf.Lerp(currentAirSupply, desiredAirSupply, 0.25f * Time.deltaTime);
        //        currentAirSupply = Mathf.MoveTowards(currentAirSupply, desiredAirSupply, 5f * Time.deltaTime);
        currentAirSupply = Mathf.SmoothStep(currentAirSupply, desiredAirSupply, lerpSpeed * Time.deltaTime);
        airSlider.GetComponent<Slider>().value = currentAirSupply;


    }
}
