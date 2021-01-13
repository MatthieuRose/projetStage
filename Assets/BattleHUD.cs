using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{

	public Text nameText;
	public Slider manaSlider;
	public Slider hpSlider;
	public RectTransform manaView;

	public void SetHUD(Unit unit)
	{
		nameText.text = unit.unitName;
		hpSlider.maxValue = unit.maxHP;
		hpSlider.value = unit.currentHP;
		if (unit.maxMana == 0)
		{
			manaSlider.gameObject.SetActive(false);
			manaView.gameObject.SetActive(false);
		}
		else
		{
			manaSlider.maxValue = unit.maxMana;
			manaSlider.value = unit.currentMana;
		}

	}

	public void SetHP(int hp)
	{
		hpSlider.value = hp;
	}

	public void SetMana(int hp)
	{
		manaSlider.value = hp;
	}

}
