using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public static UIScript instance;
    public GameObject StartText;
    public GameObject BoxText;
    public GameObject DoorText;
    public GameObject JumpText;

    public GameObject GuideController;
    public GameObject GuidingText;
    public GameObject GuidingText2;
    public GameObject GuideText3;

    public GameObject Box;
    public GameObject Controller;
    public GameObject TransparentBox;
    public GameObject JumpController;

    public GameObject KeyText;
    public GameObject SpikeText;

    public GameObject JumpBoostText;


    public bool Started;


    void Start()
    {
        
        Time.timeScale = 0;
        instance = this;
        BoxText.SetActive(false);
        DoorText.SetActive(false);
        Started = false;
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && Started == false)
        {
            Time.timeScale = 1;
            StartText.SetActive(false);
            BoxText.SetActive(true);
            DoorText.SetActive(true);
            UIScript.instance.BoxTestOn();
            Started = true;
        }

    }

    public void DoorTextOff()
    {
        DoorText.SetActive(false);
    }
    public void DoorTextOn()
    {
        DoorText.SetActive(true);
    }
    public void BoxTextOn()
    {
        BoxText.SetActive(true);
    }
    public void BoxTextOff()
    {
        BoxText.SetActive(false);
    }
    public void BoxTestOn()
    {
        Box.SetActive(true);
        TransparentBox.SetActive(true);
        Controller.SetActive(true);
    }
    public void JumpTextOff()
    {
        JumpText.SetActive(false);
        JumpController.SetActive(false);
    }
    public void GuideTextOn()
    {
        GuidingText.SetActive(true);
        GuidingText2.SetActive(true);
    }
    public void GuideTextOff()
    {
        GuideController.SetActive(false);
        GuidingText.SetActive(false);
        GuidingText2.SetActive(false);
    }
    public void GuideText3On()
    {
        GuideText3.SetActive(true);
    }
    public void KeyTextOff()
    {
        Destroy(KeyText);
        SpikeText.SetActive(false);
    }
    public void JumpBoostTextOff()
    {
        JumpBoostText.SetActive(false);
    }
}
