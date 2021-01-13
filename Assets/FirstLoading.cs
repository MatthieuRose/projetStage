using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstLoading : MonoBehaviour // se lance dans la scene FirstLoadingScreen, sert à soit lancer une nouvelle partie, soit charger la partie du joueur
{
    public controlCharacter player; // variable du joueur, à mettre depuis l'éditeur 
    
    void Start()
    {

        if (FirstMenuManager.newGame == false) // recharge les données du joueur
        {
            player.LoadPlayer();
            player.LoadQuest();
            player.LoadPlayerStat();
            player.LoadEquipment();
        }
        else // lance une nouvelle partie 
        {
            controlCharacter.playerX = 0;
            controlCharacter.playerY = 0;
            controlCharacter.playerZ = -5;
            controlCharacter.sceneToLoad = "Forge";
            SceneManager.LoadScene("LoadingScreen");
        }
        
    }
}
