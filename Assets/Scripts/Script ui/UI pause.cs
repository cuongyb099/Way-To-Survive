using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button quitButton;

    private void Start()
    {
        pauseMenuUI.SetActive(false);
        resumeButton.onClick.AddListener(ResumeGame);
        mainMenuButton.onClick.AddListener(() => LoadScene("MainMenu"));
        quitButton.onClick.AddListener(Application.Quit);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            TogglePause();
    }

    private void TogglePause()
    {
        bool isPaused = pauseMenuUI.activeSelf;
        pauseMenuUI.SetActive(!isPaused);
        Time.timeScale = isPaused ? 1f : 0f;
    }

    private void LoadScene(string sceneName)
    {
        Time.timeScale = 1f; // Đảm bảo thời gian được tiếp tục khi chuyển cảnh
        SceneManager.LoadScene(sceneName);
    }
}