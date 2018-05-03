using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDamageHitbox : MonoBehaviour {

    public float DamageAmount;

    public void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>())
        {
            other.GetComponent<HealthComponent>().TakeDamage(DamageAmount);
        }
    }
}
