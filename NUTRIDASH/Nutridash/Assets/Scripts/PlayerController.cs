using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject speedDecreaseIcon;
    public GameObject speedBoostIcon;
    public GameObject jumpBoostIcon;

    public float speed = 2.5f;
    public float jumpForce = 5f;
    public float thirstLossPerSecond = 1.0f;

    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius;

    public Vector3 initialPosition; // Nueva variable para la posición inicial

    // References
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    public PlayerHealth playerHealth;
    public PlayerThirst playerThirst;

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
        playerThirst = GetComponent<PlayerThirst>();
        initialPosition = transform.position; // Guardar la posición inicial del jugador
    }

    void Start()
    {
        ResetPlayer(); // Asegurarse de que el jugador se inicializa correctamente
        // Suscribirse al evento de sed baja
        playerThirst.OnThirstLow += HandleThirstLow;
    }

    void Update()
    {
        // Movement
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        _movement = new Vector2(horizontalInput, 0f);

        HandleThirst();

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
        jumpForce = 5f;
        points = 0;
        playerHealth.Heal(playerHealth.maxHealth); // Restablecer la salud
        playerThirst.IncreaseThirst(playerThirst.maxThirst);

        // Restablecer orientación
        if (!_facingRight)
        {
            Flip();
        }
        _facingRight = true;

        // Asegurarse de que las entradas del jugador están activas y el tiempo del juego está corriendo
        Time.timeScale = 1f;
    }

    // Método para aplicar una disminución de velocidad y salto
    public void ApplySpeedDecrease(float decreaseAmount, float jumpDecreaseAmount, float duration)
    {
        StartCoroutine(SpeedDecreaseCoroutine(decreaseAmount, jumpDecreaseAmount, duration));
        StartCoroutine(ShowEffectIcon(speedDecreaseIcon, duration));
    }

    private IEnumerator SpeedDecreaseCoroutine(float decreaseAmount, float jumpDecreaseAmount, float duration)
    {
        speed -= decreaseAmount;
        jumpForce -= jumpDecreaseAmount;
        Debug.Log($"Speed decreased to: {speed}, Jump force decreased to: {jumpForce}");  // Mensaje de depuración

        speedDecreaseIcon.SetActive(true); // Mostrar icono de disminución de velocidad
        yield return new WaitForSeconds(duration);
        speedDecreaseIcon.SetActive(false); // Ocultar el icono al finalizar el efecto

        speed += decreaseAmount;
        jumpForce += jumpDecreaseAmount;
        Debug.Log($"Speed reset to: {speed}, Jump force reset to: {jumpForce}");  // Mensaje de depuración
    }

    // Método para aplicar un aumento de salto
    public void ApplyJumpBoost(float boostAmount, float duration)
    {
        StartCoroutine(JumpBoostCoroutine(boostAmount, duration));
        StartCoroutine(ShowEffectIcon(jumpBoostIcon, duration));
    }

    private IEnumerator JumpBoostCoroutine(float boostAmount, float duration)
    {
        jumpForce += boostAmount;
        Debug.Log($"Jump force increased to: {jumpForce}");  // Mensaje de depuración

        jumpBoostIcon.SetActive(true); // Mostrar icono de aumento de salto
        yield return new WaitForSeconds(duration);
        jumpBoostIcon.SetActive(false); // Ocultar el icono al finalizar el efecto

        jumpForce -= boostAmount;
        Debug.Log($"Jump force reset to: {jumpForce}");  // Mensaje de depuración
    }

    // Método para aplicar un aumento de velocidad
    public void ApplySpeedBoost(float boostAmount, float duration)
    {
        StartCoroutine(SpeedBoostCoroutine(boostAmount, duration));
        StartCoroutine(ShowEffectIcon(speedBoostIcon, duration));
    }

    private IEnumerator SpeedBoostCoroutine(float boostAmount, float duration)
    {
        speed += boostAmount;
        Debug.Log($"Speed increased to: {speed}");  // Mensaje de depuración

        speedBoostIcon.SetActive(true); // Mostrar icono de aumento de velocidad
        yield return new WaitForSeconds(duration);
        speedBoostIcon.SetActive(false); // Ocultar el icono al finalizar el efecto

        speed -= boostAmount;
        Debug.Log($"Speed reset to: {speed}");  // Mensaje de depuración
    }

    private IEnumerator ShowEffectIcon(GameObject icon, float duration)
    {
        icon.SetActive(true);
        yield return new WaitForSeconds(duration);
        icon.SetActive(false);
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

    public void Heal(float amountHealth)
    {
        if (playerHealth != null)
        {
            playerHealth.Heal(amountHealth);
        }
    }

    private void HandleThirst()
    {
        if (IsPlayerMovingOnGround())
        {
            playerThirst.DecreaseThirst(thirstLossPerSecond * Time.deltaTime); // Multiplicar por deltaTime para hacerlo por segundo
        }
    }

    public void IncreaseThirst(float amountWater)
    {
        if (playerThirst != null)
        {
            playerThirst.IncreaseThirst(amountWater);
        }
    }

    private void HandleThirstLow()
    {
        playerHealth.TakeDamage(thirstLossPerSecond * Time.deltaTime);
    }

    private bool IsPlayerMovingOnGround()
    {
        float horizontalVelocity = Mathf.Abs(_rigidbody.velocity.x);
        return _isGrounded && horizontalVelocity > 0.1f;
    }
}
