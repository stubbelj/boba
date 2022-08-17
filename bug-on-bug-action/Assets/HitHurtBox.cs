using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitHurtBox : MonoBehaviour
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
            GameObject.Find("Player").GetComponent<Player>().TakeDamage(1);
    }
}
