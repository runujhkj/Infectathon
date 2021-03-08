using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public float speed = 4f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D other)
    {
        Physics2D.gravity = new Vector2(0, 0f);
        if (other.gameObject.CompareTag("Player"))
        {

            if (Input.GetAxisRaw("Vertical") > 0)
            {
                other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
                //other.GetComponent<Rigidbody2D>().gravityScale = 0f;

            }
            else if (Input.GetAxisRaw("Vertical") < 0)
            {
                other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -speed);
                //other.GetComponent<Rigidbody2D>().gravityScale = 0f;
            }
            else
            {
                other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                //other.GetComponent<Rigidbody2D>().gravityScale = 0f;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Physics2D.gravity = new Vector2(0, -9.81f);
        if (other.CompareTag("Player"))
        {
            //Debug.Log("Player exited");
            other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            //other.GetComponent<Rigidbody2D>().gravityScale = 1f;
        }
    }
}
