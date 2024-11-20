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
            TogglePause();
        }
    }

    private void TogglePause()
    {
        isPaused = !isPaused; // Đảo ngược trạng thái paused
        pauseMenuUI.SetActive(isPaused); // Hiển thị hoặc ẩn menu
        Time.timeScale = isPaused ? 0f : 1f; // Dừng hoặc tiếp tục thời gian
    }

    private void LoadMainMenu()
    {
        // Tải lại menu chính
        SceneManager.LoadScene("MainMenu"); // Thay "MainMenu" bằng tên scene của bạn
    }

    private void QuitGame()
    {
        Application.Quit(); // Thoát khỏi trò chơi
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Dừng chơi trong Editor
#endif
    }
}