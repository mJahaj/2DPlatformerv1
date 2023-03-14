using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]// cant add player controler unless rigid body exists

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    Vector2 moveInput;

    public float CurrentMoveSpeed { get 
        {
            if(IsMoving)
            {
                if(isRunning)
                {
                    return runSpeed;
                } else 
                {
                    return walkSpeed;
                }
            } else
            {
                // idle speed is 0
                return 0;
            }
        }}
    
    private bool _isMoving = false;

    public bool IsMoving {get 
        {
            return _isMoving;
        }
        private set 
        {
            _isMoving = value;
            animator.SetBool("isMoving", value);
        }
    }
    
    private bool _isRunning = false;

    public bool isRunning 
    {
        get
        {
            return _isRunning;
        }
        set 
        {
            _isRunning = value;
            animator.SetBool("isRunning", value);
        }
    }

    public bool _isFacingRight = true;

    public bool IsFacingRight {get {return _isFacingRight; } private set{
        if(_isFacingRight != value)
        {
            //flip the local scale to make player face the opposite direction
            transform.localScale *= new Vector2(-1, 1);
        }
        _isFacingRight = value;

    }}

    Rigidbody2D rb;
    Animator animator;
    // called before start
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        IsMoving = moveInput != Vector2.zero;

        SetFacingDirection(moveInput);
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if(moveInput.x > 0 && !IsFacingRight)
        {
            // Face right
            IsFacingRight = true;
        }else if (moveInput.x < 0 && IsFacingRight)
        {
            //Face Left
            IsFacingRight = false;
        }
    }

    public void onRun(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            isRunning = true;
        } else if(context.canceled)
        {
            isRunning = false;
        }
    }
}
