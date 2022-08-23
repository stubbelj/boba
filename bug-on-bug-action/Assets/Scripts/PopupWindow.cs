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
    public GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        img.enabled = isImgOn;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(disableKeyCode))
        {
            if (isImgOn)
            {
                Disable();
                if (isPauseMenu && Time.timeScale != 1)
                {
                    ResumeGame();
                }
            }
        } else if (Input.GetKeyDown(enableKeyCode))
        {
            if (!isImgOn)
            {
                Enable();
                if (isPauseMenu && Time.timeScale != 0)
                {
                    PauseGame();
                }
            }
        } else if (Input.GetKeyDown(switchKeyCode))
        {
            alternate();
            if (isImgOn && isPauseMenu)
            {
                PauseGame();
            }
            else if (!isImgOn && isPauseMenu)
            {
                ResumeGame();
            }
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
        if (canvas != null)
        {
            canvas.SetActive(false);
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
