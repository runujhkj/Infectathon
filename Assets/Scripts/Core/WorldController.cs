using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldController : MonoBehaviour
{
    private GameObject levelFinished;

    // Start is called before the first frame update
    void Start()
    {
        levelFinished = GameObject.FindGameObjectWithTag("Finish");
        levelFinished.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
