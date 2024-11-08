using UnityEngine;
using UnityEngine.UI;

public class PlayerControls : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Button moveLeftButton;
    public Button moveRightButton;
    public Button jumpButton;
    public Button shootButton;

    private bool isJumping = false;

    private void Start()
    {
        moveLeftButton.onClick.AddListener(MoveLeft);
        moveRightButton.onClick.AddListener(MoveRight);
        jumpButton.onClick.AddListener(Jump);
        shootButton.onClick.AddListener(Shoot);
    }

    private void Update()
    {
        // Kiểm tra trạng thái nhảy
        if (isJumping)
        {
            // Thêm logic nhảy ở đây nếu cần
        }
    }

    private void MoveLeft()
    {
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }

    private void MoveRight()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }

    private void Jump()
    {
        if (!isJumping)
        {
            isJumping = true;
            // Thêm logic nhảy ở đây
            Debug.Log("Nhảy!");
            // Reset nhảy sau một thời gian
            Invoke("ResetJump", 1f); // Ví dụ: một giây để nhảy
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
}