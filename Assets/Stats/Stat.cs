using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat // Permet de calculer et initialiser les stat du joueur
{
    [SerializeField]
    private int baseValue; // valeur de base des stat du joueur
   
    private List<int> modifiers = new List<int>();  // liste des modificateurs d'attaque et d'armure du joueur
    public int getValue() // calcul la valeur de l'armure ou des dégats du joueur en fonction des modificateurs
    {
        int finalValue = baseValue;
        modifiers.ForEach(x => finalValue += x);
        return finalValue;
    }

    public int getBaseValue() // retourne la valeur de base de baseValue
    {
        return baseValue;
    }

    public void setValue(int value)// change la valeur de baseValue
    {
        this.baseValue = value;
    }

    public void addModifier(int modifier) // ajoute un modificateur
    {
        if(modifier != 0)
        {
            modifiers.Add(modifier);
        }
    }

    public void removeModifier(int modifier) // enleve un modificateur
    {
        if (modifier != 0)
        {
            modifiers.Remove(modifier);
        }
    }
}
