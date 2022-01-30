﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;
using Platformer.Model;
using Platformer.Core;

namespace Platformer.Mechanics
{
    /// <summary>
    /// This is the main class used to implement control of the player.
    /// It is a superset of the AnimationController class, but is inlined to allow for any kind of customisation.
    /// </summary>
    public class PlayerController : KinematicObject
    {
        public AudioClip jumpAudio;
        public AudioClip respawnAudio;
        public AudioClip ouchAudio;
        public GameObject mesh;
        Material m_Material;
        public Texture m_MainTexture, m_AltMainTexture;

        bool OppositeState { get; set; }

        /// <summary>
        /// Max horizontal speed of the player.
        /// </summary>
        public float maxSpeed = 7;
        /// <summary>
        /// Initial jump velocity at the start of a jump.
        /// </summary>
        public float jumpTakeOffSpeed = 7;
        /// <summary>
        /// Gliding Lift factor.
        /// </summary>
        public float glidingLiftCoefficient = 1f;
        /// <summary>
        /// Gliding Drag factor.
        /// </summary>
        public float glidingDragCoefficient = 1.5f;

        public JumpState jumpState = JumpState.Grounded;
        private bool stopJump;
        /*internal new*/ public Collider2D collider2d;
        /*internal new*/ public AudioSource audioSource;
        public Health health;
        public bool controlEnabled = true;

        bool jump;
        bool glide;
        Vector2 move;
        //SpriteRenderer spriteRenderer;
        MeshRenderer spriteRenderer;
        public Animator animator;
        readonly PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public Bounds Bounds => collider2d.bounds;

        void Awake()
        {
            health = GetComponent<Health>();
            audioSource = GetComponent<AudioSource>();
            collider2d = GetComponent<Collider2D>();
            //spriteRenderer = mesh.GetComponent<MeshRenderer>();
            m_Material = mesh.GetComponent<Renderer>().material;
            //animator = GetComponent<Animator>();
            OppositeState = false;
            glide = false;
        }

        protected override void Update()
        {
            if (controlEnabled)
            {
                move.x = Input.GetAxis("Horizontal");
                if (jumpState == JumpState.Grounded && Input.GetButtonDown("Jump"))
                    jumpState = JumpState.PrepareToJump;
                if (jumpState == JumpState.InFlight && Input.GetButtonDown("Action")) {
                    jumpState = JumpState.PrepareToGlide;
                }
                
                if (Input.GetButtonDown("Change"))
                {
                    if (OppositeState) {
                        m_Material.SetTexture("_MainTex", m_MainTexture);
                        OppositeState = false;
                    }
                    else
                    {
                        m_Material.SetTexture("_MainTex", m_AltMainTexture);
                        OppositeState = true;
                    }
                }
                //else if (Input.GetButtonUp("Jump"))
                // {
                //     stopJump = true;
                //     Schedule<PlayerStopJump>().player = this;
                // }
            }
            else
            {
                move.x = 0;
            }
            UpdateJumpState();
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
                    glide = false;
                    break;
                case JumpState.PrepareToGlide:
                    jumpState = JumpState.Gliding;
                    glide = true;
                    break;
                case JumpState.Gliding:
                    if (IsGrounded)
                    {
                        Schedule<PlayerLanded>().player = this;
                        jumpState = JumpState.Landed;
                    }
                    break;
            }
        }

        protected override void ComputeVelocity()
        {
            if (jump && IsGrounded)
            {
                velocity.y = jumpTakeOffSpeed * model.jumpModifier;
                jump = false;
            }
            //else if (stopJump)
            //{
            //    stopJump = false;
            //    if (velocity.y > 0)
            //    {
            //        velocity.y = velocity.y * model.jumpDeceleration;
            //    }
            //}


            //if (move.x > 0.01f)
            //spriteRenderer.flipX = false;
            //else if (move.x < -0.01f)
            //spriteRenderer.flipX = true;

            animator.SetBool("grounded", IsGrounded);
            animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

            targetVelocity = move * maxSpeed;

            if (glide)
            {
                this.gravityModifier = glidingLiftCoefficient;
                targetVelocity.x *= glidingDragCoefficient;
            }
            else
            {
                this.gravityModifier = 1f;
            }
        }

        public enum JumpState
        {
            Grounded,
            PrepareToJump,
            Jumping,
            InFlight,
            Landed,
            PrepareToGlide,
            Gliding
        }
    }
}