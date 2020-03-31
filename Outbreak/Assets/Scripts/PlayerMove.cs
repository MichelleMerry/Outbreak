
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private string horizontalInputName;
        [SerializeField] private string verticalInputName;
        [SerializeField] private float movementSpeed;

        private CharacterController charController;

        [SerializeField] private AnimationCurve jumpFallOff;
        [SerializeField] private float JumpMultiplier;
        [SerializeField] private KeyCode JumpKey;
       
        private bool isJumping;
    private bool SimpleMove;

        private void Awake()
        {
            charController = GetComponent<CharacterController>();
        }


        void Update()
        {
            float horizInput = Input.GetAxis(horizontalInputName) * movementSpeed;
            float vertInput = Input.GetAxis(verticalInputName) * movementSpeed;

            Vector3 forwardMovement = transform.forward * vertInput;
            Vector3 rightMovement = transform.right * horizInput;


        charController.SimpleMove(forwardMovement + rightMovement);
            JumpInput();
        }

        private void JumpInput()
        {
            if (Input.GetKeyDown(JumpKey) && !isJumping)
            {
                isJumping = true;
                StartCoroutine(JumpEvent());
            }
        }

        private IEnumerator JumpEvent()
        {
            charController.slopeLimit = 90.0f;
            float timeInAir = 0.0f;
            do
            {
                float jumpForce = jumpFallOff.Evaluate(timeInAir);
                charController.Move(Vector3.up * jumpForce * JumpMultiplier * Time.deltaTime);
                timeInAir += Time.deltaTime;

                yield return null;
            } while (!charController.isGrounded && charController.collisionFlags != CollisionFlags.Above);

            charController.slopeLimit = 45.0f;
            isJumping = false;
        }
    }