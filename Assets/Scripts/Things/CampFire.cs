using System.Collections.Generic;
using UnityEngine;

public class CampFire : MonoBehaviour
{
    public float damageAmount;
    public float damageRate;

    private List<IDamagable> damagables = new List<IDamagable>();

    void DealDamage()
    {
        foreach (var damagable in damagables)
        {
            damagable.TakePhysicalDamage(damageAmount);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //���� IDamagable �������̽��� ����ϴ�, �������� ���� �� �ִ� ������Ʈ���
        if (other.TryGetComponent(out IDamagable damagable))
        {
            if (damagables.Count == 0) InvokeRepeating("DealDamage", 0, damageRate);
            damagables.Add(damagable);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IDamagable damagable))
        {
            damagables.Remove(damagable);
            if (damagables.Count == 0) CancelInvoke("DealDamage");
        }
    }
}
