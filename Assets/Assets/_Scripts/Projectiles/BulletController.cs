using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    public GameObject Owner;

    public float DamageAmount = 10; // how much damage bullet does

    public float BulletSpeed = 50; // how fast bullet shoots

    public float BulletDelay = .2f; // how often bullet shoots

    public void OnTriggerEnter(Collider other) // other == object we collide with
    {
        if (other.gameObject != Owner)
        {
            HealthComponent Temp = other.GetComponent<HealthComponent>();
            if (Temp != null)
            {
                Temp.TakeDamage(DamageAmount);
            }

            Destroy(gameObject);
        }
        
    }

}