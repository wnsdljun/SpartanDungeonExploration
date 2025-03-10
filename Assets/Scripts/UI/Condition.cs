using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    //각각의 상태를 나타내는데 사용.
    public float currentValue;
    public float maxValue;
    public float startValue;
    public float passiveValue;
    public Image displayBar;

    private void Start()
    {
        currentValue = startValue;
    }

    private void Update()
    {
        displayBar.fillAmount = GetPercentage();
    }

    public float GetPercentage()
    {
        return currentValue / maxValue;
    }

    public void Add(float amount)
    {
        currentValue = Mathf.Min(currentValue + amount, maxValue);
    }

    public void Subtract(float amount)
    {
        currentValue = Mathf.Max(currentValue - amount, 0.0f);
    }
}
