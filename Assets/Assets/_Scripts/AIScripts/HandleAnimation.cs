using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleAnimation : MonoBehaviour {

    public AIController OurAIController;

    public GameObject Hitbox;

	public void StartAttack()
    {
        if (Hitbox == null) { return; }
        Hitbox.SetActive(true);
    }

    public void EndDamage()
    {
        if (Hitbox == null) { return; }
        Hitbox.SetActive(false);
    }

    public void EndAttack()
    {
        if (OurAIController == null) { return; }

        OurAIController.SetAttackDelay = OurAIController.AttackDelay;
        OurAIController.isAttacking = false;
        
    }


}
