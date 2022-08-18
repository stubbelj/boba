using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    //hitboxes and hurtboxes - index 0 is hitbox for attack 1, index 1 is hurtbox for attack 1, etc.
    public GameObject[] attackBoxes;
    //vectors for hitbox and hurtbox movement - index 0 is hitbox for attack 1, index 1 is hurtbox for attack 1, etc.
    public Vector2[] attackBoxVectors = {new Vector2(5f, 0), new Vector2(5f, 0)};

    public Sprite[] attackSprites;

    public Sprite jumpSprite;
    public Sprite neutralSprite;
    public GameObject beeCorpse;


    bool isattacking = false;
    bool isGrounded = false;
    SpriteRenderer sr;
    Rigidbody2D rb;
    Animator anim;
    
    private string currentState;
    int playerHealth = 100;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(PlayerPrefs.GetInt("deathCount"));

        if (Input.GetKey(KeyCode.A)) {
            sr.flipX = true;
            rb.velocity = new Vector2(-50, rb.velocity.y);
            if (isGrounded && (!AnimatorIsPlaying() || AnimatorIsPlaying("player_idle")))
                ChangeAnimationState("player_walk");
        } else if (Input.GetKey(KeyCode.D)) {
            sr.flipX = false;
            rb.velocity = new Vector2(50, rb.velocity.y);
            if (isGrounded && (!AnimatorIsPlaying() || AnimatorIsPlaying("player_idle")))
                ChangeAnimationState("player_walk");
        }
        
        if (isGrounded) {
            if (!AnimatorIsPlaying()) {
                //anim.SetTrigger("idle");
                ChangeAnimationState("player_idle");
            }
            if (Input.GetKeyDown(KeyCode.W)) {
               // anim.SetTrigger("jump");
               ChangeAnimationState("player_jump");
                rb.velocity = new Vector2(rb.velocity.x, 50);
                isGrounded = false;
            }
        } else {
            if (!AnimatorIsPlaying()) {
                //anim.SetTrigger("jump");
                ChangeAnimationState("player_jump");
            }
        }

        //attacks here
        if (!isattacking) {
            if (Input.GetKeyDown(KeyCode.I))
                StartCoroutine(Attack("Kick"));
        }
    }

    public IEnumerator Attack(string attackName) {
        isattacking = true;
        switch (attackName) {
            case "Kick":
                ChangeAnimationState("player_kick");
                HitBoxChange(0, 3, 3);
                break;
        }
        isattacking = false;
        yield return null;
    }

    public void TakeDamage(int damage, Vector2 source) {
        //anim
        //ui change
        float mag = Mathf.Sqrt(Mathf.Pow(source.x, 2) + Mathf.Pow(source.y, 2));
        rb.velocity = new Vector2((source.x / mag) * 50, (source.y / mag) * 50);
        playerHealth -= damage;
        if (playerHealth <= 0) {
            Die();
        }
    }

    public void Die() {
        ///needs to be initialized to 0 to avoid ranodm data
        PlayerPrefs.SetInt("deathCount", PlayerPrefs.GetInt("deathCount") + 1);
        StartCoroutine(GameObject.Find("Loader").GetComponent<LevelLoader>().LoadLevel(0));
    }

    public IEnumerator HitBoxChange(int attackNum, int hitTime, int hurtTime) {
        GameObject newHitBox = GameObject.Instantiate(attackBoxes[attackNum * 2], gameObject.transform.position, Quaternion.identity);
        newHitBox.GetComponent<Rigidbody2D>().velocity = attackBoxVectors[attackNum * 2];
        Destroy(newHitBox, hitTime);
        GameObject newHurtBox = GameObject.Instantiate(attackBoxes[attackNum * 2 + 1], gameObject.transform.position, Quaternion.identity);
        newHurtBox.GetComponent<Rigidbody2D>().velocity = attackBoxVectors[attackNum * 2 + 1];
        Destroy(newHurtBox, hurtTime);
        yield return new WaitForSeconds(3);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Ground") {
            isGrounded = true;
            if (!isattacking)
                sr.sprite = neutralSprite;
        } else if (other.gameObject.tag == "Enemy") {
            TakeDamage(1, other.gameObject.transform.position);
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

        anim.Play(newState);
        currentState = newState;
    }

}
