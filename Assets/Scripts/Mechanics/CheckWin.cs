using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CheckWin : MonoBehaviour
{
    private GameObject go;
    private PlayerInventoryController pic;
    private PlayableDirector timeline;
    private bool winAvailable = false;

    // Start is called before the first frame update
    void Start()
    {
        go = GameObject.FindWithTag("HUD");
        pic = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventoryController>();
        timeline = GameObject.FindGameObjectWithTag("CutsceneTimeline").GetComponent<PlayableDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pic.VPartsLeft() > 0)
        {
            winAvailable = false;
        }
        else
        {
            winAvailable = true;
        }    
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Input.GetButtonDown("Use parts") && winAvailable)
        {
            go.SetActive(false);
            timeline.Play();
        }
    }

}
