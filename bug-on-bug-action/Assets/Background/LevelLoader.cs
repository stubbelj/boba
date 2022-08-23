using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1;
    // Change to player
    public Enemy boss;
    public bool gameOver = false;


    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Z)) && (!gameOver))
        {
            gameOver = true;
            LoadGameOver();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            gameOver = false;
            LoadGameOver();
        }
    }

    public void LoadGameOver()
    {
        StartCoroutine(LoadLevel(4));
    }

    public void LoadGame()
    {
        StartCoroutine(LoadLevel(2));
    }

    public void LoadBossScene()
    {
        StartCoroutine(LoadLevel(3));
    }

    public void LoadMainMenu()
    {
        StartCoroutine(LoadLevel(0));
    }

    public void LoadVictoryScreen()
    {
        StartCoroutine(LoadLevel(5));
    }

    public IEnumerator LoadLevel(int index)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(index);
    }
}
