using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New ItemList", menuName = "Inventory/ItemList")]
public class itemList : ScriptableObject // permet de créer une liste d'item directement depuis l'éditeur
{

    public List<Item> allItem = new List<Item>();

    
}
