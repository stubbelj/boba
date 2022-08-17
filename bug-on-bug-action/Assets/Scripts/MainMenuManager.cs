using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame() {
        SceneManager.LoadScene("HiveScene");
    }

    public void Options() {
        SceneManager.LoadScene("OptionsMenuScene");
    }

    public void Exit() {
        Application.Quit();
    }
}
