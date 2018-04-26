using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour {

    public float CurrentHealth;

    public float MaxHealth = 100;

    // public void TakeDamage(int amount)
    // {
    //     currentHealth -= amount;
    //     if (currentHealth <= 0)
    //     {
    //         currentHealth = 0;
    //         Debug.Log("Dead!");
    //     }
    // }

	// Use this for initialization
	void Start () {

        CurrentHealth = MaxHealth;

	}

    public void TakeDamage(float DamageAmount)
    {
        CurrentHealth -= DamageAmount;
    }
	
	// Update is called once per frame
	void Update () {
		
        if(CurrentHealth <= 0)
        {
            gameObject.SetActive(false);
            Debug.Log("Dead!");
        }

	}
}
