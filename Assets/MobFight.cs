using System.Collections;
using System.Collections.Generic;
//using System.Runtime.Remoting.Activation;
using UnityEngine;
using Cinemachine;
using System;


public class MobFight : MonoBehaviour // script à attacher au joueur pour que quand le joueur rentre en collision avec un ennemie, un combat se lance
{
    public GameObject cam;
    public BattleSystem battleSystem;
    public GameObject enemy;
    public GameObject camFight;
    public GameObject canvasF;

    void OnTriggerEnter(Collider other) // quand le joueur rentre en collision avec l'ennemie, un combat se lance contre celui-ci
    {
        if (other.tag == "Mob")
        {
            camFight.SetActive(true);
            //Debug.Log("collide");
            enemy = other.gameObject;
            cam.SetActive(false);
            canvasF.SetActive(true);
            battleSystem.NewBattle(enemy);

        }
    }
    public void EndBattle() // quand le combat se fini, on détruit l'ennemie
    {
        camFight.SetActive(false);
        cam.SetActive(true);
        Destroy(enemy);
        canvasF.SetActive(false);
    }
}
