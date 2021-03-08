using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainLadder : MonoBehaviour
{
    public float length;

    private PlayerInventoryController pic;
    private ItemController ic;
    private Ladder lad;
    private SpriteRenderer[] chainRend;
    private bool enabled;

    // Start is called before the first frame update
    void Start()
    {
        ic = new ItemController();
        ic.item = ItemController.Items.chain;
        pic = GameObject.FindWithTag("Player").GetComponent<PlayerInventoryController>();
        chainRend = this.gameObject.GetComponentsInChildren<SpriteRenderer>();
        Debug.Log("Beginning - disabling");
        Disable();
    }

    void Enable()
    {
        //Debug.Log("Enabled");
        if (lad == null && pic.IsInInventory(ic))
        {
            this.transform.parent.gameObject.AddComponent<Ladder>();
            for (int i = 0; i < chainRend.Length; i++)
            {
                chainRend[i].enabled = true;
            }
            enabled = true;
        }
        else
        {
            Debug.Log("Chainladder already placed or chain not in player inventory");
        }
    }

    void Disable()
    {
        //Debug.Log("Disabled");
        Destroy(this.gameObject.GetComponentInParent<Ladder>());
        for (int i = 0; i < chainRend.Length; i++)
        {
            chainRend[i].enabled = false;
        }
        enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.LogFormat("Enabled: {0}", enabled);
        //OnlyOneOn();
        lad = GameObject.Find("EnvironmentalInteractionPieces").GetComponentInChildren<Ladder>();
    }

    /*// Only one chain can be placed
    private void OnlyOneOn()
    {
        //Debug.LogFormat("Any ladder: {0}\nNo ladder here: {1}", GameObject.Find("EnvironmentalInteractionPieces").GetComponentInChildren<Ladder>(), !this.transform.parent.gameObject.GetComponent<Ladder>());
        lads = GameObject.Find("EnvironmentalInteractionPieces").GetComponentsInChildren<Ladder>();
        if (lads.Length > 1)
        {
            for 
        }
        if (GameObject.Find("EnvironmentalInteractionPieces").GetComponentInChildren<Ladder>() != this.transform.parent.gameObject.GetComponent<Ladder>())
        {
            Debug.Log("A chainladder exists that isn't this one");
            Disable();
        }
        else
        {
            //Debug.Log("No chainladders, or this chainladder found");
        }
    }*/

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Input.GetButtonDown("Use item") && !enabled)
        {
            //Debug.LogFormat("Item used, this.activeinhierarchy: {0}", this.transform.parent.gameObject.activeInHierarchy);
            Enable();
            StartCoroutine(WaitTenthSec());
        }
        else if (other.CompareTag("Player") && Input.GetButtonDown("Use item") && enabled)
        {
            //Debug.LogFormat("Item used, this.activeinhierarchy: {0}", this.transform.parent.gameObject.activeInHierarchy);
            Disable();
        }
        /*else
        {
            Disable();
        }*/
    }

    IEnumerator WaitTenthSec()
    {
        yield return new WaitForSeconds(.1f);
    }
}
