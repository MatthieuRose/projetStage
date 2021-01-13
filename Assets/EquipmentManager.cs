using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour // instance de l'équipement du joueur
{
    GameObject EquipmentParent;
    Equipment[] CurrenEquipment; // liste des équipements du joueur
    Inventory inventory; // inventaire du joueur
    public static EquipmentManager instance; // singleton , instance de la classe
    public EquipmentSlot2[] slots; // liste des emplacements d'équipement du joueur avec leur contenu, visible en jeu
    GameObject parent;
    GameObject child;
    public delegate void OnEquipmentChange(Equipment newItem, Equipment oldItem); // observateur pour vérifier quand un équipement est changé

    public OnEquipmentChange onEquipmentChange; // observateur pour vérifier quand un équipement est changé
    private void Awake() // création du singleton
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }



    private void Start() // assignement des variables, récupération des emplacements dans les canvas dans l'éditeur
    {
        parent = GameObject.Find("InventoryObject");
        parent.transform.GetChild(0).gameObject.SetActive(true);
        EquipmentParent = GameObject.Find("EquipmentParent");
        inventory = Inventory.instance;
        int nbSlot = System.Enum.GetNames(typeof(EquipmentSlot)).Length; // récupération de la taille de l'énumération dans la classe équipement
        CurrenEquipment = new Equipment[nbSlot];
        slots = EquipmentParent.GetComponentsInChildren<EquipmentSlot2>(); // récupération des objets ayant un composant EquipmentSlot2 et étant enfant de EquipmentParent
        parent.transform.GetChild(0).gameObject.SetActive(false);


    }

    public void Equip(Equipment newItem) // permet d'équiper un équipement, est appelé quand le joueur clique sur un équipement dans son inventaire
    {
        int slotIndex = (int)newItem.equipSlot;
        Equipment oldItem = null;

        if (CurrenEquipment[slotIndex] != null)
        {
            oldItem = CurrenEquipment[slotIndex];
            inventory.addItem(oldItem);
            
        }
        inventory.removeItem(newItem);
       
        if (onEquipmentChange != null)
            {

            onEquipmentChange.Invoke(newItem, oldItem); // observateur pour prévenir de mettre à jour les stats du joueur
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if ((int)newItem.equipSlot == slots[i].type)
            {

                slots[i].AddItem(newItem);

            }
        }
        CurrenEquipment[slotIndex] = newItem;
        

    }

    public void unequip(int slotIndex) // déséquipe un objet quand le joueur clique dessus dans sa liste d'objet équipé.
    {
        if(CurrenEquipment[slotIndex]!=null)
        {
            Equipment oldItem = CurrenEquipment[slotIndex];
            inventory.addItem(oldItem);
            CurrenEquipment[slotIndex] = null;

            if (onEquipmentChange != null)
            {
                onEquipmentChange.Invoke(null, oldItem); // observateur pour prévenir de mettre à jour les stats du joueur
            }
        }
    }


}

