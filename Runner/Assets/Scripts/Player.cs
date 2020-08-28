using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float playerSpeed = 1f;
    [SerializeField] float gravity = 10f;
    private CharacterController controller;
    private Vector3 moveVector;
    private float verticalVelocity;

    private float cameraAnimationDuration;

    void Start()
    {
        cameraAnimationDuration = FindObjectOfType<Camera>().AnimationDuration;
        controller = GetComponent<CharacterController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time < cameraAnimationDuration)
        {
            controller.Move(Vector3.forward * playerSpeed * Time.deltaTime);
           
            return;

        }
        if (controller.isGrounded)
        {
            verticalVelocity = -.5f;
        }
        else
        {
            verticalVelocity -= gravity;
        }
        


        moveVector = Vector3.zero;
        
        
            moveVector.x = Input.GetAxisRaw("Horizontal");
        
        //jump here
        moveVector.y = verticalVelocity;
        moveVector.z = playerSpeed;

        controller.Move(moveVector * Time.deltaTime);
    }
}
