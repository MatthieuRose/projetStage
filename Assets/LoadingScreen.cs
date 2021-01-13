using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour // script durant le chargement de Scene ( se lance durant la Scene LoadingScreen )
{
    void Start() // positionne le joueur
    {
        Time.timeScale = 1;
        GameObject.Find("Player").gameObject.GetComponent<controlCharacter>().enabled = false;
        GameObject.Find("Player").transform.position = new Vector3(controlCharacter.playerX, controlCharacter.playerY, controlCharacter.playerZ);
        StartCoroutine(ExampleCoroutine());
    }

    IEnumerator ExampleCoroutine() // charge la nouvelle scene après 1 seconde
    {
            yield return new WaitForSeconds(1);

            SceneManager.LoadScene(controlCharacter.sceneToLoad);
            GameObject.Find("Player").gameObject.GetComponent<controlCharacter>().enabled = true;


    }
}
