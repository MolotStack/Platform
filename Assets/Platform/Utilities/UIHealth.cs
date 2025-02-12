using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealth : MonoBehaviour
{
    [SerializeField]
    private Slider healthSlider;
    [SerializeField]
    private HealthComponent healthComponent;

    private void Awake()
    {
        healthComponent = GetComponentInParent<HealthComponent>();

        if (healthComponent == null)
        {
            healthSlider.gameObject.SetActive(false);
        }
        else 
        {
            healthComponent.OnChangedHealth += SetSliderHealth;
            healthComponent.OnChangedMaxHealth += SetSliderMaxHealth;

            healthSlider.maxValue = healthComponent.CurrentMaxHealth;
            healthSlider.value = healthComponent.CurrentHealth;

        }
    }

    private void OnDisable()
    {
        if (healthComponent != null)
        {
            healthComponent.OnChangedHealth -= SetSliderHealth;
            healthComponent.OnChangedMaxHealth -= SetSliderMaxHealth;
        }
    }

    private void SetSliderHealth(float currentHealth) 
    {
        healthSlider.value = currentHealth;
    }

    private void SetSliderMaxHealth(float currentMaxHealth)
    {
        healthSlider.maxValue = currentMaxHealth;
    }
}
