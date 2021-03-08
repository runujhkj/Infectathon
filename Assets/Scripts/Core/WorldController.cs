using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldController : MonoBehaviour
{
    private GameObject levelFinished;

    private void Awake()
    {
        levelFinished = GameObject.FindGameObjectWithTag("Finish");
        levelFinished.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
