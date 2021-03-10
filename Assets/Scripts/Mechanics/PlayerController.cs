using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Infectathon.Core;
using static Infectathon.Core.Simulation;
using Infectathon.Gameplay;
using Infectathon.Model;

namespace Infectathon.Mechanics
{
    public class PlayerController : KinematicObject
    {
        public AudioClip jumpAudio;
        public AudioClip respawnAudio;
        public AudioClip ouchAudio;
        
        public float maxSpeed = 7;
        public float jumpTakeOffSpeed = 7;

        public JumpState jumpState = JumpState.Grounded;
        private bool stopJump;
        public Collider2D collider2d;
        public AudioSource audioSource;
        public bool controlEnabled = true;
        public LayerMask climbable;
        public float distance;

        public Health health;

        bool jump;
        Vector2 move;
        SpriteRenderer spriteRenderer;
        Rigidbody2D rb2d;
        internal Animator animator;
        readonly InfectathonModel model = Simulation.GetModel<InfectathonModel>();
        public Bounds Bounds => collider2d.bounds;
        private bool isClimbing;
        private float inputHorizontal;
        private float inputVertical;

        private void Awake()
        {
            rb2d = GetComponentInChildren<Rigidbody2D>();
            audioSource = GetComponent<AudioSource>();
            collider2d = GetComponent<Collider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        protected override void Update()
        {
            if (controlEnabled)
            {
                move.x = Input.GetAxis("Horizontal");
                move.y = Input.GetAxis("Vertical");
                if (jumpState == JumpState.Grounded && Input.GetButtonDown("Jump"))
                    jumpState = JumpState.PrepareToJump;
                else if (Input.GetButtonUp("Jump"))
                {
                    stopJump = true;
                    Schedule<PlayerStopJump>().player = this;
                }
            }
            else
            {
                move.x = 0;
            }
            UpdateJumpState();
            UpdateClimbState();
            base.Update();
        }

        void UpdateJumpState()
        {
            jump = false;
            switch (jumpState)
            {
                case JumpState.PrepareToJump:
                    jumpState = JumpState.Jumping;
                    jump = true;
                    stopJump = false;
                    break;
                case JumpState.Jumping:
                    if (!IsGrounded)
                    {
                        Schedule<PlayerJumped>().player = this;
                        jumpState = JumpState.InFlight;
                    }
                    break;
                case JumpState.InFlight:
                    if (IsGrounded)
                    {
                        Schedule<PlayerLanded>().player = this;
                        jumpState = JumpState.Landed;
                    }
                    break;
                case JumpState.Landed:
                    jumpState = JumpState.Grounded;
                    break;
            }
        }
        
        void UpdateClimbState()
        {
            if (Physics2D.Raycast(transform.position, Vector2.right, distance, climbable).collider != null)
            {
                if (Input.GetButtonDown("Up"))
                {
                    isClimbing = true;
                }
                else
                {
                    isClimbing = false;
                }
            }
            else if (Physics2D.Raycast(transform.position, Vector2.left, distance, climbable).collider != null)
            {
                if (Input.GetButtonDown("Up"))
                {
                    isClimbing = true;
                }
                else
                {
                    isClimbing = false;
                }
            }
            else
            {
                isClimbing = false;
            }
        }

        protected override void ComputeVelocity()
        {
            if (jump && IsGrounded)
            {
                velocity.y = jumpTakeOffSpeed * model.jumpModifier;
                jump = false;
            }
            else if (stopJump)
            {
                stopJump = false;
                if (velocity.y > 0)
                {
                    velocity.y = velocity.y * model.jumpDeceleration;
                }
            }
            
            if (isClimbing)
            {
                Debug.Log("Is climbing");
                velocity = new Vector2(velocity.x, move.y * maxSpeed * .5f);
                rb2d.gravityScale = 0f;
            }
            else
            {
                rb2d.gravityScale = 1f;
            }

            animator.SetBool("grounded", IsGrounded);
            animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

            targetVelocity = move * maxSpeed;
        }

        public enum JumpState
        {
            Grounded,
            PrepareToJump,
            Jumping,
            InFlight,
            Landed
        }
    }
}
