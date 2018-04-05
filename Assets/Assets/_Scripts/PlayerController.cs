using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public enum States
    {
        OutOfCombatState,
        CombatState
    }

    States CurrentState;

    private Rigidbody RB;
    public Animator Anim;
    public float MovementSpeed = 200;

    public Vector3 IP; // Movement Input

    float DT;

	// Use this for initialization
	void Start () {

        RB = GetComponent<Rigidbody>(); //Gets Rigidbody on object
        // anim passed in through editor
	}
	
    public void KeyInput()
    {
        IP.x = Input.GetAxisRaw("Horizontal");
        IP.z = Input.GetAxisRaw("Vertical");
    }

    public void doMovement(float DeltaTime, Vector3 MoveInput)
    {
        if (RB != null)
        {
            //RB.AddForce(MoveInput * MovementSpeed * DeltaTime);
            float StoredYVelocity = RB.velocity.y; //Stores Y Velocity
            Vector3 NewVelocity = MoveInput * MovementSpeed * DeltaTime; //Gets new velocity
            Vector3 Vel = new Vector3(NewVelocity.x, StoredYVelocity, NewVelocity.z); //Creates new velocity keeping old Y velocity

            RB.velocity = Vel;
        }
    }

    public void updateAnim(Animator AnimationController)
    {
        if (AnimationController != null)
        {
            var localVel = transform.InverseTransformDirection(RB.velocity);
            AnimationController.SetFloat("ForwardSpeed", localVel.z);
            AnimationController.SetFloat("RightSpeed", localVel.x);
        }
    }

    public void doOutOfCombat()
    {
        doMovement(DT, IP);
    }

    public void doCombat()
    {

    }

    // Update is called once per frame
    void Update()
    {

        DT = Time.deltaTime;
        KeyInput();

        updateAnim(Anim);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            MovementSpeed = 400;
        }
        else
        {
            MovementSpeed = 200;
        }

    }

    private void FixedUpdate()
    {

            switch(CurrentState)
            {
                case States.OutOfCombatState:
                    doOutOfCombat();
                    break;

                case States.CombatState:
                    doCombat();
                    break;
            }

            KeyInput();
            doMovement(DT, IP);
            updateAnim(Anim);
	    }

}
