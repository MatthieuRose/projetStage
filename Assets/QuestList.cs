using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New ItemList", menuName = "Quest/QuestList")]
public class QuestList : ScriptableObject // type de donnée pour pouvoir crée des listes de quête depuis l'éditeur
{
    public List<Quest> allQuest = new List<Quest>();
}
