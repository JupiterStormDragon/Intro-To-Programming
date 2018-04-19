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
    public float SmoothDamp = 10;
    public GameObject bullet;
    public bool Attacking;
    public float SetAttackDelay = .01f;


    public Vector3 IP; // Movement Input

    float DT;

    public Camera cam;

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

    public void doLook() //Rotate player towards the mouse cursor
    {
        RaycastHit hit;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition); // creating ray from mouse point on screen

        if (Physics.Raycast(ray, out hit, 1000000000)) // casting out a ray and populating our hit value
        {
            Vector3 forward = (transform.position - hit.point) * -1; // getting the direction between our position and our hitpoint position
            forward.y = 0; // zeroing out the y to stay upright
            forward.Normalize(); // normalize to calculate direction
            transform.forward = Vector3.MoveTowards(transform.forward, forward, Time.deltaTime * SmoothDamp); // move our forward towards the direction between the positions
        }
    }

    public void doOutOfCombat()
    {
        doMovement(DT, IP);
    }

    public void doCombat()
    {
        if (Attacking)
            Instantiate(bullet, transform.position, transform.rotation);
            
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
        doLook();
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
