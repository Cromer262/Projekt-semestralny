using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public float adjustRate = 120;
    public float maxHealth;
    public float currentHealth;

    public Slider healthSlider;
    public Image fillImage;

    public TextMeshProUGUI txt_currentHealth;
    public TextMeshProUGUI txt_maxHealth;
    public Gradient colorGradient; 

    // Start is called before the first frame update
    void Awake()
    {
       currentHealth = maxHealth;
       healthSlider.value = NormalizedHealth();
       fillImage.color = colorGradient.Evaluate(NormalizedHealth());
       txt_currentHealth.text = currentHealth.ToString("F0");
       txt_maxHealth.text = maxHealth.ToString("F0");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
            StartCoroutine(TakeDamage(10));
    }

    IEnumerator TakeDamage(float damage)
    {
        float originHealth = currentHealth;
        float targetHealth = currentHealth - damage;

        float timeToAdjust = damage / adjustRate;
        float timeElapsed = 0;

        while (timeElapsed < timeToAdjust)
        {
            float t = timeElapsed / timeToAdjust;
            t = t * t * (3f - 2f * t);
            currentHealth = Mathf.Lerp(originHealth, targetHealth, t);
            healthSlider.value = NormalizedHealth();
            fillImage.color = colorGradient.Evaluate(NormalizedHealth());

            if(currentHealth <= 0)
            {
                currentHealth = 0;
                txt_currentHealth.text = currentHealth.ToString("F0");
                yield break;
            }
            txt_currentHealth.text = txt_currentHealth.text = currentHealth.ToString("F0");
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        currentHealth = targetHealth;
        fillImage.color = colorGradient.Evaluate(NormalizedHealth());
        txt_currentHealth.text = txt_currentHealth.text = currentHealth.ToString("F0");
    }

    float NormalizedHealth()
    {
        return currentHealth / maxHealth;
    }
}
