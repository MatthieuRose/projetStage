
using UnityEngine;

public class interactable : MonoBehaviour
{

    public string Name;
    public string InteractText = "Press F to Interact";
    public float radius = 3f;


    public virtual void OnInteract()
    {

    }

    void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position,radius);
    }
}
