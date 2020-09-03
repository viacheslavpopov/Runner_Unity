using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Speed & Strafe")]
    [SerializeField] float playerSpeed = 1f;
    [SerializeField] float speedIncrement = 2f;
    [SerializeField] float strafeForce = 1f;
    [Header("Jump & Mass")]
    [SerializeField] float groundDistance = 0.2f;
    [SerializeField] float jumpHeight = 2f;
    public float gravityMultiplier = 10f;
    public float jumpSensitivityThreshold = .5f;
    [SerializeField] LayerMask ground;
    [Space]
   //public VirtualJoystick joystick;

    private Vector3 moveVector;
    private bool isGrounded = true;
    private Rigidbody rigidBody;
    private float cameraAnimationDuration;
    private Animator animator;
    private float timeToRestrictControls;
    VirtualJoystick joystick;
    public bool IsDead { get; set; }

    void Start()
    {
        joystick = FindObjectOfType<VirtualJoystick>();
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
        cameraAnimationDuration = FindObjectOfType<Camera>().animationDuration;


    }

    // Update is called once per frame
    void Update()
    {
        if (timeToRestrictControls < cameraAnimationDuration)
        {
            rigidBody.velocity = Vector3.forward * Time.deltaTime * playerSpeed;
            timeToRestrictControls += Time.deltaTime;
            return;

        }
        Debug.Log("Joystic last input " + joystick.Horizontal + " " + joystick.Vertical);

        isGrounded = Physics.CheckSphere(transform.position, groundDistance,  ground, QueryTriggerInteraction.Ignore);

        moveVector = Vector3.zero;

        moveVector.z = 1;


        moveVector.x = joystick.Horizontal * strafeForce;
        // strafe
        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    Debug.Log("Dash");
        //    Vector3 dashVelocity = Vector3.Scale(transform.right, strafeForce * new Vector3((Mathf.Log(1f / (Time.deltaTime * rigidBody.drag + 1)) / -Time.deltaTime), 0, (Mathf.Log(1f / (Time.deltaTime * rigidBody.drag + 1)) / -Time.deltaTime)));
        //    rigidBody.AddForce(dashVelocity, ForceMode.VelocityChange);
        //}

        // jump here
        if (joystick.Vertical > jumpSensitivityThreshold
            && isGrounded)
        {
            animator.SetTrigger("Jump");
            rigidBody.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
        }
        // gravity
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
