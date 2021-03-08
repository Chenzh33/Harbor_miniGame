using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFadeInOut : MonoBehaviour 
{

    private RawImage rawImage;
    public float fadeSpeedToClear = 1.5f;
    public float fadeSpeedToBlack = 6f;
    private bool fadingToClear;
    private bool fadingToBlack;
    private bool fadingToWhite;
   
   

    public void Awake()
    {
        rawImage = GetComponentInChildren<RawImage>();
        fadingToClear = false;
        fadingToBlack = false;
        fadingToWhite = false;
    }

    private void FadeToClear()
    {
        rawImage.color = Color.Lerp(rawImage.color, Color.clear, fadeSpeedToClear * Time.deltaTime);
        if (rawImage.color.a < 0.02f)
        {
            //rawImage.color = Color.clear;
            rawImage.enabled = false;
            fadingToClear = false;
        }
    }
 
    private void FadeToBlack()
    {
        rawImage.color = Color.Lerp(rawImage.color, Color.black, fadeSpeedToBlack * Time.deltaTime);
        if (rawImage.color.a > 0.98f)
        {
            rawImage.color = Color.black;
            fadingToBlack = false;
        }
    }
 
    private void FadeToWhite()
    {
        rawImage.color = Color.Lerp(rawImage.color, Color.white, fadeSpeedToBlack * Time.deltaTime);
        if (rawImage.color.a > 0.5f)
        {
            //rawImage.color = Color.white;
            fadingToWhite = false;
        }
 
    }
    public void SetBlack()
    {
            rawImage.color = Color.black;
    }

    public void SetWhite()
    {
            rawImage.color = Color.white;
    }

    public void SetClear()
    {
            rawImage.color = Color.clear;
    }


    public void BeginFadeToClearFromBlack()
    {
        //rawImage.color = Color.black;
        fadingToClear = true;
        rawImage.enabled = true;
    }

    public void BeginFadeToClearFromWhite()
    {
        //rawImage.color = Color.white;
        fadingToClear = true;
        rawImage.enabled = true;
    }
 
    public void BeginFadeToBlack()
    {
        rawImage.color = Color.clear;
        fadingToBlack = true;
        rawImage.enabled = true;
    }
 
    public void BeginFadeToWhite()
    {
        rawImage.color = Color.clear;
        fadingToWhite = true;
        rawImage.enabled = true;
    }

    public void Start()
    {

    }
  

    public void Update()
    {
        if(fadingToClear)
            FadeToClear();
        else if (fadingToBlack)
            FadeToBlack();
        else if (fadingToWhite)
            FadeToWhite();

        
    }
   

}
