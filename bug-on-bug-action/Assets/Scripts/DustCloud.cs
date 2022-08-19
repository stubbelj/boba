using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustCloud : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        StartCoroutine(PlayAnim());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator PlayAnim() {
        anim.Play("dustcloud");
        yield return new WaitForSeconds(0.4f);
        Destroy(gameObject);
    }

    bool AnimatorIsPlaying(){
        return anim.GetCurrentAnimatorStateInfo(0).length > anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }
}
