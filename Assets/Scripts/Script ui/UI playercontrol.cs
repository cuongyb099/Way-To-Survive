using UnityEngine;
using UnityEngine.UI;

public class PlayerControls : MonoBehaviour
{
    [Header("UI Buttons")]
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button jumpButton;
    [SerializeField] private Button shootButton;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;

    private Rigidbody2D rb;
    private bool isJumping;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        InitializeButtons();
    }

    private void InitializeButtons()
    {
        moveLeftButton.onClick.AddListener(() => Move(-1));
        moveRightButton.onClick.AddListener(() => Move(1));
        jumpButton.onClick.AddListener(Jump);
        shootButton.onClick.AddListener(Shoot);
    }

    private void Move(int direction)
    {
        Vector2 movement = new Vector2(direction * moveSpeed * Time.deltaTime, 0);
        rb.MovePosition(rb.position + movement);
    }

    private void Jump()
    {
        if (!isJumping)
        {
            isJumping = true;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void Shoot()
    {
        Debug.Log("Bắn!");
        // Thêm logic bắn ở đây
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }
}