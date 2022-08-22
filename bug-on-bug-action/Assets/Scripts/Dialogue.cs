using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialogue : MonoBehaviour
{
    private float delay = 0.05f;
    private string currentText = "";
    // Start is called before the first frame update
    public void PrintText(string text)
    {
        StartCoroutine(ShowText(text));
    }

    // Update is called once per frame
    IEnumerator ShowText(string text)
    {
        for(int i = 0; i <= text.Length; i++)
        {
            currentText = text.Substring(0, i);
            gameObject.GetComponent<Text>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
    }
}