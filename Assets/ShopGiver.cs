using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using System;

public class ShopGiver : MonoBehaviour // script à attacher à un GameObject pour en faire une boutique
{
    public Item[] items; // liste des objets que vend la boutique, à assigner depuis l'éditeur
    controlCharacter player; // le joueur
    CinemachineFreeLook cam; // camera du joueur
    public GameObject itemPrefab; // prefab d'un item , à assigner depuis l'éditeur
    GameObject itemShopParent; // ItemShopObject dans l'éditeur
    Transform itemParent; // itemParent dans l'éditeur
    List<GameObject> listeObjet = new List<GameObject>(); // liste des objets à vendre , visible dans le jeu dans le canvas à gauche






    private void Start() // assigne les variables
    {
        itemShopParent = GameObject.Find("ItemShopObject");
        itemShopParent.transform.GetChild(0).gameObject.SetActive(true);

        
        itemParent = GameObject.Find("itemArea").transform;
        cam = GameObject.Find("Player").GetComponentInChildren<CinemachineFreeLook>();
        player = GameObject.Find("Player").GetComponent<controlCharacter>();
        itemShopParent.transform.GetChild(0).gameObject.SetActive(false);
        
        
        

    }

    private void OnTriggerEnter(Collider other) // créé la liste des objets à vendre quand le joueur entre dans le collider de l'objet
    {
        if (other.tag == "Player")
        {
            for (int i = 0; i < items.Length; i++)
            {
                GameObject go = Instantiate(itemPrefab, itemParent);
                listeObjet.Add(go);
                go.GetComponent<Text>().text = items[i].name + " " + items[i].prix + " gold";
                ItemScript its = go.GetComponent<ItemScript>();
                items[i].myItemScript = its;
                its.buttonItem = items[i];
            }
        }
    }
    void OnTriggerStay(Collider other) // fait afficher la boutique quand le joueur appuie sur T dans le collider de l'objet
    {
        

        if (other.tag == "Player")
        {
            if (Input.GetKey(KeyCode.T))
            {
                itemShopParent.transform.GetChild(0).gameObject.SetActive(true);

                cam.enabled = false;
            }
        }
    }

    private void OnTriggerExit(Collider other) // supprime la liste des objets à vendre quand le joueur quitte le collider de l'objet
    {
        if (other.tag == "Player")
        {

            foreach (var item in listeObjet)
            {
                Destroy(item);
            }
            cam.enabled = true;
            itemShopParent.transform.GetChild(0).gameObject.SetActive(false);
        }

    }






}
