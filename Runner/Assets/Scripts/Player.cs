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
    private bool isDead;

    private float cameraAnimationDuration;

    void Start()
    {
        cameraAnimationDuration = FindObjectOfType<Camera>().AnimationDuration;
        controller = GetComponent<CharacterController>();
        rigidBody = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {

        if (isDead)
        {
            return;
        }
        if (Time.time < cameraAnimationDuration)
        {
            controller.Move(Vector3.forward * playerSpeed * Time.deltaTime);
           
            return;

        }
        if (controller.isGrounded)
        {
            verticalVelocity = -1f;
        }
        else
        {
            verticalVelocity -= gravity;
        }
        


        moveVector = new Vector3(transform.position.x, 0, 0);
        //float translation = Input.GetAxis("Horizontal") * strafeForce;

        //moveVector.x = Input.GetAxis("Horizontal") * Time.deltaTime * strafeForce;


        //jump here
        moveVector.y = verticalVelocity;
        moveVector.z = playerSpeed;
        if (Input.GetKeyDown(KeyCode.A))
        {
            moveVector.x -= strafeForce;
        }
        else
        if (Input.GetKeyDown(KeyCode.D))
        {
            moveVector.x += strafeForce;
        }

        controller.Move(moveVector * Time.deltaTime);

    }


    public void IncreasePlayerSpeed() { playerSpeed += 1 * speedMultiplier; }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.point.z > transform.position.z + controller.radius)
        {
            Debug.Log("Dead");
            Die();
        }
    }
    private void Die()
    {
        isDead = true;
    }
}
