using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestLog : MonoBehaviour // partie gauche du canvas fils de QuestLogObject, permet d'afficher la liste des quêtes
{

    private Quest selected; // quête sur lequel le joueur a cliqué
    public static QuestLog instance; // instance singleton de la classe
    public Text questDescription; // GameObject de type texte pour afficher la description de la quête

    private void Awake() // création du singleton
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }

    public void description(Quest q) // permet d'afficher la description de la quête sur laquelle le joueur a cliqué
    {
        if(selected != null) // si le joueur a déjà cliqué sur une quête, cela désélectionne l'ancienne quête et selectionne la nouvelle
        {
            selected.MyQuestScript.Deselect();
        }

        selected = q;
        string title = q.Title;
        questDescription.text = string.Format("<size=20>{0}</size>\n\n{1}\n\nnombre de {2} déjà tué :{3}\n\nrécompense :\n\ngold : {4}    experience : {5} \n\n<size=20>Quete complétée : {6}</size>",title,q.description, q.goal.enemyName,q.goal.currentAmount,q.goldReward,q.experienceReward,q.isCompleted) ;
    }
}
