using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using UnityEngine.UIElements;

[System.Serializable]
[CreateAssetMenu(fileName = "New Quest", menuName = "Quest/QuestGiverName")] // permet de créer les objets directement dans l'éditeur 
public class Quest : ScriptableObject  // Permet de créer des objets de type Quete 
{

    public bool isActive = false; // booléen pour vérifier si la quête est active
    public string Title; // titre de la quête
    public string description; // description de la quête
    public int experienceReward; // récompense d'expérience que donne la quête 
    public int goldReward; // récompense en OR que donne la quête
    public bool isCompleted = false ; // booléen pour vérifier si la quête est complété
    public int questID; // ID de la quête
    public bool isRead; // vérifie si la quête a était lu par le joueur ( pour l'histoire )
    static int nextQuestID = 0; // pour donner un ID différent à chaque quête
    public Unit player; // le joueur

    public QuestScript MyQuestScript; // pour le canvas du QuestLogObject
    private GameObject completeQuest; // canvas
    


    public QuestGoal goal; // objectif de la quête


  
    public Quest() // id unique pour chaque quête
    {
        
        this.questID = Interlocked.Increment(ref nextQuestID);
        
    }

    public void Complete() // lancé si une quête est complété, affiche un canvas, donne les récompenses au joueur, rend la quête inactive et la marque comme complété
    {
        isActive = false;
        isCompleted = true;
        player = GameObject.Find("Player").GetComponent<Unit>();
        player.experience += experienceReward;
        player.gold += goldReward;
        completeQuest = GameObject.Find("QuestCompleteObject").gameObject.transform.GetChild(0).gameObject;
        completeQuest.SetActive(true);
        GameObject.Find("completeTitle").GetComponent<Text>().text = Title;
        GameObject.Find("Or").GetComponent<Text>().text = goldReward.ToString();
        GameObject.Find("Exp").GetComponent<Text>().text = experienceReward.ToString();


    }


}
