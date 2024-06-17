using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _animator;
    private bool _isGrounded;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpForce = 5f;
    public float maxHorintalSpeed = 5f; // kecepatan maksimal bergerak horizontal ketika diudara

    // range attack
    public KeyCode rangedAttackkey = KeyCode.Mouse1;
    public Transform rangedAttackOrigin = null;
    public GameObject projectile = null;
    public float rangedAttackDelay = 0.3f;
    public LayerMask enemyLayer = 9;
    private bool attempRangedAttack = false;
    private float timeUntilRangedReadied = 0;

    // layer untuk ground
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    AudioManager audioManager;
    EventSystem restartEventSystem;

    private float _horizontalMovement;
    public float HorizontalMovement
    {
        private set
        {
            if (value != _horizontalMovement)
            {
                _horizontalMovement = value;
                if (_horizontalMovement != 0)
                {
                    FacingRight = _horizontalMovement > 0;
                    _animator.SetFloat("Xspeed", Mathf.Abs(_horizontalMovement));

                    if (!audioManager.IsPlaying(audioManager.run)) // Cek apakah suara langkah tidak sedang diputar
                    {
                        audioManager.PlayLoopingSfx(audioManager.run);
                    }
                }
                else
                {
                    audioManager.StopSfx(audioManager.run); // Hentikan suara langkah saat tidak ada pergerakan
                }
            }
        }
        get => _horizontalMovement;
    }

    private bool _facingRight = true;
    public bool FacingRight
    {
        private set
        {
            if (_facingRight != value)
            {
                _facingRight = value;
                Flip();
            }
        }
        get => _facingRight;
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        audioManager.StopSfx(audioManager.run);
    }

    void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0) return; // Do nothing if the game is paused

        HorizontalMovement = Input.GetAxis("Horizontal");
        GetInput();
        CheckAndDoJump();
        HandleRangedAttack();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.instance.PauseGame();
        }
    }

    private void GetInput()
    {
        attempRangedAttack = Input.GetKeyDown(rangedAttackkey);
    }

    private void FixedUpdate()
    {
        if (Time.timeScale == 0) return; // Do nothing if the game is paused

        Move();
    }

    private void Move()
    {
        _rb.velocity = new Vector2(
            HorizontalMovement * moveSpeed,
            _rb.velocity.y
        );
    }

    void CheckAndDoJump()
    {
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _animator.SetTrigger("jumpTrigger");
            audioManager.playSfx(audioManager.jump);
            Jump();
        }
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        _animator.SetBool("grounded", _isGrounded);
        _animator.SetBool("isJumping", !_isGrounded);
        if (!_isGrounded)
        {
            LimitHorizontalSpeed();
        }
    }

    void Jump()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
    }

    void LimitHorizontalSpeed()
    {
        if (Mathf.Abs(_rb.velocity.x) > maxHorintalSpeed)
        {
            _rb.velocity = new Vector2(Mathf.Sign(_rb.velocity.x) * maxHorintalSpeed, _rb.velocity.y);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    private void Flip()
    {
        Vector3 temp = transform.localScale;
        temp.x *= -1;
        transform.localScale = temp;
    }

    private void HandleRangedAttack()
    {
        if (attempRangedAttack && timeUntilRangedReadied <= 0)
        {
            Debug.Log("player: attempting range attack!!");
            _animator.SetTrigger("shoot");
            audioManager.playSfx(audioManager.shoot);
            timeUntilRangedReadied = rangedAttackDelay;
        }
        else
        {
            timeUntilRangedReadied -= Time.deltaTime;
        }
    }

    public void PerformRangedAttack() // This method will be called by the Animation Event
    {
        // Adjust the rotation of the projectile based on the player's facing direction
        Quaternion projectileRotation = rangedAttackOrigin.rotation;
        if (!FacingRight)
        {
            projectileRotation = Quaternion.Euler(0, 180, 0) * rangedAttackOrigin.rotation;
        }

        Instantiate(projectile, rangedAttackOrigin.position, projectileRotation);
    }
}