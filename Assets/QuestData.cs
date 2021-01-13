using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestData // type de donnée pour sauvegarder les quêtes
{
    public List<int> id = new List<int>(); // liste des ID des quêtes du joueur
    public List<bool> active = new List<bool>(); // liste de booléens pour savoir si les quêtes du joueur sont active
    public List<bool> complete = new List<bool>(); // liste de booléens pour savoir si les quêtes du joueur sont complétées
    public List<bool> read = new List<bool>(); // liste de booléens pour savoir si les quêtes du joueur sont lu ( pour l'histoire )
    public List<int> currentAmount = new List<int>(); // liste d'entier pour savoir l'avancement du joueur dans sa tête


    public QuestData(List<Quest> q) // sauvegarde toutes les quêtes du joueur dans une variable de type " QuestData "
    {

        foreach (var item in q)
        {
            id.Add(item.questID);
            active.Add(item.isActive);
            complete.Add(item.isCompleted);
            read.Add(item.isRead);
            currentAmount.Add(item.goal.currentAmount);

        }
    }
}
