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


    bool isattacking = false;
    bool isGrounded = false;
    SpriteRenderer sr;
    Rigidbody2D rb;
    int playerHealth = 100;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.A)) {
            rb.velocity = new Vector2(-50, rb.velocity.y);
        }
        if (Input.GetKey(KeyCode.D)) {
            rb.velocity = new Vector2(50, rb.velocity.y);
        }

        if (isGrounded) {
            if (Input.GetKey(KeyCode.W)) {
                sr.sprite = jumpSprite;
                rb.velocity = new Vector2(rb.velocity.x, 50);
                isGrounded = false;
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
                //kick sound effect
                sr.sprite = attackSprites[0];
                HitBoxChange(0, 3, 3);
                yield return new WaitForSeconds(3);
                if (isGrounded)
                    sr.sprite = neutralSprite;
                else
                    sr.sprite = jumpSprite;
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
}
