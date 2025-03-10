using System;
using UnityEngine;

public class PlayerConditions : MonoBehaviour, IDamagable
{
    public UICondition uICondition;

    Condition health { get => uICondition.health; }
    Condition hunger { get => uICondition.hunger; }
    Condition stamina { get => uICondition.stamina; }

    public float nohungerDecay;
    public event Action onTakeDamage;

    private void Update()
    {
        hunger.Subtract(hunger.passiveValue * Time.deltaTime);
        stamina.Add(stamina.passiveValue * Time.deltaTime);

        if (hunger.currentValue <= 0f)
        {
            health.Subtract(nohungerDecay * Time.deltaTime);
        }

        if (health.currentValue <= 0f)
        {
            //»ç¸Á
        }
    }

    public void Heal (float amount)
    {
        health.Add(amount);
    }
    public void Eat(float amount)
    {
        hunger.Add(amount);
    }
    public void Rest(float amount)
    {
        stamina.Add(amount);
    }

    public void TakePhysicalDamage(float damageAmount)
    {
        health.Subtract(damageAmount);
        onTakeDamage?.Invoke();
    }
}
