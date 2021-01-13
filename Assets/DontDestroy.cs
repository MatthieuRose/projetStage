using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour // à attacher à un objet. Permet de garder l'objet entre les scenes
{   
    void Awake()
    {

        
            GameObject[] objs = GameObject.FindGameObjectsWithTag(this.gameObject.tag); 

            if (objs.Length > 1)
            {
                Destroy(this.gameObject); // on détruit l'objet si il existe déjà dans la nouvelle scene pour ne pas le dupliquer 
            }

            DontDestroyOnLoad(this.gameObject);
        
    }
}
