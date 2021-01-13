using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class QuestGoal // objectif d'une quête

{

    public GoalType goalType; // type de mission ( recolte, meurtre )
    public int requiredAmount; // nombre de la ressource / enemie qu'il faut récolter
    public int currentAmount; // nombre actuel de la ressource/ enemie qu'a récupéré / tué le joueur
    public string enemyName; // nom de la ressource / de l'ennemie que doit tuer le joueur

    public bool isReached() // vérifie si le joueur a atteint le but de la quete 
    {
        return requiredAmount <= currentAmount;
    }


    public void enemyKilled(string name) // permet d'incrémenter le nombre actuel du joueur si l'enemie tué est le bon
    {
        if (goalType == GoalType.kill && enemyName.Equals(name))
        {
            currentAmount++;
        }
    }

   
}


public enum GoalType // type de quête 
{
    kill,gathering
}