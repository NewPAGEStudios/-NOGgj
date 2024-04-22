using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    private int hitPoint;

    public AudioSource gunS;

    [Header("Movement")]
    private float moveSpeed;
    public float walkSpeed;
    public float slideSpeed;

    [Header("Jumping")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readytoJump=true;

    [Header("Slope")]
    public float maxSlopeAngle;
    private RaycastHit slopehit;
    private bool exitingSlope;

    private int currentSlide;
    private bool slideEnded = true;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode slideKey = KeyCode.LeftControl;
    public KeyCode fireKey = KeyCode.Mouse0;
    public KeyCode meleeKey = KeyCode.V;

    [Header("Ground Check")]
    bool grounded;

    public float groundDrag;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    private Transform camTransform;
    private Vector2 curRotation;
    public float maxYAngle = 80f;

    public Transform fpsLoc;

    [SerializeField]
    private GameObject bullet;

    [HideInInspector]
    public bool meleePressed;
    private float meleeCooldown=0f;
    private float _meleeCooldown=5f;

    private void Start()
    {
        camTransform = Camera.main.transform;
        Cursor.visible = false;

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        hitPoint = 1;
    }
    private void Update()
    {
        if(! slideEnded && rb.velocity.magnitude < 5f)
        {
            stopSlide();
        }
        MyInput();
        SpeedControl();

        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;

        Fire();

        if (Input.GetKeyDown(meleeKey))
        {
            if (meleeCooldown < 0)
            {
                meleeCooldown = _meleeCooldown;
                Melee();
            }
            else
            {
                Debug.Log("in Cooldown");
            }
        }


        curRotation.x += Input.GetAxis("Mouse X");
        curRotation.y += Input.GetAxis("Mouse Y");
        curRotation.x = Mathf.Repeat(curRotation.x, 360);
        curRotation.y = Mathf.Clamp(curRotation.y, -maxYAngle, maxYAngle);
        camTransform.rotation = Quaternion.Euler(-curRotation.y, curRotation.x, 0);
        transform.rotation = Quaternion.Euler(0f, curRotation.x, 0);

        rayCast(fpsLoc.forward, fpsLoc.position, 0);
        meleeCooldown -= Time.deltaTime;
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }
    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKey(jumpKey) && readytoJump && grounded)
        {
            readytoJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }

        if (Input.GetKeyDown(slideKey) && slideEnded)
        {
            startSlide();
        }
    }
    private void MovePlayer()
    {
        if (!slideEnded)
        {
            return;
        }
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        if (OnSlope() && !exitingSlope)
        {
            rb.AddForce(getSlopeMoveDirection() * walkSpeed , ForceMode.Force);
            if (rb.velocity.y > 0) rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }
        if(grounded) rb.AddForce(moveDirection.normalized * walkSpeed, ForceMode.Force);
        else if(!grounded) rb.AddForce(moveDirection.normalized * walkSpeed * airMultiplier, ForceMode.Force);

        rb.useGravity = !OnSlope();
    }
    private void SpeedControl()
    {
        if(!slideEnded) return;
        if (OnSlope() && !exitingSlope)
        {
            if (rb.velocity.magnitude > walkSpeed)
            {
                rb.velocity = rb.velocity.normalized * walkSpeed;
            }
        }
        else
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            if (flatVel.magnitude > walkSpeed)
            {
                Debug.Log("SpeedControl");
                Vector3 limitedVel = flatVel.normalized * walkSpeed;
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
        readytoJump = true;
        exitingSlope = false;
    }

    private bool OnSlope()
    {
        if(Physics.Raycast(transform.position,Vector3.down,out slopehit, 2f))
        {
            float angle = Vector3.Angle(Vector3.up, slopehit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }
        return false;

    }

    private Vector3 getSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopehit.normal).normalized;
    }

    private void startSlide()
    {
        slideEnded = false;
        transform.localScale = new Vector3(transform.localScale.x,transform.localScale.y/2,transform.localScale.z);

        GetComponent<Rigidbody>().AddForce(Vector3.down * 5f, ForceMode.Impulse);
        if(OnSlope() && !exitingSlope)
        {
            Debug.Log("LEsGO");
            GetComponent<Rigidbody>().AddForce(getSlopeSlideDirection() * slideSpeed, ForceMode.Impulse);
        }
        else
        {
            GetComponent<Rigidbody>().AddForce(transform.forward * slideSpeed, ForceMode.Impulse);
        }
    }

    private Vector3 getSlopeSlideDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopehit.normal).normalized;
    }
    private void stopSlide()
    {
        slideEnded=true;
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * 2, transform.localScale.z);
        transform.position = new Vector3(transform.position.x, transform.position.y*2, transform.position.z);
    }

    void Melee()
    {
        Debug.Log("ooo");
        meleePressed = true;
    }
    void rayCast(Vector3 direction, Vector3 origin, int iteration)
    {
        if (iteration == 5) return;
        RaycastHit[] hits = Physics.RaycastAll(origin, direction, float.MaxValue);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].transform.CompareTag("duvar"))
            {
                rayCast(Vector3.Reflect(direction, hits[i].normal), hits[i].point, iteration + 1);
            }
            else if (hits[i].transform.CompareTag("Enemy"))
            {
            }
        }
    }

    void Fire()
    {
        if (Input.GetKeyDown(fireKey))
        {
            gunS.Play();
            Instantiate(bullet, fpsLoc.position, fpsLoc.rotation);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            grounded = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            grounded = false;
        }
    }

    public void gameover()
    {
        Debug.Log("You ARE dEAD");
    }
}
