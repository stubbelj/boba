using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public Sprite jumpSprite;
    public Sprite neutralSprite;
    public GameObject beeCorpse;
    public GameObject dustCloud;


    bool isAttacking = false;
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
        
        rb.velocity = new Vector2(2, 2);
    }

    // Update is called once per frame
    void Update()
    {

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
        if (!isAttacking) {
            if (Input.GetKeyDown(KeyCode.I))
                StartCoroutine(Attack("Kick"));
        }
        if (!isAttacking) {
            if (Input.GetKeyDown(KeyCode.O))
                StartCoroutine(Attack("Sting"));
        }
        if (!isAttacking) {
            if (Input.GetKeyDown(KeyCode.P))
                StartCoroutine(Attack("Tongue"));
        }
    }

    public IEnumerator Attack(string attackName) {
        isAttacking = true;
        switch (attackName) {
            case "Kick":
                ChangeAnimationState("player_kick");
                transform.Find("HitBoxes").transform.Find("KickHitBox").gameObject.SetActive(true);
                break;
            case "Sting":
                ChangeAnimationState("player_sting");
                transform.Find("HitBoxes").transform.Find("StingHitBox").gameObject.SetActive(true);
                break;
            case "Tongue":
                ChangeAnimationState("player_tongue");
                transform.Find("HitBoxes").transform.Find("TongueHitBox").gameObject.SetActive(true);
                break;
        }
        isAttacking = false;
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
