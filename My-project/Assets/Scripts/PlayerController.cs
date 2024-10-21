using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Controls controls;
    private float direction = 0f;
    public float jumpForce;
    public float baseSpeed = 4.5f; // Базовая скорость игрока
    public float speed; // Текущая скорость игрока
    private float boostSpeed = 3f;
    private Rigidbody2D rb;
    private bool facingRight = true;
    private bool isGrounded;
    private bool isTouchingWall;
    private bool canDoubleJump = false;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGrounded;
    private Animator anim;
    public AudioSource jumpSound;
    public AudioSource loseLifeSound;
    public AudioSource footstepsSound;

    void Awake()
    {
        controls = new Controls();
        controls.Enable();

        controls.Land.Move.performed += ctx =>
        {
            direction = ctx.ReadValue<float>();
        };

        controls.Land.Jump.started += ctx => Jump();
    }

    private void Start()
    {
        anim = GetComponent <Animator>();
        rb = GetComponent<Rigidbody2D>();
        speed = baseSpeed; // Начальное значение скорости равно базовой скорости
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);

        if (facingRight == false && direction > 0)
        {
            Flip();
        }
        else if (facingRight == true && direction < 0)
        {
            Flip();
        }

        if (direction == 0)
        {
            anim.SetBool("isRunning", false);
        }
        else
        {
            anim.SetBool("isRunning", true);
        }
        if (direction != 0 && isGrounded && !footstepsSound.isPlaying)
        {
            footstepsSound.Play();
        }
        else if (direction == 0 || !isGrounded)
        {
            footstepsSound.Stop();
        }

    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGrounded);
        isTouchingWall = Physics2D.Raycast(feetPos.position, Vector2.right * transform.localScale.x, 0.1f, whatIsGrounded);

        if (isGrounded)
        {
            canDoubleJump = true;
        }
    }

    void Jump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpSound.Play(); // Воспроизведение звука прыжка
            canDoubleJump = true; // Сбросить возможность двойного прыжка после обычного прыжка
        }
        else if (canDoubleJump && !isTouchingWall)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            canDoubleJump = false;
            jumpSound.Play(); // Воспроизведение звука прыжка
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Platform_moving_horizontal") || collision.gameObject.name.Equals("vertical_move_platform"))
        {
            this.transform.parent = collision.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Platform_moving_horizontal") || collision.gameObject.name.Equals("vertical_move_platform"))
        {
            this.transform.parent = null;
        }
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    public void ApplyBoost(float duration)
    {
        speed += boostSpeed; // Использование переменной boostSpeed
        StartCoroutine(ResetSpeedCoroutine(duration));
    }


    public void RemoveBoost(float boostAmount)
    {
        // Возврат к обычной скорости после окончания ускорения
        speed -= boostAmount;
        Debug.Log("Removed boost: " + boostAmount + ". Current speed: " + speed);
    }

    public IEnumerator ResetSpeedCoroutine(float duration)
    {
        yield return new WaitForSeconds(duration);
        speed = baseSpeed;
        Debug.Log("Speed reset to base speed: " + speed);
    }

}