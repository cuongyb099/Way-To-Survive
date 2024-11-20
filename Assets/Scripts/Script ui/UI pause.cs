using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject pauseMenuUI; // Giao diện menu tạm dừng
    [SerializeField] private Button resumeButton;     // Nút tiếp tục
    [SerializeField] private Button mainMenuButton;    // Nút menu chính
    [SerializeField] private Button quitButton;        // Nút thoát

    private bool isPaused = false;

    private void Start()
    {
        pauseMenuUI.SetActive(false); // Ẩn menu khi bắt đầu
        resumeButton.onClick.AddListener(ResumeGame);
        mainMenuButton.onClick.AddListener(LoadMainMenu);
        quitButton.onClick.AddListener(QuitGame);
    }

    private void Update()
    {
        // Kiểm tra phím tạm dừng
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    private void PauseGame()
    {
        isPaused = true;
        pauseMenuUI.SetActive(true); // Hiển thị menu tạm dừng
        Time.timeScale = 0f; // Dừng thời gian trong trò chơi
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseMenuUI.SetActive(false); // Ẩn menu tạm dừng
        Time.timeScale = 1f; // Tiếp tục thời gian trong trò chơi
    }

    private void LoadMainMenu()
    {
        // Tải lại menu chính (đảm bảo rằng tên scene đúng)
        SceneManager.LoadScene("MainMenu"); // Thay "MainMenu" bằng tên scene của bạn
    }

    private void QuitGame()
    {
        Application.Quit(); // Thoát khỏi trò chơi
    }
}