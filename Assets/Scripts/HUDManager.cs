using System;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public DaveStats daveStats;
    public Image healthFillImage;
    public Image manaFillImage;
    public Image energyFillImage;
    
    private void OnEnable()
    {
        daveStats.onHealthChange.AddListener(UpdateHealthBar);
        daveStats.onManaChange.AddListener(UpdateManaBar);
        daveStats.onEnergyChange.AddListener(UpdateEnergyBar);
    }
    
    private void OnDisable()
    {
        daveStats.onHealthChange.RemoveListener(UpdateHealthBar);
        daveStats.onManaChange.RemoveListener(UpdateManaBar);
        daveStats.onEnergyChange.RemoveListener(UpdateEnergyBar);
    }
    
    private void UpdateHealthBar(float health)
    {
        healthFillImage.fillAmount = health;
    }
    
    private void UpdateManaBar(float mana)
    {
        manaFillImage.fillAmount = mana;
    }
    
    private void UpdateEnergyBar(float energy)
    {
        energyFillImage.fillAmount = energy;
    }
}