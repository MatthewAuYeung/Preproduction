using UnityEngine;
using System.Collections;

namespace Invector.CharacterController
{
    public abstract class vThirdPersonAnimator : vThirdPersonMotor
    {
        

        private void Awake()
        {
            AttackManager attackManager = FindObjectOfType<AttackManager>();
            attackManager.OnAttackStart += AttackStart;
            attackManager.OnAttackStop += AttackStop1;
        }

        void AttackStart(float padding)
        {
            canMove = false;

            animator.SetFloat("InputVertical", 0f);// 0.1f, Time.deltaTime);
            animator.SetFloat("InputHorizontal", 0f);//, 0.1f, Time.deltaTime);
            animator.SetBool("attacking", true);
        }

        void AttackStop1()
        {
            canMove = true;
            animator.SetBool("attacking", false);
        }

        public virtual void UpdateAnimator()
        {
            if (animator == null || !animator.enabled) return;

            animator.SetBool("IsStrafing", isStrafing);
            animator.SetBool("IsGrounded", isGrounded);
            animator.SetFloat("GroundDistance", groundDistance);

            if (!isGrounded)
                animator.SetFloat("VerticalVelocity", verticalVelocity);

            // fre movement get the input 0 to 1
            if (canMove)
            {
                if (speed > 0)
                {
                    SoundManagerScript.PlaySound("Walking");
                }
                else
                {
                    SoundManagerScript.EndPlay();
                }
                animator.SetFloat("InputVertical", speed);//, 0.1f, Time.deltaTime);

                if (isStrafing)
                {
                    // strafe movement get the input 1 or -1
                    animator.SetFloat("InputHorizontal", direction, 0.1f, Time.deltaTime);
                }
            }
        }

        public void OnAnimatorMove()
        {
            // we implement this function to override the default root motion.
            // this allows us to modify the positional speed before it's applied.
            if (isGrounded)
            {
                transform.rotation = animator.rootRotation;

                var speedDir = Mathf.Abs(direction) + Mathf.Abs(speed);
                speedDir = Mathf.Clamp(speedDir, 0, 1);
                var strafeSpeed = (isSprinting ? 1.5f : 1f) * Mathf.Clamp(speedDir, 0f, 1f);
                
                // strafe extra speed
                if (isStrafing)
                {
                    if (strafeSpeed <= 0.5f)
                        ControlSpeed(strafeWalkSpeed);
                    else if (strafeSpeed > 0.5f && strafeSpeed <= 1f)
                        ControlSpeed(strafeRunningSpeed);
                    else
                        ControlSpeed(strafeSprintSpeed);
                }
                else if (!isStrafing)
                {
                    // free extra speed                
                    if (speed <= 0.5f)
                        ControlSpeed(freeWalkSpeed);
                    else if (speed > 0.5 && speed <= 1f)
                        ControlSpeed(freeRunningSpeed);
                    else
                        ControlSpeed(freeSprintSpeed);
                }
            }
        }
    }
}