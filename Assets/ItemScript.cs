using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemScript : MonoBehaviour // permet de selectionner un item depuis la boutique 
{
    public Item buttonItem; // item assigné au script.
    public void Select() // change la couleur du texte et fait apparaitre sa description dans la partie droite de la boutique.
    {
        GetComponent<Text>().color = Color.red;
        ItemShop.instance.description(buttonItem);
    }

    public void Deselect() // remet la couleur du texte à la normal.
    {
        GetComponent<Text>().color = Color.black;
    }
}
