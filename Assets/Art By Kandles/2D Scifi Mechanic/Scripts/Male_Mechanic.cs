using UnityEngine;

public class Male_Mechanic : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0) == true)
            animator.SetTrigger("Idle");

        if (Input.GetKeyDown(KeyCode.Alpha1) == true)
            animator.SetTrigger("Walk");

        if (Input.GetKeyDown(KeyCode.Alpha2) == true)
            animator.SetTrigger("Jump");

        if (Input.GetKeyDown(KeyCode.Alpha3) == true)
            animator.SetTrigger("Die");

        if (Input.GetKeyDown(KeyCode.Alpha4) == true)
            animator.SetTrigger("1H_Idle");

        if (Input.GetKeyDown(KeyCode.Alpha5) == true)
            animator.SetTrigger("1H_Walk");

        if (Input.GetKeyDown(KeyCode.Alpha6) == true)
            animator.SetTrigger("1H_Jump");

        if (Input.GetKeyDown(KeyCode.Alpha7) == true)
            animator.SetTrigger("Shoot_Blaster"); 

        if (Input.GetKeyDown(KeyCode.Alpha8) == true)
            animator.SetTrigger("1H_Die");
    }
}