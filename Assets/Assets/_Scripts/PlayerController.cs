using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Rigidbody RB;
    public Animator Anim;
    public float MovementSpeed = 100;

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
            RB.AddForce(MoveInput * MovementSpeed * DeltaTime);
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

	// Update is called once per frame
	void Update () {

        DT = Time.deltaTime;

        KeyInput();
        doMovement(DT, IP);
        updateAnim(Anim);
	}
}
