using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LaunchPad : MonoBehaviour
{
    public float force = 500f;
    public float rechargeTime = 10f;
    private bool canExpel = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Rigidbody rigidbody))
        {
            if (canExpel)
            {
                rigidbody.AddForce(Vector3.up * force, ForceMode.Impulse);
                BlockExpel();
            }
        }
    }

    void BlockExpel()
    {
        canExpel = false;
        StartCoroutine(BlockExpelCoroutine());
    }

    IEnumerator BlockExpelCoroutine()
    {
        yield return new WaitForSeconds(rechargeTime);
        canExpel = true;
    }
}
