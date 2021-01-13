using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour // script attaché au GameObject MainMenu dans l'éditeur
{

    public void ResumeGame() // enleve la pause
    {
        Time.timeScale = 1;
    }
    public void QuitGame() // éteint le jeu
    {
        Application.Quit();
    }

    public void loadMainMenu() // retourne à l'écran principal et détruit tout les objets
    {
        SceneManager.LoadScene("MainMenu");
        foreach (GameObject o in Object.FindObjectsOfType<GameObject>())
        {
            Destroy(o);
        }
    }
}

