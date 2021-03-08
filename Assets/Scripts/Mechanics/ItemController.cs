using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public enum Items { chain, vPart};

    public Items item;

    private PlayerInventoryController invController;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(true);
        invController = GameObject.FindWithTag("Player").GetComponent<PlayerInventoryController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            invController.AddToInventory(this);
            this.gameObject.SetActive(false);
        }
    }
}
