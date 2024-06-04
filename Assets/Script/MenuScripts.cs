using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScripts : MonoBehaviour
{
    public GameObject DeathText;
    public GameObject ContinueText;
    public GameObject[] UiStuff;

    public bool DeathSceneStarted;


    // Start is called before the first frame update
    void Start()
    {
        
        DeathSceneStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && DeathSceneStarted == false)
        {
            DeathTextOff();
            DeathSceneStarted = true;
            Camera.main.GetComponent<Camera>().backgroundColor = new Color32(84, 84, 84, 0);
        }
    }

    public void DeathTextOff()
    {
        DeathText.SetActive(false);
        ContinueText.SetActive(false);
        StartScene();
    }

    public void StartScene()
    {
        foreach (GameObject text in UiStuff)
        {
            text.SetActive(true);
        }
    }

}
