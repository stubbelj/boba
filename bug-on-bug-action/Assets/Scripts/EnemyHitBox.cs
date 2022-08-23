using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBox : MonoBehaviour
{
    GameObject player;
    GameObject enemy;

    bool currentlyDamaging = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        enemy = GameObject.Find("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        if(!currentlyDamaging) {
            StartCoroutine(DamagePlayer());
        }
    }

    public IEnumerator DamagePlayer() {
        currentlyDamaging = true;
        if (Mathf.Abs(player.transform.position.x - enemy.transform.position.x) < 25) {
            switch (enemy.GetComponent<Enemy>().currentAttack) {
                case "Stomp":
                    yield return new WaitForSeconds(0.5f);
                    player.gameObject.GetComponent<Player>().TakeDamage(34, GameObject.Find("Enemy").transform.position);
                    break;
                //case "Charge":
               //     player.gameObject.GetComponent<Player>().TakeDamage(50, GameObject.Find("Enemy").transform.position);
                  //  break;
                case "Impale":
                    yield return new WaitForSeconds(0.5f);
                    player.gameObject.GetComponent<Player>().TakeDamage(90, GameObject.Find("Enemy").transform.position);
                    break;
                case "GroundPound":
                    yield return new WaitForSeconds(1.0f);
                    player.gameObject.GetComponent<Player>().TakeDamage(90, GameObject.Find("Enemy").transform.position);
                    break;

            }
        }
        currentlyDamaging = false;
    }

    /*private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player")
            switch(GameObject.Find("Player").GetComponent<Player>().currentAttack) {
                case "Stomp":
                    other.gameObject.GetComponent<Player>().TakeDamage(34, GameObject.Find("Enemy").transform.position);
                    break;
                case "Charge":
                    other.gameObject.GetComponent<Player>().TakeDamage(50, GameObject.Find("Enemy").transform.position);
                    break;
                case "Impale":
                    other.gameObject.GetComponent<Player>().TakeDamage(90, GameObject.Find("Enemy").transform.position);
                    break;
                case "GroundPound":
                    other.gameObject.GetComponent<Player>().TakeDamage(90, GameObject.Find("Enemy").transform.position);
                    break;
            }
    }*/
}
