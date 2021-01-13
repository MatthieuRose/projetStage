using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour // inventaire du joueur
{
    public delegate void onItemChange();
    public onItemChange onItemChangeCallBack; // observateur pour prévenir des qu'un objet est supprimé ou ajouté à l'inventaire ( utilisé dans InventoryUI ) 


    public static Inventory instance; // singleton, instance de la classe

    public int space = 20; // taille de l'inventaire
    private void Awake() // création du singleton
    {
        if(instance != null)
        {
            return;
        }
        instance = this;
    }


    public List<Item> items = new List<Item>(); // liste des objets de l'inventaire

    public void addItem(Item item) // ajoute un objet dans l'inventaire si il reste de la place
    {

            if (items.Count >= space)
            {
                Debug.Log("Pas de place dans l'inventaire");
                return;
            }
            items.Add(item);
            if (onItemChangeCallBack != null)
            {
               
                onItemChangeCallBack.Invoke();
            }

    }

  

    public void removeItem(Item item) // retire un objet de l'inventaire
    {
        items.Remove(item);
        if (onItemChangeCallBack != null)
        {
            onItemChangeCallBack.Invoke();
        }
    }


}
