using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverlayManager : MonoBehaviour {

    public Image Healthbar;

    public HealthComponent PlayerHealth;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        Healthbar.fillAmount = PlayerHealth.CurrentHealth / PlayerHealth.MaxHealth;

	}
}
