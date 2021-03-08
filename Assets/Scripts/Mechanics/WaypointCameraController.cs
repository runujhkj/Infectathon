using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class WaypointCameraController : MonoBehaviour
{
    public GameObject camera;
    private CinemachineVirtualCamera cameraAngle;

    // Start is called before the first frame update
    void Start()
    {
        cameraAngle = camera.GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cameraAngle.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cameraAngle.enabled = false;
        }
    }
}
