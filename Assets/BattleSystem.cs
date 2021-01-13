using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Security.Cryptography;
using System;
using UnityEngine.PlayerLoop;
using System.Collections.Specialized;
using System.ComponentModel;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST } // état du combat ( début, tour du joueur, tour de l'adversaire, gagné , perdu )

public class BattleSystem : MonoBehaviour // Système de Combat du jeu ( machine à état )
{

	public GameObject playerPrefab; // GameObject du joueur
	public GameObject enemyPrefab; // GameObject de l'adversaire
	[Space(10)]
	public Transform playerBattleStation; // canvas de l'emplacement du joueur
	public Transform enemyBattleStation;  // canvas de l'emplacement de l'adversaire
	[Space(10)]
	Unit playerUnit; // Composant Unit du joueur
	Unit enemyUnit; // Composant Unit de l'adversaire
	[Space(10)]
	public Text dialogueText; // GameObject Text dans l'editeur 
	[Space(10)]
	public BattleHUD playerHUD; // Interface joueur durant le combat
	public BattleHUD enemyHUD; // Interface adversaire durant le combat
	[Space(10)]
	public BattleState state; // état de la partie
	[Space(10)]
	public string gameLost;
	[Space(10)]
	public GameObject DialoguePanel; // Canvas pour les dialogues
	public GameObject SpellPanel; // canvas pour les sorts
	public GameObject StuffPanel; // canvas pour les objets
	[Space(10)]
	public Vector3 pos; // position du joueur
	[Space(10)]
	public Item Hp; // potion de point de vie
	public Item Mp; // potion de mana
	public Item Sp; // potion de force
	public Item MSp; // potion de puissance de mana
	[Space(10)]
	public GameObject gh;

	//déroulement du combat
	public void NewBattle(GameObject thisEnemyPrefab) // initialisation d'un nouveau combat
    {

		GameObject.Find("FightObject").gameObject.transform.GetChild(0).gameObject.SetActive(true); // activation du canvas de combat
		GameObject.Find("Fight Camera").gameObject.transform.position = new Vector3(163.6F, 4.9F, 612.42F); // changement position camera
		enemyPrefab = thisEnemyPrefab;
		state = BattleState.START;
		StartCoroutine(SetupBattle());
    }

	IEnumerator SetupBattle() // préparation du nouveau combat
	{
		gh.SetActive(false);

		pos = playerPrefab.transform.position; // sauvegarde de la position du joueur
		playerPrefab = GameObject.Find("Player").gameObject; 
		playerPrefab.GetComponent<controlCharacter>().enabled = false;
		playerPrefab.transform.position = new Vector3(157.9F, 0, 616.41F);
		playerPrefab.transform.rotation = Quaternion.Euler(0, 353,0);
		playerPrefab.GetComponent<Animator>().enabled = false;
	

		playerUnit = playerPrefab.GetComponent<Unit>();
		// initialisation du positionnement du joueur 

		enemyPrefab.transform.position = new Vector3(159,0,624);
		enemyUnit = enemyPrefab.GetComponent<Unit>();
		// initialisation du positionnement de l'adversaire 

		dialogueText.text = "A wild " + enemyUnit.unitName + " approaches...";

		playerHUD.SetHUD(playerUnit);
		enemyHUD.SetHUD(enemyUnit);




		yield return new WaitForSeconds(2f);
		
		state = BattleState.PLAYERTURN;
		if (playerUnit.speed.getValue() < enemyUnit.speed.getValue()) // on vérifie qui joue en premier basé sur leur vitesse
		{
			EnemyTurn();
		}
		PlayerTurn();
		
	}

	IEnumerator EnemyTurn() // tour de l'adversaire
	{
		enemyUnit.IncreaseMana();
		int rdm = UnityEngine.Random.Range(0, 10);
		if (rdm <=9)
		{
			dialogueText.text = enemyUnit.unitName + " attacks!";

			yield return new WaitForSeconds(1f);

			bool isDead = playerUnit.TakeDamage(enemyUnit.damage.getValue());

			playerHUD.SetHP(playerUnit.currentHP);

			yield return new WaitForSeconds(1f);

			if (isDead)
			{
				state = BattleState.LOST;
				EndBattle();
			}
			else
			{
				state = BattleState.PLAYERTURN;
				PlayerTurn();
			}
		}

		else
        {
			enemyUnit.Heal(5);

			enemyHUD.SetHP(playerUnit.currentHP);
			dialogueText.text = enemyUnit.unitName + " regenerate his heath!";

			yield return new WaitForSeconds(1f);

			state = BattleState.PLAYERTURN;
			PlayerTurn();
		}
	}

	void EndBattle() // fin de la partie
	{
		

		if (state == BattleState.WON) // fonction lancé si le joueur gagne
		{
			
			dialogueText.text = "You won the battle!";
			playerUnit.IncreaseXp(enemyUnit.xpGiven);
			if (enemyUnit.loot.allItem != null)
			{
				foreach (var item in enemyUnit.loot.allItem) // on lui rajoute des item aléatoire du pull de l'adversaire.
				{
					int rd = UnityEngine.Random.Range(0, 10);
					if (rd > 4)
					{
						Inventory.instance.addItem(item);
					}
				}
			}
			MobFight end = playerPrefab.GetComponent<MobFight>();

			playerPrefab.transform.position = pos;

			gh.GetComponent<GameHUD>().SetHUD(playerUnit);
			gh.SetActive(true);
			foreach (var item in controlCharacter.activeQuest) // on vérifie si cela complète certaine de ses quêtes
			{
				if (!item.isCompleted)
				{
					item.goal.enemyKilled(enemyUnit.type);
					if (item.goal.isReached())
					{
						item.Complete();
					}
				}
			}

			end.EndBattle();
			
		} else if (state == BattleState.LOST) // si le joueur perd la partie
		{
			dialogueText.text = "You were defeated.";
			//SceneManager.LoadScene(gameLost);
		}
		playerPrefab.GetComponent<Animator>().enabled = true;
		playerPrefab.GetComponent<controlCharacter>().enabled = true;
	}

	void PlayerTurn() // tour du joueur
	{
		dialogueText.text = "Choose an action:";
		playerUnit.IncreaseMana();
		playerHUD.SetMana(playerUnit.currentMana);
	}

	//Attack
	public void OnAttackButton() // lancé si le joueur appuie sur le bouton d'attaque
	{
		if (state != BattleState.PLAYERTURN)
			return;

		StartCoroutine(PlayerAttack());
	}
	IEnumerator PlayerAttack() // lancé si le joueur a appuyé sur le bouton d'attaque, attaque l'adversaire
	{
		bool isDead = enemyUnit.TakeDamage(playerUnit.damage.getValue() + playerUnit.potionEffectDamage); 
		enemyHUD.SetHP(enemyUnit.currentHP);
		dialogueText.text = "The attack is successful!";

		yield return new WaitForSeconds(2f);

		if (isDead) // on vérifie si le coup tue l'ennemie 
		{
			state = BattleState.WON;
			EndBattle();
		}
		else
		{
			state = BattleState.ENEMYTURN;
			StartCoroutine(EnemyTurn());
		}
	}

	//Heal
	public void OnHealButton() // se lance si le joueur appuie sur le bouton de soin
	{
		if (state != BattleState.PLAYERTURN)
			return;

		StartCoroutine(PlayerHeal());
	}
	IEnumerator PlayerHeal() // se lance si le joueur a appuyé sur le bouton de soin , soigne le joueur
	{
		playerUnit.Heal(5);

		playerHUD.SetHP(playerUnit.currentHP);
		dialogueText.text = "You feel renewed strength!";

		yield return new WaitForSeconds(2f);

		state = BattleState.ENEMYTURN;
		StartCoroutine(EnemyTurn());
	}

	//Spell
	public void OnSpellButton() // se lance si le joueur a appuyé sur le bouton de sort, affiche les différents sort du joueur
    {
		if (state != BattleState.PLAYERTURN)
			return;


		DialoguePanel.SetActive(false);
		SpellPanel.SetActive(true);

		if (playerUnit.level < 2)
		{
			SpellPanel.transform.Find("Spell4").gameObject.SetActive(false);
			SpellPanel.transform.Find("unvS4").gameObject.SetActive(true);
			SpellPanel.transform.Find("Spell2").gameObject.SetActive(false);
			SpellPanel.transform.Find("unvS2").gameObject.SetActive(true);
			SpellPanel.transform.Find("Spell3").gameObject.SetActive(false);
			SpellPanel.transform.Find("unvS3").gameObject.SetActive(true);
		}
		else if (playerUnit.level < 4)
		{
			SpellPanel.transform.Find("Spell4").gameObject.SetActive(false);
			SpellPanel.transform.Find("unvS4").gameObject.SetActive(true);
			SpellPanel.transform.Find("unvS3").gameObject.SetActive(true);
			SpellPanel.transform.Find("Spell3").gameObject.SetActive(false);
		}
		else if (playerUnit.level < 6)
		{
			SpellPanel.transform.Find("Spell4").gameObject.SetActive(false);
			SpellPanel.transform.Find("unvS4").gameObject.SetActive(true);
		}
		else
        {
			SpellPanel.transform.Find("unvS4").gameObject.SetActive(false);
			SpellPanel.transform.Find("unvS2").gameObject.SetActive(false);
			SpellPanel.transform.Find("unvS3").gameObject.SetActive(false);
		}
		
	}

	public void OnSpell1Button() // sort numéro 1 du joueur
    {
		if (state != BattleState.PLAYERTURN)
			return;

		StartCoroutine(Spell1());
	}
	public IEnumerator Spell1() // lance le sort numéro 1 du joueur et fait des dégats à l'adversaire
    {
		DialoguePanel.SetActive(true);
		SpellPanel.SetActive(false);

		bool isDead = enemyUnit.TakeDamage(playerUnit.magicDamage.getValue()+(playerUnit.level % 10) + playerUnit.potionEffectMDamage);
		playerUnit.castSpell();
		playerHUD.SetMana(playerUnit.currentMana);

		enemyHUD.SetHP(enemyUnit.currentHP);
		dialogueText.text = "The spell1 is successful!";

		yield return new WaitForSeconds(2f);

		if (isDead)
		{
			state = BattleState.WON;
			EndBattle();
		}
		else
		{
			state = BattleState.ENEMYTURN;
			StartCoroutine(EnemyTurn());
		}
	}

	public void OnSpell2Button() // lance sort numéro 2 du joueur
	{
		if (state != BattleState.PLAYERTURN)
			return;

		StartCoroutine(Spell2());
	}
	public IEnumerator Spell2() // lance le sort numero 2 du joueur et fait des dégats à l'adversaire
	{
		DialoguePanel.SetActive(true);
		SpellPanel.SetActive(false);

		bool isDead = enemyUnit.TakeDamage(playerUnit.magicDamage.getValue()+(playerUnit.level % 12) + playerUnit.potionEffectMDamage);
		playerUnit.castSpell();
		playerHUD.SetMana(playerUnit.currentMana);
		enemyHUD.SetHP(enemyUnit.currentHP);
		dialogueText.text = "The spell2 is successful!";

		yield return new WaitForSeconds(2f);

		

		if (isDead)
		{
			state = BattleState.WON;
			EndBattle();
		}
		else
		{
			state = BattleState.ENEMYTURN;
			StartCoroutine(EnemyTurn());
		}
	}

	public void OnSpell3Button()  // lance sort numéro 3 du joueur
	{
		if (state != BattleState.PLAYERTURN)
			return;

		StartCoroutine(Spell3());
	}
	public IEnumerator Spell3() // lance le sort numero 3 du joueur et fait des dégats à l'adversaire
	{
		DialoguePanel.SetActive(true);
		SpellPanel.SetActive(false);

		bool isDead = enemyUnit.TakeDamage(playerUnit.magicDamage.getValue()+ (playerUnit.level % 14) + playerUnit.potionEffectMDamage);
		playerUnit.castSpell();
		playerHUD.SetMana(playerUnit.currentMana);
		enemyHUD.SetHP(enemyUnit.currentHP);
		dialogueText.text = "The spell3 is successful!";

		yield return new WaitForSeconds(2f);

		if (isDead)
		{
			state = BattleState.WON;
			EndBattle();
		}
		else
		{
			state = BattleState.ENEMYTURN;
			StartCoroutine(EnemyTurn());
		}
	}

	public void OnSpell4Button() // lance sort numéro 4 du joueur
	{
		if (state != BattleState.PLAYERTURN)
			return;

		StartCoroutine(Spell4());
	}
	public IEnumerator Spell4() // lance le sort numero 4 du joueur et fait des dégats à l'adversaire
	{

		DialoguePanel.SetActive(true);
		SpellPanel.SetActive(false);

		bool isDead = enemyUnit.TakeDamage(playerUnit.magicDamage.getValue()+ (playerUnit.level % 16) + playerUnit.potionEffectMDamage);
		playerUnit.castSpell();
		playerHUD.SetMana(playerUnit.currentMana);
		enemyHUD.SetHP(enemyUnit.currentHP);
		dialogueText.text = "The spell4 is successful!";

		DialoguePanel.SetActive(true);
		SpellPanel.SetActive(false);

		yield return new WaitForSeconds(2f);

		if (isDead)
		{
			state = BattleState.WON;
			EndBattle();
		}
		else
		{
			state = BattleState.ENEMYTURN;
			StartCoroutine(EnemyTurn());
		}
	}

	//Stuff
	public void OnStuffButton() // se lance si le joueur appuie sur le bouton stuff 
	{
		if (state != BattleState.PLAYERTURN)
			return;


		DialoguePanel.SetActive(false);
		StuffPanel.SetActive(true);

		PlayerStuff();
	}

	public void PlayerStuff() // affiche la liste des items utilisables en combat du joueur
	{
		foreach (var i in Inventory.instance.items)
		{
			if (i.itemId == 10)// id de l'item potion de soin
			{
				StuffPanel.transform.Find("NoHealthPotionButton").gameObject.SetActive(false);
				StuffPanel.transform.Find("HealthPotionButton").gameObject.SetActive(true);
			}
			else if (i.itemId == 11)// id de l'item potion de mana
			{
				StuffPanel.transform.Find("NoManaPotionButton").gameObject.SetActive(false);
				StuffPanel.transform.Find("ManaPotionButton").gameObject.SetActive(true);
			}
			else if (i.itemId == 12)// id de l'item potion de force
			{
				StuffPanel.transform.Find("NoStrengthPotionButton").gameObject.SetActive(false);
				StuffPanel.transform.Find("StrengthPotionButton").gameObject.SetActive(true);
			}
			else if (i.itemId == 13)// id de l'item potion de puissance magique
			{
				StuffPanel.transform.Find("NoMagicalStrengthPotionButton").gameObject.SetActive(false);
				StuffPanel.transform.Find("MagicalStrengthPotionButton").gameObject.SetActive(true);
			}
		}
	}

	public void OnHPotionButton() // se lance quand le joueur appuie sur la potion de vie
	{
		if (state != BattleState.PLAYERTURN)
			return;

		StartCoroutine(HPPotion());
	}
	public IEnumerator HPPotion() // régénère des points de vie au joueur et retire la potion de vie de son inventaire
    {

		DialoguePanel.SetActive(true);
		StuffPanel.SetActive(false);

		playerUnit.Heal(10);
		Inventory.instance.removeItem(Hp);
		playerHUD.SetHP(playerUnit.currentHP);

		dialogueText.text = "You feel renewed strength!";

		yield return new WaitForSeconds(2f);

		state = BattleState.ENEMYTURN;
		StartCoroutine(EnemyTurn());
	}

	public void OnMPotionButton() // se lance quand le joueur appuie sur la potion de mana
	{
		if (state != BattleState.PLAYERTURN)
			return;
		Debug.Log("ManaPotionButton");
		StartCoroutine(MPotion());
	}
	public IEnumerator MPotion() // régénère des points de mana au joueur et retire la potion de mana de son inventaire
	{

		DialoguePanel.SetActive(true);
		StuffPanel.SetActive(false);

		playerUnit.IncreaseMana(10);
		Inventory.instance.removeItem(Mp);
		playerHUD.SetMana(playerUnit.currentMana);

		dialogueText.text = "You feel renewed strength!";

		yield return new WaitForSeconds(2f);

		state = BattleState.ENEMYTURN;
		StartCoroutine(EnemyTurn());
	}

	public void OnSPotionButton() // se lance quand le joueur appuie sur la potion de force
	{
		if (state != BattleState.PLAYERTURN)
			return;
		Debug.Log("SPotionButton");
		StartCoroutine(SPotion());
	}
	public IEnumerator SPotion() // augmente les dégats physique du joueur et retire la potion de force de son inventaire
	{

		DialoguePanel.SetActive(true);
		StuffPanel.SetActive(false);

		playerUnit.UpdateStrength(10);
		Inventory.instance.removeItem(Sp);

		dialogueText.text = "You feel renewed strength!";

		yield return new WaitForSeconds(2f);

		state = BattleState.ENEMYTURN;
		StartCoroutine(EnemyTurn());
	}

	public void OnMSPotionButton() // se lance quand le joueur appuie sur la potion de puissance magique
	{
		if (state != BattleState.PLAYERTURN)
			return;
		Debug.Log("MSPotionButton");
		StartCoroutine(MSPotion());
	}
	public IEnumerator MSPotion() // augmente les dégats magique du joueur et retire la potion de puissance magique de son inventaire
	{

		DialoguePanel.SetActive(true);
		StuffPanel.SetActive(false);

		playerUnit.UpdateMagicStrength(10);
		Inventory.instance.removeItem(MSp);

		dialogueText.text = "You feel renewed strength!";

		yield return new WaitForSeconds(2f);

		state = BattleState.ENEMYTURN;
		StartCoroutine(EnemyTurn());
	}

	//Other
	public void OnBackButton() // retourne dans menu principal du combat
    {
		DialoguePanel.SetActive(true);
		SpellPanel.SetActive(false);
		StuffPanel.SetActive(false);
	}

	
}
