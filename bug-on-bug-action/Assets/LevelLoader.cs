using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LoadGameOver();
        } else if (Input.GetMouseButtonDown(1))
        {
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
