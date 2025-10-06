using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Движение")]
    public float playerSpeed = 5f;       // текущая скорость
    public float horizontalSpeed = 5f;
    public float maxSpeed = 15f;         // максимальная скорость
    public float acceleration = 0.2f;    // прирост скорости за секунду

    [Header("Прыжок")]
    public float jumpForce = 7f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    private bool isGrounded = true;
    private bool isSliding = false;

    private Rigidbody rb;

    public float minX = -5.5f;
    public float maxX = 5.5f;

    private float startSpeed;

    void Awake()
    {
        Time.timeScale = 1f; // при старте новой сцены
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startSpeed = playerSpeed; // запоминаем базовую скорость
    }

    void Update()
    {
        // Постепенно увеличиваем скорость вперед
        if (playerSpeed < maxSpeed)
            playerSpeed += acceleration * Time.deltaTime;

        Vector3 targetPos = rb.position;

        // Движение вперёд
        targetPos += Vector3.forward * playerSpeed * Time.deltaTime;

        // Горизонтальное движение: ПК или свайп
        float moveX = 0f;

        // ПК-клавиши
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            moveX = -horizontalSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            moveX = horizontalSpeed * Time.deltaTime;

        // Свайпы (мобильное управление)
        if (SwipeManager.swipeLeft)
            moveX = -horizontalSpeed * Time.deltaTime;
        if (SwipeManager.swipeRight)
            moveX = horizontalSpeed * Time.deltaTime;

        targetPos += Vector3.right * moveX;

        // Ограничение по X
        targetPos.x = Mathf.Clamp(targetPos.x, minX, maxX);

        // Применяем движение
        rb.MovePosition(targetPos);

        // Прыжок: ПК или свайп вверх
        if ((Input.GetKeyDown(KeyCode.Space) || SwipeManager.swipeUp) && isGrounded && !isSliding)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            isGrounded = false;
        }
    }

    void FixedUpdate()
    {
        // Улучшенный прыжок
        if (rb.velocity.y < 0)
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space) && !SwipeManager.swipeUp)
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }

    public void ResetPlayer()
    {
        // сброс позиции и скорости
        playerSpeed = startSpeed;
        rb.velocity = Vector3.zero;
        transform.position = new Vector3(0, 1, 0);
        isGrounded = true;
        isSliding = false;
    }
}
