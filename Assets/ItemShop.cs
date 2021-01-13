using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemShop : MonoBehaviour // script pour initialiser et assigner le canvas dans ItemShopObject ( pour la boutique )
{
    public GameObject itemPrefab; // prefab d'un item à mettre depuis l'éditeur

    GameObject itemParent;
    private Item selected; // item selectionné par le joueur ( item sur lequel il a cliqué )
    public static ItemShop instance; // partie gauche du canvas dans ItemShopObject
    public Text itemDescription; // GameObject texte pour la description de l'item
    private Equipment f; // variable de transition dans la fonction description
    public Button buyButton; // bouton pour l'achat de l'objet / lance la fonction buyItem
    public Image img; // image de l'objet
    public GameObject gh;

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
        buyButton.gameObject.SetActive(false);
        buyButton.enabled = false;
        img.enabled = false;

    }

    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void description(Item q) // permet de faire apparaitre la description de l'objet sur la droite du canvas fils de ItemShopObject
    {
        if(selected != null) // si on appelle description alors qu'un objet est déjà selectionné, alors on déséléctionne l'ancienne objet
        {
            selected.myItemScript.Deselect();
        }

        selected = q;
        string title = q.name;
        Debug.Log(q.GetType());
        if(q is Equipment) // texte affiché différent si il s'agit d'un item ou d'un équipement
        {
            f = (Equipment)q;

            itemDescription.text = string.Format("{0}\n\n\n\n Armure : {1} \n\ndégats : {2}   ", title, f.armorModifier, f.damageModifier);

        }
        else
        {
            itemDescription.text = string.Format("{0}\n\n\n\n Description : {1} \n\n  ", title,q.itemId );
        }
        img.sprite = q.icon;
        buyButton.gameObject.SetActive(true);
        buyButton.enabled = true;
        img.enabled = true;


    }


    public void buyItem() // fonction pour acheter l'objet, déduit le prix de l'objet de l'or du joueur si le joueur a assez d'argent pour acheter l'objet
    {
        if (GameObject.Find("Player").GetComponent<Unit>().gold >= selected.prix)
        {
            GameObject.Find("Player").GetComponent<Unit>().gold -= selected.prix;
            Inventory.instance.addItem(selected);
            gh.GetComponent<GameHUD>().SetGold(GameObject.Find("Player").GetComponent<Unit>().gold);

        }
        
        
    }
}
