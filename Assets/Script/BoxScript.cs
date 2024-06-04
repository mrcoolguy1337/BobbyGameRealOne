using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    public static BoxScript instance;
    [SerializeField]
    public GameObject Door;
    public GameObject Controller;
    public GameObject Key;
    public void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "BoxEnd")
        {
            Door.SetActive(false);
            Controller.SetActive(false);
            UIScript.instance.DoorTextOff();
            UIScript.instance.BoxTextOff();
            UIScript.instance.GuideTextOn();
        }
        if (collision.gameObject.name == "Controller2")
        {
            Key.SetActive(true);
            Controller.SetActive(false);
        }
    }
}
