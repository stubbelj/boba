using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public HealthBar health;
    bool takingDamage = false;
    bool isAttacking = false;
    bool isGrounded = false;
    SpriteRenderer sr;
    Rigidbody2D rb;
    Animator anim;
    System.Random r = new System.Random();
    public int bossHealth = 100;

    private string currentState;

    List<string> attackList = new List<string>{"Stomp"};

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();

        health.totalHealth = bossHealth;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            TakeDamage(20);
        }
        //if (!isattacking) {
        //    StartCoroutine(Attack(attackList[r.Next(attackList.Count)]));
        //}
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
                break;
            case "GroundPound":
                ChangeAnimationState("enemy_groundpound");
                transform.Find("HitBoxes").transform.Find("GroundPoundHitBox").gameObject.SetActive(true);
                break;
            case "Impale":
                ChangeAnimationState("enemy_impale");
                transform.Find("HitBoxes").transform.Find("ImpaleHitBox").gameObject.SetActive(true);
                break;
            case "Vulnerable":
                ChangeAnimationState("enemy_vulnerable");
                transform.Find("HitBoxes").transform.Find("VulnerableHitBox").gameObject.SetActive(true);
                break;
        }
        isAttacking = false;
        yield return null;
    }

    public void TakeDamage(int damage) {
        health.takeDmg(damage);
        bossHealth -= damage;
        if (!takingDamage) {
            StartCoroutine(ChangeColor());
        }
        if (bossHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        GameObject.Find("Loader").GetComponent<LevelLoader>().LoadGameOver();
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
