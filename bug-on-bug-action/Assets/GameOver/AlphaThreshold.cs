using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlphaThreshold : MonoBehaviour
{
    public Image buttonInQuestion;

    // Start is called before the first frame update
    void Start()
    {
        buttonInQuestion.alphaHitTestMinimumThreshold = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
