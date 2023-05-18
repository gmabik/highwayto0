using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditorInternal;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Camera mainCam;

    [Header("Movement")]
    public float moveSpeed;
    public float walkSpeed;

    public float groundDrag;

    [Header("Jumping")]
    public float jumpForce;
    public float jumpCD;
    public float airMultiplayer;
    bool readyToJump;

    [Header("Crouching")]
    public float crouchSpeed;
    public float crouchYScale;
    private float startYScale;

    [Header("Wallrunning")]
    public float wallrunSpeed;
    public float desiredMovespeed;

    [Header("Dashing")]
    public float DashCD;
    public bool isDashOnCD;
    public float DashMultiplier;

    [Header("Invincibility")]
    public float timeOfInvincible;
    public bool isInvincible;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode crouchKey = KeyCode.LeftControl;
    public KeyCode dashKey = KeyCode.LeftShift;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    [Header("Slope Handling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    public MovementState state;
    public enum MovementState
    {
        walking,
        sprinting,
        crouching,
        wallrunning,
        air
    }

    public bool wallrunning;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;

        startYScale = transform.localScale.y;

        rb.drag = groundDrag;
        state = MovementState.walking;
        moveSpeed = walkSpeed;

        isDashOnCD = false;
    }


    // Update is called once per frame
    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.5f, whatIsGround);
        MyInput();
        SpeedControl();

        if(grounded && state == MovementState.air)
        {
            rb.drag = groundDrag;
            /*if (Input.GetKey(crouchKey))
            {
                state = MovementState.crouching;
                moveSpeed = crouchSpeed;
            }
            else if (Input.GetKey(sprintKey))
            {
                state = MovementState.sprinting;
                moveSpeed = sprintSpeed;
            }*/
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }
        else if(!grounded)
        {
            rb.drag = 0;
            state = MovementState.air;
        }
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCD);
        }

        DashManagement();

        CrouchManagement();
    }

    private void CrouchManagement()
    {
        if (Input.GetKeyDown(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y / 2, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }

        if (Input.GetKey(crouchKey))
        {
            state = MovementState.crouching;
            moveSpeed = crouchSpeed;
        }

        if (Input.GetKeyUp(crouchKey))
        {
            TryStandUp();
        }
    }

    private void DashManagement()
    {
        if (Input.GetKeyDown(dashKey) && !isDashOnCD) StartCoroutine(Dash());
    }

    private IEnumerator Dash()
    {
        Vector3 direction = new Vector3(mainCam.transform.rotation.x, transform.rotation.y, 1);
        rb.velocity = new Vector3(0, 0f, 0);
        rb.AddForce(mainCam.transform.forward * DashMultiplier, ForceMode.Impulse);
        isDashOnCD = true;
        StartCoroutine(InvincibilityFrames());
        yield return new WaitForSeconds(DashCD);
        isDashOnCD = false;
    }

    private IEnumerator InvincibilityFrames()
    {
        isInvincible = true;
        yield return new WaitForSeconds(timeOfInvincible);
        isInvincible = false;
    }

    private bool standUpInvokeStarted;
    private void TryStandUp()
    {
        bool cantStandUp = Physics.Raycast(transform.position, Vector3.up, playerHeight * 0.5f + 10f, whatIsGround);
        if (!cantStandUp)
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
            CancelInvoke(nameof(TryStandUp));
            standUpInvokeStarted = false;

            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }
        else if (!standUpInvokeStarted)
        {
            InvokeRepeating(nameof(TryStandUp), 0.1f, 0.1f);
            standUpInvokeStarted = true;
        }
    }
    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        if (OnSlope() && !exitingSlope)
        {

            rb.AddForce(GetSlopeMoveDirection() * moveSpeed * 20f, ForceMode.Force);
            rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }
        else if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplayer, ForceMode.Force);

    }
    private void SpeedControl()
    {
        //slope speed limit
        if (OnSlope() && !exitingSlope)
        {
            if (rb.velocity.magnitude > moveSpeed)
                rb.velocity = rb.velocity.normalized * moveSpeed;
        }

        else
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }
    }
    private void Jump()
    {
        exitingSlope = true;
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
        exitingSlope = false;
    }

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }
    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }

    public void UpdateMoveSpeed()
    {
        if (state == MovementState.walking) moveSpeed = walkSpeed;
    }

    public void Die()
    {
        if (!isInvincible) Destroy(gameObject);
    }
}
