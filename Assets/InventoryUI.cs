using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour // permet de mettre à jour le visuel de l'inventaire
{
    // canvas de l'inventaire dans InventoryObject
    GameObject itemParent;
    GameObject parent;
    GameObject inventoryUI;

    InventorySlot[] slots; // tout les objets ayant le composant InventorySlot fils de l'objet InventoryObject dans l'éditeur
    Inventory inventory; // Singleton inventaire du joueur

    void Start() // assignement des variables
    {
        parent = GameObject.Find("InventoryObject");
        inventoryUI = parent.transform.GetChild(0).gameObject;
        inventoryUI.SetActive(true);
        itemParent = GameObject.Find("ItemsParent");
       
        
        inventory = Inventory.instance;
        inventory.onItemChangeCallBack += UpdateUI; // permet de souscrire à la variable onItemChangeCallBack de l'inventaire et de lancer la méthode updateUi quand celui-ci envoie une notification

        slots = itemParent.GetComponentsInChildren<InventorySlot>();
        inventoryUI.SetActive(false);

    }
    void UpdateUI() // permet de mettre à jour le visuel de l'inventaire quand un nouvel objet est ajouté
    {
        for (int i=0;i<slots.Length;i++)
        {
            if( i < inventory.items.Count)
            {
                slots[i].clearSlot();
                slots[i].AddItem(inventory.items[i]);
                
            }
            else
            {
                slots[i].clearSlot();
            }
        }
    }
}
