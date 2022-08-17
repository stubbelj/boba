using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    private float start, length, distanceMoved, loop;
    public GameObject cam;
    public float effectAmount;

    // Start is called before the first frame update
    void Start()
    {
        start = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        loop = cam.transform.position.x * (1 - effectAmount);
        distanceMoved = cam.transform.position.x * effectAmount;
        transform.position = new Vector3(start + distanceMoved, transform.position.y, transform.position.z);

        if (loop > start + length)
        {
            start += length;
        } else if (loop < start - length)
        {
            start -= length;
        }
    }
}
