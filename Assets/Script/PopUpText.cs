using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpText : MonoBehaviour
{
    public GameObject PopUpTextInteract;
    void Start()
    {
        PopUpTextInteract.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.name == "PrototypeHero")
        {
            PopUpTextInteract.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PopUpTextInteract.SetActive(false);
    }
}
