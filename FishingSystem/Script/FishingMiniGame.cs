using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishingMiniGame : MonoBehaviour
{
    public RectTransform FishTransform;
    public RectTransform catcherTransform;
    public bool isFishOverlapping;

    public Slider successSlider;
    float successIncrement = 15;
    float failDecrement = 12;
    float successThreshold = 100;
    float failThreshold = -100;
    float successCounter = 0;
    private void Update()
    {
        if(CheckOverLapping(FishTransform,catcherTransform))
        {
            isFishOverlapping = true;
        }
        else
        {
            isFishOverlapping= false;
        }
        OverlappingCalculation();
    }

    private void OverlappingCalculation()
    {
       if(isFishOverlapping)
        {
            successCounter += successIncrement * Time.deltaTime;

        }
        else
        {
            successCounter-= failDecrement * Time.deltaTime;
        }
        successCounter = Mathf.Clamp(successCounter, failThreshold, successThreshold);

        successSlider.value=successCounter;
        
        if(successCounter>=successThreshold)
        {
            
            FishingSystem.instance.EndMiniGame(true);
            successCounter = 0;
            successSlider.value = 0;
        }
        else if(successCounter<=failThreshold)
        {
            
            FishingSystem.instance.EndMiniGame(false);
            successCounter = 0;
            successSlider.value = 0;
        }
    }

    private bool CheckOverLapping(RectTransform fishTransform, RectTransform catcherTransform)
    {
        Rect r1 = new Rect(fishTransform.position.x, fishTransform.position.y, fishTransform.rect.width, fishTransform.rect.height);
        Rect r2 = new Rect(catcherTransform.position.x, catcherTransform.position.y, catcherTransform.rect.width, catcherTransform.rect.height);
        return r1.Overlaps(r2);
    }
}
