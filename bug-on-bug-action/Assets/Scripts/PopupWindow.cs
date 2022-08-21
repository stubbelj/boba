using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupWindow : MonoBehaviour
{
    public bool isImgOn = false;
    private Image img;
    public KeyCode enableKeyCode;
    public KeyCode disableKeyCode;
    public KeyCode switchKeyCode;
    public bool isPauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        img.enabled = false;
        isImgOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(disableKeyCode))
        {
            if (isImgOn)
            {
                Disable();
            }
        } else if (Input.GetKeyDown(enableKeyCode))
        {
            if (!isImgOn)
            {
                Enable();
            }
        } else if (Input.GetKeyDown(switchKeyCode))
        {
            alternate();
        }

        if (isImgOn && isPauseMenu)
        {
            PauseGame();
        } else if (!isImgOn && isPauseMenu)
        {
            ResumeGame();
        }
    }

    public void Enable()
    {
        img.enabled = true;
        isImgOn = true;
        img.raycastTarget = true;
    }

    public void alternate()
    {
        img.enabled = !img.enabled;
        isImgOn = !isImgOn;
        img.raycastTarget = !img.raycastTarget;
    }

    public void Disable()
    {
        img.enabled = false;
        isImgOn = false;
        img.raycastTarget = false;
    }

    void PauseGame()
    {
        Time.timeScale = 0;
    }
    void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
