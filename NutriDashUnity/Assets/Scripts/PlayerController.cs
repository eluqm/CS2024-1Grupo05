using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Velocidad del personaje.
    public float speed = 2.5f;
    // Fuerza del salto del personaje.
    public float jumpForce = 2.5f;

    // Transform del objeto que verifica si el personaje está en el suelo.
    public Transform groundCheck;
    // Capa en la que se encuentra el suelo.
    public LayerMask groundLayer;
    // Radio del círculo de verificación de suelo.
    public float groundCheckRadius;

    // References
    // Referencia al Rigidbody2D del personaje.
    private Rigidbody2D _rigidbody;
    // Referencia al Animator del personaje.
    private Animator _animator;

    // Movement
    // Vector que almacena la dirección del movimiento.
    private Vector2 _movement;
    // Booleano que indica si el personaje está mirando hacia la derecha.
    private bool _facingRight = true;
    // Booleano que indica si el personaje está en el suelo.
    private bool _isGrounded;

    // Método Awake, se llama cuando se inicializa el objeto.
    void Awake()
    {
        // Obtiene el Rigidbody2D del objeto.
        _rigidbody = GetComponent<Rigidbody2D>();
        // Obtiene el Animator del objeto.
        _animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Movement
        // Obtiene la entrada horizontal del teclado.
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        // Crea un vector de movimiento con la entrada horizontal.
        _movement = new Vector2(horizontalInput, 0f);
        
        // Flip Character
        // Si la entrada horizontal es negativa y el personaje mira hacia la derecha.
        if (horizontalInput < 0f && _facingRight == true) {
            // Voltea el personaje.
            Flip();
        // Si la entrada horizontal es positiva y el personaje no mira hacia la derecha.
        } else if (horizontalInput > 0f && _facingRight == false) {
            // Voltea el personaje.
            Flip();
        }

        // Is Grounded?
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Is Jumping?
        if (Input.GetButtonDown("Jump") && _isGrounded == true) {
            _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

    }

    void FixedUpdate()
    {
        float horizontalVelocity = _movement.normalized.x * speed;
        _rigidbody.velocity = new Vector2(horizontalVelocity, _rigidbody.velocity.y);
    }

    void LateUpdate()
    {
        _animator.SetBool("Idle", _movement == Vector2.zero);
        _animator.SetBool("IsGrounded", _isGrounded);
        _animator.SetFloat("VerticalVelocity", _rigidbody.velocity.y);
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        float localScaleX = transform.localScale.x;
        localScaleX = localScaleX * -1f;
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }
}
