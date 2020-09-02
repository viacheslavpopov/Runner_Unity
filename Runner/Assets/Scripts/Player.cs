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
    public float gravityMultiplier = 10f;
    [SerializeField] LayerMask ground;


    private Vector3 moveVector;
    private bool isGrounded = true;
    private Rigidbody rigidBody;
    private float cameraAnimationDuration;
    private Animator animator;
    private float timeToRestrictControls;
    public bool IsDead { get; set; }

    private void Awake()
    {
        

        
    }
    void Start()
    {

        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
        cameraAnimationDuration = FindObjectOfType<Camera>().animationDuration;


    }

    // Update is called once per frame
    void Update()
    {
        if (timeToRestrictControls < cameraAnimationDuration)
        {
            // controller.Move(Vector3.forward * playerSpeed * Time.deltaTime);
            rigidBody.velocity = Vector3.forward * Time.deltaTime * playerSpeed;
            timeToRestrictControls += Time.deltaTime;
            return;

        }
        

        isGrounded = Physics.CheckSphere(transform.position, groundDistance,  ground, QueryTriggerInteraction.Ignore);

        moveVector = Vector3.zero;

        moveVector.z = 1;
        //moveVector.x = Input.GetAxisRaw("Horizontal") * strafeForce * playerSpeed/2 * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.A))
        {
           //moveVector.x = -1 * strafeForce * playerSpeed / 2 * Time.deltaTime;
            rigidBody.AddForce(Vector3.left  * strafeForce , ForceMode.VelocityChange);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            //moveVector.x = 1 * strafeForce * playerSpeed / 2 * Time.deltaTime;
            rigidBody.AddForce(Vector3.right * strafeForce, ForceMode.VelocityChange);
        }
        // strafe
        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    Debug.Log("Dash");
        //    Vector3 dashVelocity = Vector3.Scale(transform.right, strafeForce * new Vector3((Mathf.Log(1f / (Time.deltaTime * rigidBody.drag + 1)) / -Time.deltaTime), 0, (Mathf.Log(1f / (Time.deltaTime * rigidBody.drag + 1)) / -Time.deltaTime)));
        //    rigidBody.AddForce(dashVelocity, ForceMode.VelocityChange);
        //}

        // jump here
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Debug.Log("Jump");
            animator.SetTrigger("Jump");
            rigidBody.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
        }
        if (!isGrounded)
        {
            rigidBody.AddForce(Vector3.down * gravityMultiplier *  -Physics.gravity.y, ForceMode.Force);
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
