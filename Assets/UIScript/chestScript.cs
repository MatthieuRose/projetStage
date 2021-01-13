using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chestScript : interactable
{   private bool isOpen = false ;
    public override void OnInteract()
    {
        InteractText = " Press F to";
        isOpen = !isOpen;
        Debug.Log("bonjour");

    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if(Input.GetKey(KeyCode.Z)) {   
                Debug.Log("Fonctionne");
            }
            if (Input.GetKey(KeyCode.T))
            {
                Debug.Log("T");
            }
        }
    }
}
