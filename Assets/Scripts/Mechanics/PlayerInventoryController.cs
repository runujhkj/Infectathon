using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryController : MonoBehaviour
{
    enum Items { Chain, };

    public int vPartTotal = 3;

    Dictionary<int, ItemController> items = new Dictionary<int, ItemController>();
    private int vPartCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int VPartsCollected()
    {
        return vPartCount;
    }

    public int VPartsLeft()
    {
        if (vPartCount < vPartTotal)
        {
            return vPartTotal - vPartCount;
        }
        else
        {
            return 0;
        }    
    }

    public bool IsInInventory(ItemController item)
    {
        if (items.ContainsKey((int)item.item))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddToInventory(ItemController item)
    {
        items[(int)item.item] = item;
        if (item.item == ItemController.Items.vPart)
        {
            vPartCount++;
        }
        /*//Debug.LogFormat("item: {0}\nitems: {1}", item, items);
        if (itemCount > 6)
        {

        }
        else
        {
            items[itemCount] = item;
            itemCount++;
        }*/
    }

    public void RemoveFromInventory(ItemController item)
    {

    }
}
