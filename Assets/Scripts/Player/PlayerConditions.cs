using System;
using System.Collections;
using UnityEngine;

public class PlayerConditions : MonoBehaviour, IDamagable
{
    public UICondition uICondition;

    Condition health { get => uICondition.health; }
    Condition hunger { get => uICondition.hunger; }
    Condition stamina { get => uICondition.stamina; }

    public float nohungerDecay;
    public float sprintStaminaDecay;
    public event Action onTakeDamage;
    private bool exhausted;
    private bool exhaustedStaminaBlocking;
    public float exhaustedBlockingTime = 5f;
    private void Update()
    {
        hunger.Subtract(hunger.passiveValue * Time.deltaTime);
        if (hunger.currentValue > 0f && !exhaustedStaminaBlocking) stamina.Add(stamina.passiveValue * Time.deltaTime);

        if (hunger.currentValue <= 0f)
        {
            health.Subtract(nohungerDecay * Time.deltaTime);
        }

        if (health.currentValue <= 0f)
        {
            //»ç¸Á
        }

        if (stamina.currentValue < 1f)
        {
            if (!exhausted) StartCoroutine(BlockStaminaRegeneration());
            exhausted = true;
        }

        if (stamina.currentValue > 10f)
        {
            exhausted = false;
        }
    }

    public void Heal(float amount)
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


    public bool CanSprint()
    {
        if (exhausted) return false;
        stamina.Subtract(sprintStaminaDecay * Time.deltaTime);
        return true;
    }

    private IEnumerator BlockStaminaRegeneration()
    {
        exhaustedStaminaBlocking = true;
        yield return new WaitForSeconds(exhaustedBlockingTime);
        exhaustedStaminaBlocking = false;
    }
}
