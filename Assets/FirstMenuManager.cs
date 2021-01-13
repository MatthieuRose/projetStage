using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstMenuManager : MonoBehaviour // script pour le premier menu du jeu , se lance directement
{
    public GameObject loadOption; // bouton Load du menu principal


    [HideInInspector] // cache la variable dans l'inspecteur 
    public static Boolean newGame; // variable pour savoir s'il s'agit d'une nouvelle partie ou non

    
    void Start()
    {
        if(File.Exists(Application.persistentDataPath + "/playerStat.xml")) // on affiche le bouton si le joueur a un fichier de sauvegarde
        {
            loadOption.SetActive(true);
        }
        
    }

    public void newGameLoad() // charge une nouvelle partie
    {
        newGame = true;
        SceneManager.LoadScene("FirstLoadingScreen");
    }


    public void loadGame() // charge l'ancienne partie du joueur
    {
        newGame = false;
        SceneManager.LoadScene("FirstLoadingScreen");
    }

}
