using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBox : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
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
    }
}
