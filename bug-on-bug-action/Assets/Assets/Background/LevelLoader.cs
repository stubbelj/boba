using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1;
    public bool gameOver = false;


    // Update is called once per frame
    void Update()
    {
        Debug.Log(gameOver);
        if ((Input.GetMouseButtonDown(0)) && (!gameOver))
        {
            LoadGameOver();
            gameOver = true;
        } else if (Input.GetMouseButtonDown(1))
        {
            LoadBack();
            gameOver = false;
        }
    }

    public void LoadGameOver()
    {
        StartCoroutine(LoadLevel(2));
    }

    public void LoadBack()
    {
        StartCoroutine(LoadLevel(1));
    }

    IEnumerator LoadLevel(int index)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(index);
    }
}