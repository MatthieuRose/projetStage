using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using System;

public class QuestGiver : MonoBehaviour // script à attacher à un gameObject. Permet de proposer des quêtes au joueur
{
    public Quest[] quest; // liste de quête que l'objet peut proposer 

    Quest actualQuest; // variable pour savoir quel quête le joueur peut accepter 
    controlCharacter player; // joueur 
    CinemachineFreeLook cam; // camera du joueur 
    GameObject questCanvas; // QuestCanvas dans l'éditeur
    public GameObject questPrefab; // Prefab d'une quête, à assigner dans l'éditeur 
    GameObject questLogParent; // Canvas QuestLog dans l'éditeur
    Transform questParent; // canvas questParent dans l'éditeur.
    GameObject parent; // QuestObject dans l'éditeur

    Text titleText; // GameObject de type texte pour le titre
    Text descriptionText; // GameObject de type texte pour la description 
     Text experienceText;// GameObject de type texte pour la l'expérience 
    Text goldText; // GameObject de type texte pour l'argent 
    Button but; // bouton pour accepter la quête



    private void Start() // assignement des variables 
    {
        questLogParent = GameObject.Find("QuestLogObject");
        questLogParent.transform.GetChild(0).gameObject.SetActive(true);
        parent = GameObject.Find("QuestObject");
        questCanvas = parent.transform.GetChild(0).gameObject;
        questCanvas.SetActive(true);
        titleText = GameObject.Find("QuestTitle").GetComponent<Text>();

        descriptionText = GameObject.Find("QuestDescrip").GetComponent<Text>();
        experienceText = GameObject.Find("QuestExp").GetComponent<Text>();
        goldText = GameObject.Find("QuestGold").GetComponent<Text>();
        questCanvas.SetActive(false);
        but = questCanvas.GetComponentInChildren<Button>();
        but.onClick.AddListener(AcceptQuest); // ajout d'un eventListener au bouton ( il lancera la fonction AcceptQuest )
        questParent = GameObject.Find("QuestArea").transform;

        cam = GameObject.Find("Player").GetComponentInChildren<CinemachineFreeLook>(); 
        player = GameObject.Find("Player").GetComponent<controlCharacter>();
        questCanvas.SetActive(false);
        questLogParent.transform.GetChild(0).gameObject.SetActive(false);


        for (int i = 0; i < quest.Length; i++) // on réinitialiste les valeurs des quêtes que l'objet n'a pas encore proposées
        {
            if (!controlCharacter.activeQuest.Contains(quest[i]))
            {

                quest[i].isActive = false;
                quest[i].isCompleted = false;
                quest[i].goal.currentAmount = 0;
                quest[i].isRead = false;
            }
        }

    }
    public bool whatQuest() // permet de savoir quelle quête l'objet n'a pas encore proposée
    {
        
        for (int i = 0; i < quest.Length; i++)
        {
           
            if (!controlCharacter.activeQuest.Contains(quest[i]))
            {
                this.actualQuest = quest[i];
                return true;
            }
        }
        return false;
    }

    
    void OnTriggerStay(Collider other) // permet d'afficher le canvas et selectionner la quête si le joueur appuie sur T dans le collider de l'objet
    {

        if (other.tag == "Player")
        {
            if (Input.GetKey(KeyCode.T))
            {


                whatQuest();
                cam.enabled = false;
                enableCanvas();
            }
        }
    }

    private void OnTriggerExit(Collider other) // fait disparaitre le canvas et réactive la camera si le joueur quitte le collider de l'objet
    {
        if (other.tag == "Player")
        {
            cam.enabled = true;
            questCanvas.SetActive(false);
        }
    }

    public void enableCanvas() // active le canvas et assigne les textes au description de la quête
    {
        if (!whatQuest())
        {
            Debug.Log(" je n'ai plus de quète pour toi ");
        }
        else
        {
            questCanvas.SetActive(true);
            titleText.text = actualQuest.Title;
            descriptionText.text = actualQuest.description;
            experienceText.text = actualQuest.experienceReward.ToString() + "xp";
            goldText.text = actualQuest.goldReward.ToString() + "g";
        }
    }


    public void AcceptQuest() // ajoute la quête à la liste des quêtes du joueur, désactive le canvas
    {
        GameObject go = Instantiate(questPrefab, questParent);
        go.GetComponent<Text>().text = actualQuest.Title; // ajoute à la liste des quêtes du joueur visible en jeux depuis le menu

        questCanvas.SetActive(false);
        controlCharacter.activeQuest.Add(actualQuest);
        QuestScript qs = go.GetComponent<QuestScript>();
        actualQuest.MyQuestScript = qs;
        qs.buttonQuest = actualQuest;

    }

}
