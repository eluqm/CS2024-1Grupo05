using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 2.5f;
    public float jumpForce = 2.5f;

    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius;

    public Vector3 initialPosition; // Nueva variable para la posición inicial

    // References
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    public PlayerHealth playerHealth;

    // Movement
    private Vector2 _movement;
    private bool _facingRight = true;
    private bool _isGrounded;

    // Points
    private int points = 0;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        playerHealth = GetComponent<PlayerHealth>();
        initialPosition = transform.position; // Guardar la posición inicial del jugador
    }

    void Start()
    {
        ResetPlayer(); // Asegurarse de que el jugador se inicializa correctamente
    }

    void Update()
    {
        // Movement
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        _movement = new Vector2(horizontalInput, 0f);

        // Flip Character
        if (horizontalInput < 0f && _facingRight)
        {
            Flip();
        }
        else if (horizontalInput > 0f && !_facingRight)
        {
            Flip();
        }

        // Is Grounded?
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Is Jumping?
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
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

    // Método para reiniciar el estado del jugador
    public void ResetPlayer()
    {
        Debug.Log("Reseting Player Position");
        _rigidbody.velocity = Vector2.zero;
        transform.position = initialPosition; // Usar la posición inicial guardada
        speed = 2.5f; // Restablecer otros parámetros si es necesario
        jumpForce = 2.5f;
        _facingRight = true;
        points = 0;
        playerHealth.Heal(playerHealth.maxHealth); // Restablecer la salud
                                                   // Asegurarse de que las entradas del jugador están activas y el tiempo del juego está corriendo
        Time.timeScale = 1f;
    }

    // Método para aplicar una disminución de velocidad y salto
    public void ApplySpeedDecrease(float decreaseAmount, float jumpDecreaseAmount, float duration)
    {
        StartCoroutine(SpeedDecreaseCoroutine(decreaseAmount, jumpDecreaseAmount, duration));
    }

    private IEnumerator SpeedDecreaseCoroutine(float decreaseAmount, float jumpDecreaseAmount, float duration)
    {
        speed -= decreaseAmount;
        jumpForce -= jumpDecreaseAmount;
        Debug.Log($"Speed decreased to: {speed}, Jump force decreased to: {jumpForce}");  // Mensaje de depuración
        yield return new WaitForSeconds(duration);
        speed += decreaseAmount;
        jumpForce += jumpDecreaseAmount;
        Debug.Log($"Speed reset to: {speed}, Jump force reset to: {jumpForce}");  // Mensaje de depuración
    }

    // Método para aplicar un aumento de salto
    public void ApplyJumpBoost(float boostAmount, float duration)
    {
        StartCoroutine(JumpBoostCoroutine(boostAmount, duration));
    }

    private IEnumerator JumpBoostCoroutine(float boostAmount, float duration)
    {
        jumpForce += boostAmount;
        Debug.Log($"Jump force increased to: {jumpForce}");  // Mensaje de depuración
        yield return new WaitForSeconds(duration);
        jumpForce -= boostAmount;
        Debug.Log($"Jump force reset to: {jumpForce}");  // Mensaje de depuración
    }

    // Método para aplicar un aumento de velocidad
    public void ApplySpeedBoost(float boostAmount, float duration)
    {
        StartCoroutine(SpeedBoostCoroutine(boostAmount, duration));
    }

    private IEnumerator SpeedBoostCoroutine(float boostAmount, float duration)
    {
        speed += boostAmount;
        Debug.Log($"Speed increased to: {speed}");  // Mensaje de depuración
        yield return new WaitForSeconds(duration);
        speed -= boostAmount;
        Debug.Log($"Speed reset to: {speed}");  // Mensaje de depuración
    }

    // Método para agregar puntos
    public void AddPoints(int pointsToAdd)
    {
        points += pointsToAdd;
        Debug.Log("Puntos: " + points);
    }

    // Método para aplicar daño al jugador
    public void TakeDamage(float damageAmount)
    {
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damageAmount);
        }
    }
}