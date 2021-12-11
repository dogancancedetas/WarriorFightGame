using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private CharacterController charController;
    private CharacterAnimations playerAnimations;

    public float movementSpeed = 3f;
    public float gravity = 9.8f;
    public float rotationSpeed = 0.15f;
    public float rotateDegreesPerSecond = 180;

    void Awake()
    {
        charController = GetComponent<CharacterController>();
        playerAnimations = GetComponent<CharacterAnimations>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();
        AnimateWalk();
    }

    //Move
    void Move()
    {
        if (Input.GetAxis(Axis.VERTICAL_AXIS) > 0)
        {
            Vector3 moveDirection = transform.forward;
            moveDirection.y -= gravity * Time.deltaTime;

            charController.Move(moveDirection * movementSpeed * Time.deltaTime);
        }
        else if (Input.GetAxis(Axis.VERTICAL_AXIS) < 0)
        {
            Vector3 moveDirection = -transform.forward;
            moveDirection.y -= gravity * Time.deltaTime;

            charController.Move(moveDirection * movementSpeed * Time.deltaTime);
        }
        else
        {
            //if we dont have any input to move the character
            charController.Move(Vector3.zero);
        }
    }

    //Rotate
    void Rotate()
    {
        Vector3 rotationDirection = Vector3.zero;

        if (Input.GetAxis(Axis.HORIZONTAL_AXIS) < 0)
        {
            rotationDirection = transform.TransformDirection(Vector3.left);
        }
        if (Input.GetAxis(Axis.HORIZONTAL_AXIS) > 0)
        {
            rotationDirection = transform.TransformDirection(Vector3.right);

        }
        if (rotationDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(rotationDirection), rotateDegreesPerSecond * Time.deltaTime);
            
        }
    }

    void AnimateWalk()
    {
        if (charController.velocity.sqrMagnitude != 0)
        {
            playerAnimations.Walk(true);
        }
        else
        {
            playerAnimations.Walk(false);
        }
    }
}
