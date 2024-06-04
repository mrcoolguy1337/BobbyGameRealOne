using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public static ButtonScript Instance;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }
    public void StartBTN()
    {
        SceneManager.LoadScene("LevelOne");
    }
    public void StartLVL2BTN()
    {
        SceneManager.LoadScene("LevelTwo");
    }
    public void SettingsBTN()
    {
        SceneManager.LoadScene("Settings");
    }
    public void MainMenuBTN()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
