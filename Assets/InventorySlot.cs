using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System;
using TMPro.Examples;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler // représente un emplacement d'item visuel du joueur dans l'inventaire
{
    public Image icon; // icone de l'item dans l'inventaire
    public Image icon2; // icone de l'item dans l'infobulle ( item sur lequel passe la souris )
    public Canvas c; // canvas de l'inventaire
    public Item item; // item stocké dans l'emplacement 
    public Equipment eq; // équipement stocké dans l'emplacement
    public Button removeButton; // bouton pour supprimé l'objet
    public TextMeshProUGUI t; // tewte de l'item dans l'infobulle


    GameObject currentHover;

    public void Start()
    {
        c.enabled = false;
        t = c.GetComponentInChildren<TextMeshProUGUI>();
    }
    public void OnPointerEnter(PointerEventData eventData) // si la souris passe au dessus d'un emplacement, fait apparaitre une info bulle avec la description de l'objet
    {
        if (item != null)
        {
            c.enabled = true;

            icon2.sprite = item.icon;
            icon2.enabled = true;
            t.text = "";

        }
        else
        {
            if (eq != null)
            {
                c.enabled = true;
                icon2.sprite = eq.icon;
                icon2.enabled = true;
                t.text = " Modificateur d'armure : " + eq.armorModifier + "\n" + "\n" + "\n" + " Modificateur de dégat : " + eq.armorModifier;
            }
        }
        
    }

    public void OnPointerExit(PointerEventData eventData) // si la souris quitte l'emplacement, fait disparaitre l'info bulle
    {
        c.enabled = false;
     
    }

 
    public void AddItem(Item newItem) // ajoute un item dans l'emplacement
    {
        if (newItem is Equipment)
        {
            eq = (Equipment)newItem;
            icon.sprite = eq.icon;
            icon.enabled = true;
            
        }

        else
        {
            item = newItem;
            icon.sprite = item.icon;
            icon.enabled = true;
        }
        
        removeButton.interactable = true;
    }

   

    public void clearSlot() // vide l'emplacement totalement
    {
 
        item = null;
        eq = null;
        icon.sprite = null;
        icon.enabled = false;
        icon2.sprite = null;
        icon2.enabled = false;
        removeButton.interactable = false;

    }

    public void UseItem() // utilise l'objet ( la fonction se lance quand on clique sur un emplacement )
    {
        if(eq != null)
        {
            eq.Use();
        }
        else if(item != null )
        {
            item.Use();
        }
    }
}
