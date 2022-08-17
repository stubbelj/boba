using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //hitboxes and hurtboxes - index 0 is hitbox for attack 1, index 1 is hurtbox for attack 1, etc.
    public GameObject[] attackBoxes;
    //vectors for hitbox and hurtbox movement - index 0 is hitbox for attack 1, index 1 is hurtbox for attack 1, etc.
    public Vector2[] attackBoxVectors = {new Vector2(5f, 0), new Vector2(5f, 0)};
    Rigidbody2D rb;
    int playerHealth = 100;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(playerHealth);

        if (Input.GetKey(KeyCode.A)) {
            rb.velocity = new Vector2(-5, 0);
        }
        if (Input.GetKey(KeyCode.D)) {
            rb.velocity = new Vector2(5, 0);
        }

        if(Input.GetKeyDown(KeyCode.K))
            StartCoroutine(HitBoxChange(0));
    }

    public void TakeDamage(int damage) {
        Debug.Log("damaged");
        //anim
        //ui change
        playerHealth -= damage;
    }

    public IEnumerator HitBoxChange(int attackNum) {
        //change animation
        GameObject newHitBox = GameObject.Instantiate(attackBoxes[attackNum * 2], gameObject.transform.position, Quaternion.identity);
        newHitBox.GetComponent<Rigidbody2D>().velocity = attackBoxVectors[attackNum * 2];
        Destroy(newHitBox, 3f);
        GameObject newHurtBox = GameObject.Instantiate(attackBoxes[attackNum * 2 + 1], gameObject.transform.position, Quaternion.identity);
        newHurtBox.GetComponent<Rigidbody2D>().velocity = attackBoxVectors[attackNum * 2 + 1];
        Destroy(newHurtBox, 3f);
        yield return new WaitForSeconds(3);
    }
}
