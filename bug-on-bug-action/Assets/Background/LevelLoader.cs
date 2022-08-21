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
            LoadBack();
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

    public IEnumerator LoadLevel(int index)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(index);
    }
}
