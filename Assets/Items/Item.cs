using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item",menuName = "Inventory/Item")]
public class Item : ScriptableObject {

    public ItemScript myItemScript;
    static int nextIdItem = 0;
    new public string name = "New item";
    public Sprite icon = null;
    public int itemId = 0;
    public int prix;
    



    public Item()
    {
        this.itemId = Interlocked.Increment(ref nextIdItem);
    }
    public virtual void Use()
    {

    }

    public void removeInventory()
    {
        Inventory.instance.removeItem(this);
    }

    
}
