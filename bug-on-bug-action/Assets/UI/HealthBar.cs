using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public float currentHealth;
    public float totalHealth;
    private float timer;
    public float speed = 2f;
    public Image frontHB;
    public Image backHB;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = totalHealth;
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, totalHealth);
        float fillFront = frontHB.fillAmount;
        float fillBack = backHB.fillAmount;
        float percent = currentHealth / totalHealth;
        if (fillBack > percent)
        {
            frontHB.fillAmount = percent;
            timer += Time.deltaTime;
            float complete = timer / speed;
            backHB.fillAmount = Mathf.Lerp(fillBack, percent, complete);
        }
    }

    public void takeDmg(int amount)
    {
        currentHealth -= amount;
        timer = 0f;
    }
}
