using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float playerSpeed = 1f;
    [SerializeField] float speedIncrement = 2f;
    [SerializeField] float strafeForce = 1f;
    [SerializeField] float groundDistance = 0.2f;
    [SerializeField] float jumpHeight = 2f;
    [SerializeField] LayerMask ground;


    private Vector3 moveVector;
    private bool isGrounded = true;
    private float verticalVelocity;
    private Rigidbody rigidBody;
    private float cameraAnimationDuration;
    private Transform groundChecker;

    public bool IsDead { get; set; }

    private void Awake()
    {
        

        
    }
    void Start()
    {


        rigidBody = GetComponent<Rigidbody>();
       // groundChecker.GetComponent<CapsuleCollider>();
        cameraAnimationDuration = FindObjectOfType<Camera>().AnimationDuration;


    }

    // Update is called once per frame
    void Update()
    {
        //isGrounded = Physics.CheckSphere(groundChecker.position, groundDistance, ground, QueryTriggerInteraction.Ignore);

        if (Time.time < cameraAnimationDuration)
        {
           // controller.Move(Vector3.forward * playerSpeed * Time.deltaTime);
            rigidBody.velocity = Vector3.forward * Time.deltaTime * playerSpeed * playerSpeed;
            return;

        }


        moveVector = Vector3.zero;

        //jump here

        moveVector.z = playerSpeed;
        moveVector.x = Input.GetAxisRaw("Horizontal") * strafeForce;
        moveVector.y = verticalVelocity;
        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    Debug.Log("Dash");
        //    Vector3 dashVelocity = Vector3.Scale(transform.right, strafeForce * new Vector3((Mathf.Log(1f / (Time.deltaTime * rigidBody.drag + 1)) / -Time.deltaTime), 0, (Mathf.Log(1f / (Time.deltaTime * rigidBody.drag + 1)) / -Time.deltaTime)));
        //    rigidBody.AddForce(dashVelocity, ForceMode.VelocityChange);
        //}
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rigidBody.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
        }
        rigidBody.velocity = new Vector3(moveVector.x, 0, moveVector.z) * Time.deltaTime * playerSpeed;


    }


    public void IncreasePlayerSpeed() { playerSpeed += speedIncrement; }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Tile")
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
