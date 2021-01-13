using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Cinemachine;
using Cinemachine.Utility;
using System;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    private int index;
    public float typingSpeed;
    public Button continueButton;
    public Button storyButton;
    private bool reading = false;
    public GameObject c;
    public GameObject parent;
    public CinemachineFreeLook cam;
    private bool isOpen = false;

    void OnTriggerStay(Collider other)
    {
        
        if (other.tag == "Player")
        {
            if (Input.GetKey(KeyCode.T) && !reading)
            {
                textDisplay.text = "";
                storyButton.gameObject.SetActive(false);
                c.SetActive(true);
                reading = true;
                cam.enabled = false;
                StartCoroutine(Type());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            cam.enabled = true;
            index = 0;
            reading = false;
            c.SetActive(false);
            StopCoroutine(Type());
            continueButton.gameObject.SetActive(false);

            storyButton.gameObject.SetActive(true);
            textDisplay.text = "";

        }
    }



    void startCoroutine()
    {
        c.SetActive(true);
        reading = true;
        cam.enabled = false;
        StartCoroutine(Type());
    }


    void Start()
    {
        cam = GameObject.Find("Player").GetComponentInChildren<CinemachineFreeLook>();
        parent = GameObject.Find("DialogueObject");
        c = parent.transform.GetChild(0).gameObject;
        continueButton = c.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.GetComponent<Button>();
        storyButton = c.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.GetComponent<Button>();
        
        continueButton.onClick.AddListener(NextSentence);
        textDisplay = c.GetComponentInChildren<TextMeshProUGUI>();
        c.SetActive(false);
    }
    void Update()
    {
        if(textDisplay.text == sentences[index])
        {
            continueButton.gameObject.SetActive(true);
        }
    }
    IEnumerator Type()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

    }

    public void NextSentence()
    {
        continueButton.gameObject.SetActive(false);

        if (index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            textDisplay.text = "";
            reading = false;
            index = 0;
            cam.enabled = true;
            c.SetActive(false);
            continueButton.gameObject.SetActive(false);
            textDisplay.text = "";
            storyButton.gameObject.SetActive(false);
        }
    }
}
