using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
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
        if (other.tag == "Enemy")
            switch(GameObject.Find("Player").GetComponent<Player>().currentAttack) {
                case "Kick":
                    other.gameObject.GetComponent<Enemy>().TakeDamage(1);
                    break;
                case "Sting":
                    other.gameObject.GetComponent<Enemy>().TakeDamage(5);
                    break;
                case "Tongue":
                    other.gameObject.GetComponent<Enemy>().TakeDamage(2);
                    break;
            }
    }
}
