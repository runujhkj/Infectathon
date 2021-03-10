using System.Collections;
using System.Collections.Generic;
using Infectathon.Core;
using Infectathon.Model;
using UnityEngine;

namespace Infectathon.Mechanics
{
    /// <summary>
    /// AnimationController integrates physics and animation. It is generally used for simple enemy animation.
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
    public class AnimationController : KinematicObject
    {
        /// <summary>
        /// Max horizontal speed.
        /// </summary>
        public float maxSpeed = 7;
        /// <summary>
        /// Max jump velocity
        /// </summary>
        public float jumpTakeOffSpeed = 7;

        /// <summary>
        /// Used to indicated desired direction of travel.
        /// </summary>
        public Vector2 move;

        /// <summary>
        /// Set to true to initiate a jump.
        /// </summary>
        public bool jump;

        /// <summary>
        /// Set to true to set the current jump velocity to zero.
        /// </summary>
        public bool stopJump;

        public bool flip;

        SpriteRenderer spriteRenderer;
        Animator animator;
        InfectathonModel model = Simulation.GetModel<InfectathonModel>();
        private Vector3 startDirection;

        protected virtual void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
            startDirection = this.transform.localScale;
        }

        protected override void ComputeVelocity()
        {
            if (jump && IsGrounded)
            {
                velocity.y = jumpTakeOffSpeed * model.jumpModifier;
                jump = false;
                animator.SetBool("grounded", false);
            }
            else if (stopJump)
            {
                stopJump = false;
                if (velocity.y > 0)
                {
                    velocity.y *= model.jumpDeceleration;
                }
            }
            else if (IsGrounded)
            {
                animator.SetBool("grounded", true);
            }

            if (move.x > 0.01f)
                this.transform.localScale = new Vector3((startDirection.x > 0 ? startDirection.x : -startDirection.x), startDirection.y, startDirection.z);
            else if (move.x < -0.01f)
                this.transform.localScale = new Vector3(-(startDirection.x > 0 ? startDirection.x : -startDirection.x), startDirection.y, startDirection.z);

            animator.SetBool("grounded", IsGrounded);
            animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

            targetVelocity = move * maxSpeed;
        }
    }
}