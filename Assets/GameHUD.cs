using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class GameHUD : MonoBehaviour // Interface jeu du joueur
{

	public Text nameText;  // GameObject text dans l'editeur pour le nom du joueur
	public Slider hpSlider; // barre de vie du joueur
	public Text HP; // GameObject text dans l'editeur pour les points de vie du joueur
	public Text gold; // GameObject text dans l'editeur pour l'or joueur
	public Text lvl; // GameObject text dans l'editeur pour le level joueur

	public void SetHUD(Unit unit) // met à jour les informations de l'interface jeu du joueur
	{
		nameText.text = unit.unitName;
		hpSlider.maxValue = unit.maxHP;
		hpSlider.value = unit.currentHP;;
		HP.text = unit.currentHP.ToString()+"/"+ unit.maxHP.ToString();
		gold.text = unit.gold.ToString();
		lvl.text = "lvl :"+unit.level.ToString();

	}

	public void SetHP(int hp, int maxHp) // met à jour les points de vie du joueur dans l'interface jeu
	{
		hpSlider.value = hp;
		hpSlider.maxValue = maxHp;
		HP.text = hp.ToString()+"/"+ maxHp.ToString();

	}

	public void SetLevel(int Nlvl) // met à jour le level du joueur dans l'interface jeu
	{
		lvl.text = "lvl :"+Nlvl.ToString();
	}

	public void SetGold(int g) // met à jour l'or du joueur dans l'interace jeu
	{
		gold.text = g.ToString();
	}

}