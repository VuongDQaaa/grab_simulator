using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [Header("Component")]
    public TextMeshProUGUI healthText;
    public Image healthBar;
    public static PlayerHealth instance;
    [Header("Health")]
    [SerializeField] SO healthSO;
    float health, maxHealth = 100;
    float lerpSpeed;
    private void Awake() {
        if(instance == null){
            instance = this;
        }
    }
    private void Start()
    {
        health = maxHealth;
        
    }
    private void Update()
    {
        healthText.text = health + "%";
        if (health > maxHealth) health = maxHealth;

        lerpSpeed = 5f * Time.deltaTime;

        HealthBarFiller();
        ColorChanger();
        healthSO.value = health;
    }

    void HealthBarFiller()
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, health / maxHealth, lerpSpeed);
    }

    void ColorChanger()
    {
        Color healthColor = Color.Lerp(Color.red, Color.green, (health / maxHealth));

        healthBar.color = healthColor;

    }

    public void TakeDamage(float damagePoints)
    {
        if (health > 0)
            health -= damagePoints;
        if(health <= 0)
            Invoke("Delay", 1f);
    }
    public void TakeHeal(float healingPoints)
    {
        if (health < maxHealth)
            health += healingPoints;

    }
}
