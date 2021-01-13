using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class forgeQuestDialogue : MonoBehaviour // script à ajouter à un GameObject pour pouvoir rajouter des dialogues à l'histoire 
{
    public QuestList allquest; // Liste de quête à mettre dans l'éditeur pour savoir l'avancement dans l'histoire du joueur
    private int i = -1; // index du dialogue 
    public TextMeshProUGUI textDisplay; // texte à afficher
    public sentenceList[] sentences; // liste de dialogue 
    private int index; // index de la lettre affichée du dialogue
    public float typingSpeed; // vitesse d'écriture du dialogue ( à mettre dans l'éditeur )
    Button continueButton; // bouton continuer du canvas 
    GameObject c; // canvas du Dialogue
    GameObject parent; // empty object DialogueObject
    CinemachineFreeLook cam; // camera du joueur 
    public Boolean needClose; // booléen pour savoir si le dialogue doit être joué quand le joueur est prêt .
    Boolean started = false;





    void startCoroutine()
    {
        c.SetActive(true);
        cam.enabled = false;
        StartCoroutine(TypeQuest());
    }


    void Start() // assigne les variables 
    {
        cam = GameObject.Find("Player").GetComponentInChildren<CinemachineFreeLook>();
        parent = GameObject.Find("DialogueObject");
        c = parent.transform.GetChild(0).gameObject;
        continueButton = c.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.GetComponent<Button>();
        continueButton.onClick.AddListener(NextSentence);
        textDisplay = c.GetComponentInChildren<TextMeshProUGUI>();
        c.SetActive(false); // désactive le canvas
        if (!needClose)
        {
            startStory();
        }
        
    }


    void startStory()
    {
        Debug.Log(controlCharacter.activeQuest.Count);
        foreach (var q in controlCharacter.activeQuest) // on vérifie les quêtes du joueur pour savoir quel dialogue jouer
        {
            
            foreach (var v in allquest.allQuest)
            {
                Debug.Log(q.Title + " " + v.Title);
                if (q.Title.Equals(v.Title))
                {
                    Debug.Log(q.isCompleted + " " + q.isRead);

                    if (q.isCompleted && !q.isRead)
                    {
                        i = allquest.allQuest.IndexOf(v)+1;
                        q.isRead = true;
                        startCoroutine();
                        return;
                    }
                }

            }
        }
        i = 0;
        startCoroutine();
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && needClose)
        {
            textDisplay.text = "";
            started = true;
            startStory();
            
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && needClose)
        {
            cam.enabled = true;
            index = 0;
            c.SetActive(false);
            continueButton.gameObject.SetActive(false);
            StopCoroutine(TypeQuest());
            textDisplay.text = "";
            started = false;

        }
    }
    void Update()
    {
        if (started || !needClose) { 

        if (textDisplay.text == sentences[i].sentence[index])
        {
            continueButton.gameObject.SetActive(true);
        }
        }
    }
    IEnumerator TypeQuest()
    {
        foreach (char letter in sentences[i].sentence[index].ToCharArray()) // affiche les lettres une par une à une vitesse de typingSpeed
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

    }

    public void NextSentence() // joue la prochaine phrase du dialogue
    {
        continueButton.gameObject.SetActive(false);
        if (index < sentences[i].sentence.Length - 1 )
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(TypeQuest());
        }
        else
        {
            textDisplay.text = "";
            index = 0;
            cam.enabled = true;
            c.SetActive(false);
            continueButton.gameObject.SetActive(false);
            textDisplay.text = "";
            started = false;

        }
    }
}
