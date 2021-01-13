using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item // Classe permettant de crée des objets de type équipements qui hérite de Item
{
    public EquipmentSlot equipSlot; // type d'équipement assignable depuis l'éditeur
    public int armorModifier; // armure que donne l'équipement
    public int damageModifier; // dégats que donne l'équipement


    public override void Use() // se produit quand on clique sur l'objet dans l'inventaire 
    {
        EquipmentManager.instance.Equip(this); // équipe l'objet

        removeInventory(); // enleve l'objet de l'inventaire
    }


}

public enum EquipmentSlot { Head,Chest,Leg,Weapon,Shield,Shoes} // les différents type d'équipement