using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

//
public class Movement : MonoBehaviour
{
    // Movement
    // [SerializeField] gör så att variablen visas i inspectorn i Unity
    // private gör så att scriptet "Movement" är den enda använder den variabeln
    // float gör så att man kan spara decimal tal i variabeln
    // MoveSpeed = rörelse hastighet när du går runt
    [SerializeField] private float MoveSpeed = 8f;
    // Jumpforce = Hopp kraft när du hoppar
    [SerializeField] private float JumpForce = 16f;
    // MaxFallSpeed = max hastighet när du faller
    [SerializeField] private float MaxFallSpeed = -25f;
    // accelerationTime = antal tid när du accelererar
    [SerializeField] private float accelerationTime = 0.1f;
    // decelerationTime = antal tid när du decelererar
    [SerializeField] private float decelerationTime = 0.1f;

    // Jumping
    // coyoteTime = 
    [SerializeField] private float coyoteTime = 0.15f;
    // jumpBufferTime = 
    [SerializeField] private float jumpBufferTime = 0.15f;
    // varianlejumpHeight = 
    [SerializeField] private float variablejumpHeight = 0.5f;
    // 
    [SerializeField] private float fallMultiplier = 2.5f;
    //
    [SerializeField] private float lowJumpMultiplier = 2f;

    // ground detection
    // Transform håller koll på objectets position, rotation och skala
    // LayerMask markerar flera lager på spel objektet
    // groundCheck = känner av när du står på marken
    [SerializeField] private Transform groundCheck;
    // groundLayerMask = 
    [SerializeField] private LayerMask groundLayerMask;
    // groundCheckRadius = 
    [SerializeField] private float groundCheckRadius = 0.2f;
    // groundCheckDistance = 
    [SerializeField] private float groundCheckDistance = 0.1f;

    // Components
    // Rigidbody gör så att man kan lägga till fysik i ett objekt som gravitation, krafter, kollidation etc.
    private Rigidbody rb;

    // movement state 
    // bool 
    // 
    private float horizontalInput;
    // 
    private bool isGrounded;
    // 
    private bool wasGrounded;
    private float velocityXSmoothing;

    // 
    //jump state
    private bool jumpInputDown;
    private bool jumpInputHeld;
    private float coyoteTimeCounter;
    private float jumpbuffercounter;
    private bool isJumping;

    private void Awake()
    {
        // GetComponent 
        // freezeRotation 
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        // 
        CheckGroundStatus();
    }

    // Update is called once per frame
    void Update()
    {
        // 
        applyMovement();
        handleJumping();
    }

    private void OnDrawGizmos()
    {
        // 
        Gizmos.DrawRay(groundCheck.position, Vector3.down * groundCheckDistance);
    }


    private void CheckGroundStatus()
    {
        //wasGrounded = isGrounded;

        //Vector2 groundCheckPosition = (Vector2)groundCheck.position;
        //isGrounded = Physics.OverlapCircle(groundCheckPosition, groundCheckRadius, groundLayerMask);

       // RaycastHit hit;

        // 
        if (Physics.CheckSphere(groundCheck.position, groundCheckDistance, groundLayerMask))
        {
            // 
           // print(hit.transform.name);
            isGrounded = true;
            isJumping = false;
        }
        else
        {
            //
            isGrounded = false;
        }
       /* if (!isGrounded)
        {
           RaycastHit2D hit = Physics2D.Raycast(
                groundCheckPosition,
                Vector2.down,
                groundCheckDistance,
                groundLayerMask);
        }

        if (isGrounded && !wasGrounded)
        {
            isJumping = false;
            // Onlanding();
        }*/


    }
    private void handleJumping()
    {
        // 
        bool canJump = (isGrounded); // || coyoteTimeCounter > 0f) && jumpbuffercounter > 0f;
        print("handle" + canJump);
        if (canJump && !isJumping && Input.GetKeyDown(KeyCode.Space))
        {
            // 
            print("canJump");
            PerformJump();
        }

       /* if (!jumpInputHeld && rb.linearVelocity.y > 0f && isJumping)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y * variablejumpHeight);
            isJumping = false;
        }*/
    }

    private void PerformJump()
    {
        //
        print("jump");
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, JumpForce);

        //
        isJumping = true;
        coyoteTimeCounter = 0f;
        jumpbuffercounter = 0f;

        //  animator.SetTrigger("Jump");
        // ParticleSystem jumpEffect = Instantiate(jumpParticlePrefab, transform.position, Quaternion.identity);
    }

    private void applyMovement()
    {
        //input
        float horz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        //
        Vector3 movementDir = new Vector3(horz,0,vert);

        //kameras riktning
        //applicera fysik till rigidbody
        if (rb.linearVelocity.magnitude < 8)
        {
            //
            Vector3 movementForward = transform.forward * vert;
            Vector3 movementSideways = transform.right * horz;
            movementDir = (movementForward + movementSideways).normalized;
            rb.AddForce(movementDir * MoveSpeed * Time.deltaTime);
        }
        
    }
}
