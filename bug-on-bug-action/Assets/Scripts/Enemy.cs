using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject dustCloud;

    bool takingDamage = false;
    bool isAttacking = false;
    bool isGrounded = false;
    SpriteRenderer sr;
    Rigidbody2D rb;
    Animator anim;
    System.Random r = new System.Random();

    private string currentState;

    List<string> attackList = new List<string>{"Stomp", "Charge", "GroundPound", "Impale"};

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    //GameObject.Instantiate(dustCloud, new Vector2(transform.position.x, transform.position.y + 1), Quaternion.identity);

    void Update()
    {
        if (!isAttacking) {
            if (Mathf.Abs(GameObject.Find("Player").transform.position.x - transform.position.x) <= 20) {
                rb.velocity = new Vector2(0, 0);
                if (AnimatorIsPlaying("enemy_walk"))
                    GameObject.Instantiate(dustCloud, new Vector2(transform.position.x, transform.position.y + 1), Quaternion.identity);
                StartCoroutine(Attack(attackList[r.Next(attackList.Count)]));
            } else {
                if (!AnimatorIsPlaying("enemy_walk")) {
                    ChangeAnimationState("enemy_walk");
                }
                int dir = 1;
                if (GameObject.Find("Player").transform.position.x - transform.position.x < 1)
                    dir = -1;
                rb.velocity = new Vector2(dir * 20, rb.velocity.y);
            }
        }
    }

    public IEnumerator Attack(string attackName) {
        isAttacking = true;
        switch (attackName) {
            case "Stomp":
                ChangeAnimationState("enemy_stomp");
                transform.Find("HitBoxes").transform.Find("StompHitBox").gameObject.SetActive(true);
                break;
            case "Charge":
                ChangeAnimationState("enemy_charge");
                transform.Find("HitBoxes").transform.Find("ChargeHitBox").gameObject.SetActive(true);
                int dir = 1;
                if (GameObject.Find("Player").transform.position.x - transform.position.x < 1)
                    dir = -1;
                rb.velocity = new Vector2(dir * 20, rb.velocity.y);
                break;
            case "GroundPound":
                ChangeAnimationState("enemy_groundpound");
                transform.Find("HitBoxes").transform.Find("GroundPoundHitBox").gameObject.SetActive(true);
                break;
            case "Impale":
                ChangeAnimationState("enemy_impale");
                transform.Find("HitBoxes").transform.Find("ImpaleHitBox").gameObject.SetActive(true);
                break;
            //case "Vulnerable":
              //  ChangeAnimationState("enemy_vulnerable");
                //transform.Find("HitBoxes").transform.Find("VulnerableHitBox").gameObject.SetActive(true);
                //break;
        }
        isAttacking = false;
        yield return null;
    }

    public void TakeDamage(int damage) {
        if (!takingDamage) {
            StartCoroutine(ChangeColor());
        }
    }

    public IEnumerator ChangeColor() {
        takingDamage = true;
        sr.color = new Color(1, 0.8f, 0.8f, 1f);
        yield return new WaitForSeconds(1);
        sr.color = new Color(1, 1, 1, 1);
        takingDamage = false;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "PlayerAttack") {
            TakeDamage(1);
        }

    }

    bool AnimatorIsPlaying(){
        return anim.GetCurrentAnimatorStateInfo(0).length > anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    bool AnimatorIsPlaying(string stateName){
        return AnimatorIsPlaying() && anim.GetCurrentAnimatorStateInfo(0).IsName(stateName);
    }

    void ChangeAnimationState(string newState) {
        if (isAttacking) {
            foreach (Transform hitbox in transform.Find("HitBoxes")) {
                hitbox.gameObject.SetActive(false);
            }
        }

        anim.Play(newState);
        currentState = newState;
    }
}
