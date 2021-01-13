using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData // type de donnée pour stocker les données du joueur lors de la sauvegarde 
{ 
    public string stage; // Scene dans laquelle se trouve le joueur
    public int health; // point de vie du joueur 
    public float[] position; // position du joueur ( son transform )
    public int gold; // or du joueur 
    public int experience; // expérience du joueur 


    public PlayerData(Unit player, string scene)
    {

        stage = scene;
        health = player.currentHP;
        gold = player.gold;
        experience = player.experience;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;

    }
}
