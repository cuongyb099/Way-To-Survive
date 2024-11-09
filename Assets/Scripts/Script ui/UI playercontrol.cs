using UnityEngine;
using UnityEngine.UI;

public class PlayerControls : MonoBehaviour
{
    [Header("UI Buttons")]
    public Button moveLeftButton;
    public Button moveRightButton;
    public Button jumpButton;
    public Button shootButton;

    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;

    private Rigidbody2D rb;
    private bool isJumping = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

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
            Invoke("ResetJump", 1f); // Thay đổi thời gian nếu cần
        }
    }

    private void Shoot()
    {
        Debug.Log("Bắn!");
        // Thêm logic bắn ở đây
    }

    private void ResetJump()
    {
        isJumping = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Kiểm tra va chạm với mặt đất để reset trạng thái nhảy
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }
}