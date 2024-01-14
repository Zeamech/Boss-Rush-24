using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public float MaxHealth = 10;
    public bool UpdateOnUpdate;

    public Slider healthBarSlider;
    [SerializeField] private HealthBar healthbarHead;
    [SerializeField] private float currentHealth;

    private void Start()
    {
        currentHealth = MaxHealth;
    }

    private void Update()
    {
        if(UpdateOnUpdate) UpdateBar(MaxHealth, currentHealth);

        if(currentHealth <= 0 && healthbarHead == null)
        {
            gameObject.SetActive(false);
        }
    }

    public void AlterHealth(float Change)
    {
        if(healthbarHead != null)
            healthbarHead.AlterHealth(Change);
        else
            currentHealth += Change;
    }

    public void UpdateBar(float maxHealth, float currentHealth)
    {
        healthBarSlider.maxValue = maxHealth;
        healthBarSlider.value = currentHealth;
    }

}
