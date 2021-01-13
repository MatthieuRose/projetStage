using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestScript : MonoBehaviour  // permet de selectionner une quête depuis la liste des quêtes 
{


    public Quest buttonQuest; // quête assigné au script

    public void Select() // change la couleur du titre de la quête dans le QuestLogObject et affiche sa description à droite dans le canvas
    {
        GetComponent<Text>().color = Color.red;
        QuestLog.instance.description(buttonQuest);
    }

    public void Deselect() // remet la couleur du titre de la quête à la normale .
    {
        GetComponent<Text>().color = Color.black;
    }
}
