using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempMove : MonoBehaviour
{
    public float moveValue = 0.5F;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.position = new Vector3(transform.position.x - moveValue, transform.position.y, transform.position.z);
        } else if (Input.GetKey(KeyCode.D))
        {
            transform.position = new Vector3(transform.position.x + moveValue, transform.position.y, transform.position.z);
        }
    }
}
