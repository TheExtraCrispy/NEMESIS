using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEnergy : MonoBehaviour
{
    [SerializeField] Image energyBar;
    [SerializeField] float maxEnergy;
    [SerializeField] float energyRegenRate;
    [SerializeField] float energyRegenCooldown;
    [SerializeField] float energyRegenRampUp;
   

    private float energy;
    private float realRegenRate;
    private float timeToRegen;
    private bool regenerating = false;

    public bool UseEnergy(float amount)
    {
        if(energy >= amount)
        {
            energy -= amount;
            timeToRegen = Time.time + energyRegenCooldown;
            regenerating = false;
            realRegenRate = 0;
            return true;
        }
        return false;
    }

    private void Start()
    {
        energy = maxEnergy;
    }

    private void Update()
    {
        if (!regenerating)
        {
            if(Time.time >= timeToRegen)
            {
                regenerating = true;
            }
        }
        else
        {
            energy =  Mathf.Clamp(energy + realRegenRate * Time.deltaTime, 0, maxEnergy);
        }
        realRegenRate = Mathf.Clamp(realRegenRate + energyRegenRampUp * Time.deltaTime, 0, energyRegenRate);
        energyBar.fillAmount = energy / maxEnergy;
    }
}
