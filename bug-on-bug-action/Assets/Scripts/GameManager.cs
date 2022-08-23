using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Dialogue dialogueBox;
    // Start is called before the first frame update
    void Start()
    {
        //dialogueBox = GameObject.Find("DialogueTMP").GetComponent<Dialogue>();
        dialogueBox.PrintText("According to all known laws of aviation, there is no way a bee should be able to fly. It's wings are too small to get its fat little body off the ground. The bee, of course, flies anyway, because bees don't care what humans think is impossible.");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
