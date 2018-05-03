using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour {

    public GameObject TargetPoint; //point to move towards

    private NavMeshAgent agent;

    public float AttackRange = 10.0f;

    public float AttackDelay;

    public float SetAttackDelay;

    public bool isAttacking = false;

    public enum AIStates
    {
        ChaseState,
        AttackState,
        DeathState
    }

    public AIStates CurrentState;

    public Animator Anim;

    public Rigidbody RB;

    // Use this for initialization
    void Start() {
        agent = GetComponent<NavMeshAgent>(); // gets the compnent on the object that the script is attatched to
        agent.SetDestination(TargetPoint.transform.position);
    }

    public void updateAnim(Animator AnimationController)
    {
        if (AnimationController != null)
        {
            var localVel = transform.InverseTransformDirection(agent.velocity);
            
            AnimationController.SetFloat("ForwardSpeed", localVel.z);
        }
    }

    public bool InAttackRange()
    {
        float DistanceToTarget = agent.remainingDistance;

        if (DistanceToTarget <= AttackRange) // check if target is in attack range
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public void doChase(GameObject target)
    {
        if(target == null)
        {
            Debug.Log("Chase Can Not Run Due To Null Reference of Target");
            return;
        }

        if (agent.isStopped) { agent.isStopped = false; }

        if (InAttackRange())
        {
            CurrentState = AIStates.AttackState;
            return;
        }

        agent.SetDestination(target.transform.position);

    }

    public void doAttack(float DeltaTime)
    {
        if (!agent.isStopped) { agent.isStopped = true; }
        if (!InAttackRange() && !isAttacking)
        {
            CurrentState = AIStates.ChaseState;
            return;
        }

        if (SetAttackDelay <= 0)
        {
            Anim.SetTrigger("Attack");
            SetAttackDelay = AttackDelay;
        }

    }

    public void doDeath(bool isDead)
    {

    }

    // Update is called once per frame
    void Update () {

        updateAnim(Anim);

        switch (CurrentState)
        {
            case AIStates.ChaseState:
                doChase(TargetPoint);
                break;
            case AIStates.AttackState:
                doAttack(Time.deltaTime);
                break;
            case AIStates.DeathState:
                doDeath(false); //TODO: create death variable to check
                break;
        }

        SetAttackDelay -= Time.DeltaTime;

        agent.SetDestination(TargetPoint.transform.position);
	}

}
