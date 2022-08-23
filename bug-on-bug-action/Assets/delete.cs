using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class delete : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void destroy()
    {
        // wait();
        Destroy(gameObject);
    }

    private IEnumerator wait()
    {
        yield return new WaitForSeconds(0.5F);
    }
}
