using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public LevelLoader loader;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame() {
        loader.LoadGame();
    }

    public void Options() {
        SceneManager.LoadScene("OptionsMenuScene");
    }

    public void Exit() {
        Application.Quit();
    }
}
