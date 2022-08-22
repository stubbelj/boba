using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpAndDownAnimate : MonoBehaviour
{
    float originalY;

    public float floatStrength = 1;
    public float floatSpeed = 1.5F;

    // Start is called before the first frame update
    void Start()
    {
        this.originalY = this.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, originalY + ((float)Mathf.Sin(Time.time * floatSpeed) * floatStrength), transform.position.z);
    }
}
