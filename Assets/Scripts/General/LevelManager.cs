using System.Collections;
using System.Collections.Generic;
using Tech.Singleton;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void SwitchToMap1()
    {
        SceneManager.LoadScene("Map1");
    }
    public void SwitchToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
