using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public float MaxHealth = 10;
    public float currentHealth;
    public bool UpdateOnUpdate;
    public bool isInvulnerable;
    [HideInInspector]public bool hitReg;

    public Animator objAni;
    public Slider healthBarSlider;
    public HealthBar healthbarHead;

    private void Start()
    {
        currentHealth = MaxHealth;
        if(GetComponent<Animator>() != null)
        {
            objAni = GetComponent<Animator>();
        }
    }

    private void Update()
    {
        if(UpdateOnUpdate) UpdateBar(MaxHealth, currentHealth);

        if(currentHealth <= 0 && healthbarHead == null)
        {
            gameObject.SetActive(false);
        }

        hitReg = false;
    }

    public void AlterHealth(float Change)
    {

        if (isInvulnerable)
        {
            //play dink noise
            hitReg = true;
            return;
        }
        else
        {
            hitReg = true;
        }
            
        if (GetComponent<Player>())
        {
            FindAnyObjectByType<ScreenEffects>().PlayerHit();
        }

        if (healthbarHead != null)
            healthbarHead.AlterHealth(Change);
        else
            currentHealth += Change;

        if(Change < 0 && objAni != null)
        {
            objAni.SetTrigger("Hit");
        }
    }

    public void UpdateBar(float maxHealth, float currentHealth)
    {
        healthBarSlider.maxValue = maxHealth;
        healthBarSlider.value = currentHealth;
    }

}
