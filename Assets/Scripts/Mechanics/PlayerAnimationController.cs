using System.Collections;
using System.Collections.Generic;
using Infectathon.Core;
using Infectathon.Model;
using UnityEngine;

namespace Infectathon.Mechanics
{
    [RequireComponent(typeof(Animator), typeof(Collider2D))]
    public class PlayerAnimationController : KinematicObject
    {
        public float maxSpeed = 7;
        public float jumpTakeOffSpeed = 7;

        public Vector2 move;
        public bool jump;
        public bool stopJump;
        public bool isGrounded;
        public bool flip;

        public LayerMask standable;

        SpriteRenderer spriteRenderer;
        Animator animator;
        private Collider2D collider2d;
        InfectathonModel model = Simulation.GetModel<InfectathonModel>();
        private Vector3 startDirection;

        protected virtual void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            collider2d = GetComponent<Collider2D>();
            animator = GetComponent<Animator>();
            startDirection = this.transform.localScale;
        }

        protected override void Update()
        {
            move.x = Input.GetAxis("Horizontal");
            RaycastHit2D raycastHit2d = Physics2D.BoxCast(collider2d.bounds.center, collider2d.bounds.size, 0f, Vector2.down, 1f, standable);
            isGrounded = (raycastHit2d.collider != null);
            ComputeVelocity();
            base.Update();
        }

        protected override void ComputeVelocity()
        {

            if (move.x > 0.01f)
            {
                animator.ResetTrigger("Idle");
                this.transform.localScale = new Vector3((startDirection.x > 0 ? startDirection.x : -startDirection.x), startDirection.y, startDirection.z);
                animator.SetTrigger("Walk");
            }
            else if (move.x < -0.01f)
            {
                animator.ResetTrigger("Idle");
                this.transform.localScale = new Vector3(-(startDirection.x > 0 ? startDirection.x : -startDirection.x), startDirection.y, startDirection.z);
                animator.SetTrigger("Walk");
            }
            else
            {
                animator.ResetTrigger("Walk");
                animator.SetTrigger("Idle");
            }
            
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = jumpTakeOffSpeed * model.jumpModifier;
                jump = false;
                Debug.Log("Yo");
                animator.ResetTrigger("Idle");
                animator.SetTrigger("1H_Jump");
            }
            else if (Input.GetButtonUp("Jump"))
            {
                stopJump = false;
                Debug.Log("No");
                if (velocity.y > 0)
                {
                    velocity.y *= model.jumpDeceleration;
                }
                animator.ResetTrigger("1H_Jump");
                animator.SetTrigger("1H_JumpDown");
            }
            else if (isGrounded && move.x < 0.003625f && move.x > -0.003625f)
            {
                animator.ResetTrigger("1H_Jump");
                animator.SetTrigger("Idle");
            }
            else if (isGrounded && (move.x > 0.003625f || move.x < -0.003625f))
            {
                animator.ResetTrigger("1H_Jump");
                animator.SetTrigger("Walk");
            }

            //animator.SetBool("grounded", isGrounded);
            //animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

            targetVelocity = move * maxSpeed;
        }
    }
}