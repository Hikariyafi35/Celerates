using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D _rb;
    private Animator _animator;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpHeight = 5f;

    //properties
    private bool _grounded = false;
    public bool OnTheGround{
        private set {
            if (_grounded != value) {
                _grounded = value;
                _animator.SetBool("grounded", _grounded);
            }
        }
        get => _grounded;
    }
    private float _horizontalMovement;
    public float HorizontalMovement{
        private set {
            if(value != _horizontalMovement) {
                _horizontalMovement = value;
                if(_horizontalMovement != 0)
                    FacingRight = _horizontalMovement>0;

                    _animator.SetFloat("Xspeed", Mathf.Abs(_horizontalMovement));
                
            }
        }

        get => _horizontalMovement;
    }
    private bool _facingRight = true;
    public bool FacingRight{
        private set {
            if (_facingRight != value) {
                _facingRight=value;
                Flip();
            }
        }
        get  => _facingRight;
    }
    private float _verticalSpeed;
    public float VerticalSpeed {
        private set {
            if(_verticalSpeed != value){
                _verticalSpeed = value;
                _animator.SetFloat("Yspeed",_verticalSpeed);
            }
        }
        get => _verticalSpeed;
    }
    private void Awake(){
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        GroundCheck();
        VerticalSpeed = _rb.velocity.y;

        HorizontalMovement = Input.GetAxis("Horizontal");
        Jump();
        
        //Debug.Log(_grounded;
    }
    
    private void FixedUpdate() {
        Move();
    }
    
    private void Move(){
        _rb.velocity = new Vector2(
            HorizontalMovement * moveSpeed,
            _rb.velocity.y
        );
    }

    private void GroundCheck(){
        RaycastHit2D[] hits = new RaycastHit2D[5];
        int numhits = _rb.Cast(Vector2.down, hits, 0.5f);
        _grounded = numhits > 0;
    }
    private void Jump(){
        if(Input.GetButtonDown("Jump") && _grounded)
        {
            _animator.SetTrigger("jumpTrigger");
            _rb.AddForce(Vector2.up * jumpHeight,ForceMode2D.Impulse);
        }
    }

    private void Flip(){
        Vector3 temp = transform.localScale;
        temp.x *= -1;
        transform.localScale = temp;
    }
    
}
