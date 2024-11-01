using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button playButton;          // Nút Chơi
    public Button instructionsButton;   // Nút Hướng Dẫn
    public Button quitButton;           // Nút Thoát

    void Start()
    {
        // Gán các listener cho nút
        playButton.onClick.AddListener(PlayGame);
        instructionsButton.onClick.AddListener(ShowInstructions);
        quitButton.onClick.AddListener(QuitGame);
    }

    void PlayGame()
    {
        // Tải cảnh trò chơi, giả sử là "GameScene"
        SceneManager.LoadScene("GameScene");
    }

    void ShowInstructions()
    {
        // Tải cảnh hướng dẫn, giả sử là "InstructionsScene"
        SceneManager.LoadScene("InstructionsScene");
    }

    void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Dừng trò chơi trong editor
#endif
    }
}