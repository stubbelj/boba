using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    bool isattacking = false;
    bool isGrounded = false;
    SpriteRenderer sr;
    Rigidbody2D rb;
    Animator anim;
    System.Random r = new System.Random();

    private string currentState;

    List<string> attackList = new List<string>{"Stomp"};

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        //if (!isattacking) {
        //    StartCoroutine(Attack(attackList[r.Next(attackList.Count)]));
        //}
    }

    public IEnumerator Attack(string attackName) {
        isattacking = true;
        switch (attackName) {
            case "Stomp":
                ChangeAnimationState("enemy_stomp");
                break;
        }
        isattacking = false;
        yield return null;
    }

    public void TakeDamage(int damage) {
        //anim
        //belarhg
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "PlayerAttack") {
            TakeDamage(1);
        }

    }

    void ChangeAnimationState(string newState) {
        if (currentState == newState) return;
        anim.Play(newState);
        currentState = newState;
    }
}
