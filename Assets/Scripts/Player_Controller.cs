using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player_Controller : MonoBehaviour
{

    private Rigidbody2D _rb2D;
    [SerializeField] private float _moveSpeed = 10;
    [Range(0, .3f)][SerializeField] private float m_MovementSmoothing = .05f;
    public GameObject _player;
    private Vector2 _moveDirection = Vector2.zero;

    private Animator _animator;
    //private Vector2 velocity = Vector2.zero;

    private float _horizontalInput;
    private float _verticalInput;

    // Units per second
    public float uPS = 3;


    // Start is called before the first frame update
    void Start()
    {
        /*DOTween.Init();*/
        _rb2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetAxis("Vertical") != 0)
        {
            _verticalInput = Input.GetAxis("Vertical");
            Debug.Log(_verticalInput);
            /*_rb2D.transform.DOMoveY(_verticalInput * _moveSpeed * Time.deltaTime * m_MovementSmoothing, 1/3);*/
        }

        else if (Input.GetAxis("Horizontal") != 0)
        {
            _horizontalInput = Input.GetAxis("Horizontal");
            Debug.Log(_horizontalInput);
            /*_rb2D.transform.DOMoveY(_horizontalInput * _moveSpeed * Time.deltaTime * m_MovementSmoothing, 1 / 3);*/
        }

    }

    void FixedUpdate()
    { 
        
    }

   /* void Move(float movmentSpeed)
    {
        Vector2 targetVelocity = new Vector2(movmentSpeed * 10f, rb.velocity.y);
        rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref velocity, movementSmoothing);
        if (movmentSpeed > 0 && !isRight) FlipDirection();
        if (movmentSpeed < 0 && isRight) FlipDirection();
    }

    private void HandleMovement(float horizontal)
    {
        if (rb.velocity.y < 0)
        {
            animator.SetBool("isGrounded", true);
        }

        animator.SetFloat("speed", Mathf.Abs(horizontal));

    }

    private void HandleInput()
    { 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("jump");
        }
    }*/

    public class InputLibrary
    {
        private KeyCode key;
        private float holdThreshold = 0.5f;
        private float doubleTapThreshold = 0.3f;

        private UnityEvent singlePressActions;
        private UnityEvent doublePressActions;
        private UnityEvent holdActions;

        public InputLibrary(KeyCode key, float holdThreshold, float doubleTapThreshold)
        {
            this.key = key;
            this.holdThreshold = holdThreshold;
            this.doubleTapThreshold = doubleTapThreshold;
        }

        #region Quick Information
        /*[BoxedHeader("Quick Information"), Space(10)]
        [BoxGroup("Quick Information"), SerializeField] private float timer = 0f;
        [BoxGroup("Quick Information"), SerializeField] private float lastTapTime = 0f;*/
        #endregion

        /*public KeyCode Key { get => key; set => key = value; }
        public float Timer { get => timer; set => timer = value; }
        public float LastTapTime { get => lastTapTime; set => lastTapTime = value; }
        public float HoldThreshold { get => holdThreshold; set => holdThreshold = value; }
        public float DoubleTapThreshold { get => doubleTapThreshold; set => doubleTapThreshold = value; }
        public UnityEvent SinglePressActions { get => singlePressActions; set => singlePressActions = value; }
        public UnityEvent DoublePressActions { get => doublePressActions; set => doublePressActions = value; }
        public UnityEvent HoldActions { get => holdActions; set => holdActions = value; }*/
    }
}
