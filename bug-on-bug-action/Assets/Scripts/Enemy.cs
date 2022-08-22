using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public HealthBar health;
    public GameObject dustCloud;

    bool takingDamage = false;
    bool isAttacking = false;
    SpriteRenderer sr;
    Rigidbody2D rb;
    Animator anim;
    System.Random r = new System.Random();

    private string currentState;
    public int enemyHealth = 100;
    private bool hasDied = false;

    List<string> attackList = new List<string>{"GroundPound"};

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    //GameObject.Instantiate(dustCloud, new Vector2(transform.position.x, transform.position.y + 1), Quaternion.identity);

    void Update()
    {
        Debug.Log(enemyHealth);
        if (!isAttacking) {
            if (Mathf.Abs(GameObject.Find("Player").transform.position.x - transform.position.x) <= 25) {
                rb.velocity = new Vector2(0, 0);
                if (AnimatorIsPlaying("enemy_walk"))
                    GameObject.Instantiate(dustCloud, new Vector2(transform.position.x, transform.position.y + 1), Quaternion.identity);
                StartCoroutine(Attack(attackList[r.Next(attackList.Count)]));
            } else {
                if (r.Next(0, 4) >= 1) {
                    if (!AnimatorIsPlaying("enemy_walk")) {
                        ChangeAnimationState("enemy_walk");
                    }
                    if (GameObject.Find("Player").transform.position.x - transform.position.x < 1) {
                        sr.flipX = false;
                        rb.velocity = new Vector2(-1 * 20, rb.velocity.y);
                    }
                    else {
                        sr.flipX = true;
                        rb.velocity = new Vector2(1 * 20, rb.velocity.y);
                    }
                } else {
                    StartCoroutine(Attack("Charge"));
                }
            }
        }
    }

    public IEnumerator Attack(string attackName) {
        isAttacking = true;
        switch (attackName) {
            case "Stomp":
                ChangeAnimationState("enemy_stomp");
                transform.Find("HitBoxes").transform.Find("StompHitBox").gameObject.SetActive(true);
                yield return new WaitForSeconds(2);
                break;
            case "Charge":
                sr.color = new Color(1, 0, 0, 1f);
                ChangeAnimationState("enemy_charge");
                transform.Find("HitBoxes").transform.Find("ChargeHitBox").gameObject.SetActive(true);
                int dir = 1;
                if (GameObject.Find("Player").transform.position.x - transform.position.x < 0)
                    dir = -1;
                rb.velocity = new Vector2(dir * 50, rb.velocity.y);
                yield return new WaitForSeconds(1.5f);
                sr.color = new Color(1, 1, 1, 1);
                break;
            case "GroundPound":
                ChangeAnimationState("enemy_groundpound");
                transform.Find("HitBoxes").transform.Find("GroundPoundHitBox").gameObject.SetActive(true);
                yield return new WaitForSeconds(1.5f);
                break;
            case "Impale":
                ChangeAnimationState("enemy_impale");
                transform.Find("HitBoxes").transform.Find("ImpaleHitBox").gameObject.SetActive(true);
                yield return new WaitForSeconds(1.7f);
                break;
            case "Vulnerable":
                ChangeAnimationState("enemy_vulnerable");
                transform.Find("HitBoxes").transform.Find("VulnerableHitBox").gameObject.SetActive(true);
                yield return new WaitForSeconds(5);
                break;
        }
        isAttacking = false;
        yield return null;
    }

    public void TakeDamage(int damage) {
        health.takeDmg(damage);
        enemyHealth -= damage;
        if (enemyHealth <= 0) {
            StartCoroutine(Die());
        }
    }

    public IEnumerator Die() {
        if (!hasDied) {
            hasDied = true;
            ChangeAnimationState("enemy_death");
            yield return new WaitForSeconds(0.5f);
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
