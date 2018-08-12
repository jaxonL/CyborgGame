using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public PlayerMovementComponent controller;

	public float runSpeed = 40f;

	float horizontalMove = 0f;

	bool jump = false;
	
	// Update is called once per frame
	void Update () {
		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

		if(Input.GetButtonDown("Jump"))
		{
			Debug.Log("jump");
			jump = true;
		}
	}

	void FixedUpdate()
	{
		controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
		jump = false;
	}
}
