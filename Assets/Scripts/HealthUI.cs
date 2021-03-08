using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Text healthGuide;
    public Text healthMaster;
    public LivingEntity entityGuide;
    public LivingEntity entityMaster;
    public Image hpBar;

    public void Awake()
    {

        healthGuide.enabled = false;
        healthMaster.enabled = false;
    }
    

    void Update () 
	{
        float currHealthMaster = entityMaster.GetHealth();
        float maxHealthMaster = entityMaster.GetMaxHealth();
        //float currHealthMaster = entityMaster.GetHealth();
        //healthGuide.text = "Guide HP " + currHealthGuide.ToString();
        //healthMaster.text = "Master HP " + currHealthMaster.ToString();
        hpBar.fillAmount = (float)currHealthMaster / maxHealthMaster;
       
    }

}
