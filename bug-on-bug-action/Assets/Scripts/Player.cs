using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public HealthBar health;
    public Sprite jumpSprite;
    public Sprite neutralSprite;
    public GameObject beeCorpse;
    public GameObject dustCloud;
    public LevelLoader loader;

    private string[] modeList = {"", "_mustache", "_star", "_fleur"};
    private string mode = "";

    public string currentAttack;
    private bool hasDied = false;

    bool isAttacking = false;
    bool isGrounded = false;
    SpriteRenderer sr;
    Rigidbody2D rb;
    Animator anim;
    System.Random r = new System.Random();
    
    private string currentState;
    int playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = (int) health.totalHealth;

        if (SceneVariables.mode != null)
        {
            mode = SceneVariables.mode;
        } else
        {
            mode = modeList[r.Next(0, 3)];
            SceneVariables.mode = mode;
        }
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasDied) {
            if (Input.GetKey(KeyCode.A)) {
                sr.flipX = true;
                rb.velocity = new Vector2(-50, rb.velocity.y);
                if (isGrounded && (!AnimatorIsPlaying() || AnimatorIsPlaying("player_idle" + mode)))
                    ChangeAnimationState("player_walk" + mode);
            }
            else if (Input.GetKey(KeyCode.D)) {
                sr.flipX = false;
                rb.velocity = new Vector2(50, rb.velocity.y);
                if (isGrounded && (!AnimatorIsPlaying() || AnimatorIsPlaying("player_idle" + mode)))
                    ChangeAnimationState("player_walk" + mode);
            }

            if (isGrounded) {
                if (!AnimatorIsPlaying()) {
                    //anim.SetTrigger("idle");
                    ChangeAnimationState("player_idle" + mode);
                }
                if (Input.GetKeyDown(KeyCode.W)) {
                    // anim.SetTrigger("jump");
                    ChangeAnimationState("player_jump" + mode);
                    rb.velocity = new Vector2(rb.velocity.x, 50);
                    isGrounded = false;
                }
            }
            else {
                if (!AnimatorIsPlaying()) {
                    //anim.SetTrigger("jump");
                    ChangeAnimationState("player_jump" + mode);
                }
            }

            //attacks here
            if (!isAttacking) {
                if (Input.GetKeyDown(KeyCode.J))
                    StartCoroutine(Attack("Kick"));
            }
            if (!isAttacking) {
                if (Input.GetKeyDown(KeyCode.L))
                    StartCoroutine(Attack("Sting"));
            }
            if (!isAttacking) {
                if (Input.GetKeyDown(KeyCode.I))
                    StartCoroutine(Attack("Tongue"));
            }
        }
    }

    public IEnumerator Attack(string attackName) {
        isAttacking = true;
        switch (attackName) {
            case "Kick":
                currentAttack = "Kick";
                ChangeAnimationState("player_kick" + mode);
                transform.Find("HitBoxes").transform.Find("KickHitBox").gameObject.SetActive(true);
                break;
            case "Sting":
                currentAttack = "Sting";
                ChangeAnimationState("player_sting" + mode);
                transform.Find("HitBoxes").transform.Find("StingHitBox").gameObject.SetActive(true);
                yield return new WaitForSeconds(0.3f);
                TakeDamage(100, transform.position);
                break;
            case "Tongue":
                currentAttack = "Tongue";
                ChangeAnimationState("player_tongue" + mode);
                transform.Find("HitBoxes").transform.Find("TongueHitBox").gameObject.SetActive(true);
                break;
        }
        isAttacking = false;
        yield return null;
    }

    public void TakeDamage(int damage, Vector2 source) {
        //anim
        //ui change
        health.takeDmg(damage);
        float mag = Mathf.Sqrt(Mathf.Pow(source.x, 2) + Mathf.Pow(source.y, 2));
        rb.velocity = new Vector2((source.x / mag) * 50, (source.y / mag) * 50);
        playerHealth -= damage;
        if (playerHealth <= 0) {
            StartCoroutine(Die());
        }
    }

    public IEnumerator Die() {
        ChangeAnimationState("player_death");
        yield return new WaitForSeconds(0.5f);
        if (!hasDied) {
            hasDied = true;
            SceneVariables.deathCount++;
            SceneVariables.mode = null;
            loader.LoadGameOver();
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Death Zone")
        {
            TakeDamage(100, new Vector2(0, 0));
        }
    }


    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Ground") {
            if (!isGrounded) {
                GameObject.Instantiate(dustCloud, new Vector2(transform.position.x, transform.position.y + 1), Quaternion.identity);
            }
            isGrounded = true;
            if (!isAttacking)
                sr.sprite = neutralSprite;
        } else if (other.gameObject.tag == "Enemy") {
            TakeDamage(1, other.gameObject.transform.position);
        } else {
            Debug.Log(other.gameObject.tag);
        }

    }

    bool AnimatorIsPlaying(){
        return anim.GetCurrentAnimatorStateInfo(0).length > anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    bool AnimatorIsPlaying(string stateName){
        return AnimatorIsPlaying() && anim.GetCurrentAnimatorStateInfo(0).IsName(stateName);
    }

    void ChangeAnimationState(string newState) {
        if (currentState == newState) return;
        if (isAttacking) {
            foreach (Transform hitbox in transform.Find("HitBoxes")) {
                hitbox.gameObject.SetActive(false);
            }
        }

        anim.Play(newState);
        currentState = newState;
    }

}
