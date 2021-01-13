using UnityEngine;
using System.Collections;
using System;
using System.Text;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEditor;
using System.CodeDom;

public class controlCharacter : MonoBehaviour
{
    //Variables

    //For save
    public List<int> currentItem = new List<int>();
    public static List<Quest> activeQuest = new List<Quest>();
    public QuestList q;

    public GameObject questPrefab;
    public Transform questParent;
    static public String sceneToLoad;
    static public int playerX;
    static public int playerY;
    static public int playerZ;

    //

    public itemList v;

    public float speedChar = 5.0F;
    public float jumpSpeed = 8.0F;
    public float rotSpeed = 80;
    public float gravity = 10.0F;
    public bool isJumping = false;
    public float rotation = 0f;
    public Animator animator;
    public GameObject c;
    public Boolean paused = false;
    private Vector3 moveDirection = Vector3.zero;
    public GameHUD hud;

    public void SavePlayerStat()
    {
        SaveSystem.SavePlayer(this.GetComponent<Unit>(), SceneManager.GetActiveScene().name);
    }


    public void LoadPlayerStat()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        this.GetComponent<Unit>().currentHP = data.health;
        this.GetComponent<Unit>().experience = data.experience;
        this.GetComponent<Unit>().gold = data.gold;
        Vector3 position;

        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;
        controlCharacter.playerX = (int)position.x;
        controlCharacter.playerY = (int)position.y;
        controlCharacter.playerZ = (int)position.z;
        controlCharacter.sceneToLoad = data.stage;

        
        SceneManager.LoadScene("LoadingScreen");
    }

    public void SaveQuest()
    {
        SaveSystem.SaveQuest(activeQuest);
    }

    public void LoadQuest()
    {
        activeQuest = new List<Quest>();
        QuestData currentQuest = SaveSystem.LoadQuest();

        for (int i = 0; i < currentQuest.id.Count; i++)
        {
            foreach (var item2 in q.allQuest)
            {
                if (item2.questID == currentQuest.id[i])
                {
                    item2.isActive = currentQuest.active[i];
                    item2.isCompleted = currentQuest.complete[i];
                    item2.isRead = currentQuest.read[i];
                    item2.goal.currentAmount = currentQuest.currentAmount[i];
                    activeQuest.Add(item2);
                    GameObject go = Instantiate(questPrefab, questParent);
                    go.GetComponent<Text>().text = item2.Title;
                    QuestScript qs = go.GetComponent<QuestScript>();
                    item2.MyQuestScript = qs;
                    qs.buttonQuest = item2;

                }
            }
        }
    }

    public void SavePlayer()
    {
        SaveSystem.SaveInventory();
    }

    public void SaveEquipment()
    {
        SaveSystem.SaveEquipment();
    }

    public void LoadEquipment()
    {
        List<int> currentEquipment = SaveSystem.LoadEquipment();
        GameObject.Find("InventoryObject").gameObject.transform.GetChild(0).gameObject.SetActive(true);
        foreach (var item in currentEquipment)
        {
            foreach (var item2 in v.allItem)
            {
                if (item == item2.itemId)
                {
                    EquipmentManager.instance.Equip(item2 as Equipment);
                }
            }
        }
        GameObject.Find("InventoryObject").gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }
    public void LoadPlayer()
    {
        currentItem = SaveSystem.LoadInventory();
        GameObject.Find("InventoryObject").gameObject.transform.GetChild(0).gameObject.SetActive(true);
        foreach (var item in currentItem)
        {
            foreach (var item2 in v.allItem)
            {
                if (item == item2.itemId)
                {
                    Inventory.instance.addItem(item2);

                }
            }
        }
        GameObject.Find("InventoryObject").gameObject.transform.GetChild(0).gameObject.SetActive(false);

        
    }





    public void changePause()
    {
        paused = !paused;
    }
    void Update()
    {

        playerMovement();

    }

    void playerMovement()
    {
        CharacterController controller = GetComponent<CharacterController>();

        if (Input.GetKeyDown("escape") && !paused)
        {
            c.SetActive(true);
            Time.timeScale = 0;
            paused = true;

        }


        if (controller.isGrounded)
        {
            isJumping = false;
            animator.SetBool("param_idletojump", false);

            if (Input.GetKey(KeyCode.Z))
            {
                animator.SetBool("param_idletorunning", true);
                moveDirection = new Vector3(0, 0, 1);
                moveDirection *= speedChar;
                moveDirection = transform.TransformDirection(moveDirection);
            }

            if (Input.GetKey(KeyCode.S))
            {
                animator.SetBool("param_idletorunning", true);
                moveDirection = new Vector3(0, 0, 1);
                moveDirection *= speedChar;
                moveDirection = transform.TransformDirection(-moveDirection);


            }
            //Feed moveDirection with input.

            if (Input.GetButton("Jump") && !isJumping)
            {
                animator.SetBool("param_idletorunning", false);
                isJumping = true;
                animator.SetBool("param_idletojump", true);
                moveDirection.y = jumpSpeed;
            }
        }
        if (Input.GetKeyUp(KeyCode.Z) || Input.GetKeyUp(KeyCode.S))

        {
            animator.SetBool("param_idletorunning", false);
            moveDirection = new Vector3(0, 0, 0);
        }
        rotation += Input.GetAxis("Horizontal") * 160 * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, rotation, 0);
        moveDirection.y -= gravity * Time.deltaTime;

        controller.Move(moveDirection * Time.deltaTime);
    }

    void Start()
    {
        c.SetActive(false);
    }
}