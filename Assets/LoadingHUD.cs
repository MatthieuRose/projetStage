using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingHUD : MonoBehaviour // permet d'actualiser l'hud avec les stat du joueur  ( hud = interface utilisateur )
{

    public GameHUD HUD;

    void Start()
    {
        HUD.SetHUD(GameObject.Find("Player").gameObject.GetComponent<Unit>());
        
    }

}
