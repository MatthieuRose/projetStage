
    using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Cinemachine;
using Cinemachine.Utility;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class doorScene : MonoBehaviour { // script pour changer de Scene, à attacher à un GameObject


    TextMeshProUGUI textDisplay; // GameObject Texte, fils de forgeObject dans la hierarchie
    public string Scene; // scene à charger 
    public int x; // position X du joueur après avoir chargé la scene 
    public int y; // position Y du joueur après avoir chargé la scene 
    public int z; // position Z du joueur après avoir chargé la scene
    public string textToDisplay; // texte à afficher dans textDisplay
    GameObject c; // Canvas fils de forgeObject ;
    GameObject parent; // forgeObject dans l'éditeur


    void Start() // assignement des variables
    {
        parent = GameObject.Find("forgeObject");
        c = parent.transform.GetChild(0).gameObject;

        textDisplay = c.GetComponentInChildren<TextMeshProUGUI>();

        c.SetActive(false); // désactive le canvas
    }


    // fonction pour vérifier si le joueur est dans le collider de l'objet auquel ce script est attaché.
    // affiche le texte et permet de changer de Scène
    void OnTriggerStay(Collider other)
    {
        

        if (other.tag == "Player")
        {
            textDisplay.text = textToDisplay;
            c.SetActive(true);
            if (Input.GetKey(KeyCode.T))
            {
                c.SetActive(false);
                controlCharacter.playerX = x;
                controlCharacter.playerY = y;
                controlCharacter.playerZ = z;
                controlCharacter.sceneToLoad = Scene;
                SceneManager.LoadScene("LoadingScreen");
            }
        }
        
        
    }


    private void OnTriggerExit(Collider other) // désactive le canvas quand le joueur quitte le collider de l'objet
    {
        c.SetActive(false);
    }
   
}


