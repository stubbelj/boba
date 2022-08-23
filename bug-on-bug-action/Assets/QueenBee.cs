using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenBee : MonoBehaviour
{
    string currentState = "queen_idle";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (SceneVariables.deathCount < 10) {
            if (other.tag == "Player") {
                ChangeAnimationState("queen_talk");
                GameObject.Find("QueenDialogue").SetActive(true);
            }
        } else {
            a
        }
    }

    void ChangeAnimationState(string newState) {
        if (currentState == newState) return;

        anim.Play(newState);
        currentState = newState;
    }
}
