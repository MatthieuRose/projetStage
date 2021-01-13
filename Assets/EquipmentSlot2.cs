using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;
public class EquipmentSlot2 : MonoBehaviour // représente un emplacement d'équipement visuel du joueur
{
    public Image icon; // image de l'emplacement
    public Equipment item; // équipement dans l'emplacement
    public int type; // type de l'équipement ( head, chest , leg .. )

    public void AddItem(Equipment newItem) // ajoute un item dans l'emplacement et met la bonne icone
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;

    }

    public void Unequip() // enleve un item de l'emplacement.
    {
        EquipmentManager.instance.unequip(type);
        icon.sprite = null;
        icon.enabled = false;
        item = null;
    }



}
