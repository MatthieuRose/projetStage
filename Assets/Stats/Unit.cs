using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Principal;
using UnityEngine;

public class Unit : StatCharacter  // script à attacher à un personnage / ennemie / joueur ( à une unité en général )
{

	public string unitName;  // nom de l'unité 
	public string type; // type de l'unité 
	[Space(10)]
	public int maxHP; // nombre de point de vie maximal de l'unité
	public int currentHP; // nombre de point de vie actuel de l'unité
	[Space(10)]
	public int maxMana; // nombre de point de mana maximal de l'unité 
	public int currentMana; // nombre de point de mana actuel de l'unité
	[Space(10)]
	public int experience; // expérience actuel de l'unité
	public int level; // level de l'unité
	public int xpGiven; // experience que donne l'unité quand elle est battu
	[Space(10)]
	public Stat magicDamage; // dégats magique de l'unité
	public Stat speed; // vitesse de l'unité
	public int gold; // argent de l'unité
	public itemList loot; // liste d'objet que donne l'unité quand elle est vaincu
	[Space(10)]
	public int potionEffectDamage; //  dégats physiques ajoutés par la potion 
	public int potionEffectMDamage; // dégats magiques ajoutés par la potion


	// Start is called before the first frame update
	void Start() // on souscrit la méthode ChangingEquipment à la variable onEquipmentChange
	{
		EquipmentManager.instance.onEquipmentChange += ChangingEquipment;
	}

	void ChangingEquipment(Equipment newItem, Equipment oldItem) // on actualise les stat du joueur
	{

		if (newItem != null)
		{
			armor.addModifier(newItem.armorModifier);
			damage.addModifier(newItem.damageModifier);
		}

		if (oldItem != null)
		{
			armor.removeModifier(oldItem.armorModifier);
			damage.removeModifier(oldItem.damageModifier);
		}
	}
	public bool TakeDamage(int dmg) // fonction pour quand une unité prends des dégats
	{
		currentHP -= dmg - armor.getValue();

		if (currentHP <= 0)
			return true;
		else
			return false;
	}

	public void Heal(int amount) // fonction pour quand une unité se soigne
	{
		currentHP += amount;
		currentMana -=1 ;
		if (currentHP > maxHP)
			currentHP = maxHP;
	}

	public void IncreaseMana() // fonction pour augmenter le mana
	{
		if (currentMana <= maxMana)
		{
			currentMana += 1;
		}
		UnityEngine.Debug.Log("mana increased");
		if (currentMana == maxMana)
        {
			UnityEngine.Debug.Log("You are full of mana");
        }
	}
	public void IncreaseMana(int x) // fonction pour augmenter le mana
	{
		if (currentMana < maxMana)
		{
			currentMana += x;
		}
		UnityEngine.Debug.Log("mana increased");
		if (currentMana == maxMana)
		{
			UnityEngine.Debug.Log("You are full of mana");
		}
	}
	public void DecreaseMana() // fonction pour réduire le mana
	{
		if (currentMana > 0)
		{
			currentMana -= 1;
			UnityEngine.Debug.Log("mana decreased");
		}
		
	}
	public void IncreaseXp(int xp) // fonction pour augmenter l'expérience et mettre à jour l'interface utilisateur
	{
		experience += xp;
		updateLevel();
    }

	public void updateLevel() { // fonction pour mettre à jour le level du joueur
		if (experience >= (((4 * (level + 1 ^ 3)) / 5)))
        {
			level += 1;
			maxHP += 20;
			currentHP = maxHP;
			damage.setValue(damage.getValue() + 5);
			
        }
	}

	public void castSpell()
    {
		currentMana = 0;
    }

	public void UpdateStrength(int x)
    {
		potionEffectDamage += x;
    }

	public void UpdateMagicStrength(int x)
    {
		potionEffectMDamage += x;
    }
}
