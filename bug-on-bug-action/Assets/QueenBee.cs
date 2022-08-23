using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenBee : MonoBehaviour
{
    public string currentState = "queen_idle";
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            if (SceneVariables.deathCount < 10) {
                anim.Play("queen_talk");
                GameObject.Find("QueenDialogue").GetComponent<SpriteRenderer>().enabled = true;
            }
            else {
                anim.Play("queen_talk_sassy");
                GameObject.Find("QueenDialogueSassy").GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }

    public void ChangeAnimationState(string newState) {
        if (currentState == newState) return;
        anim.Play(newState);
        currentState = newState;
    }
}
