using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float playerSpeed = 1f;
    [SerializeField] float gravity = 10f;
    [SerializeField] float speedMultiplier = 2f;
    [SerializeField] float strafeForce = 1f;
    private CharacterController controller;
    private Vector3 moveVector;
    private float verticalVelocity;
    private Rigidbody rigidBody;
    private float cameraAnimationDuration;

    public bool IsDead { get; set; }

    void Start()
    {
        cameraAnimationDuration = FindObjectOfType<Camera>().AnimationDuration;
        controller = GetComponent<CharacterController>();
        rigidBody = GetComponent<Rigidbody>();
        
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
            verticalVelocity = -5f;
        }
        else
        {
            verticalVelocity -= gravity;
        }



        moveVector = Vector3.zero;


        //jump here
       // moveVector.y = verticalVelocity;

        moveVector.z = playerSpeed;
        moveVector.x = Input.GetAxisRaw("Horizontal") * playerSpeed;
        moveVector.y = verticalVelocity;
        controller.Move(moveVector * Time.deltaTime);

    }


    public void IncreasePlayerSpeed() { playerSpeed += 1 * speedMultiplier; }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Tile")
        {
            return;
        }
       
            Debug.Log("Dead");
            Die();
        
    }
    private void Die()
    {
        IsDead = true;
    }
}
