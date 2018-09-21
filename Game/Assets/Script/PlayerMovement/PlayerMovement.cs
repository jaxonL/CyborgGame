using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public PlayerMovementComponent controller;

    public Animator animator;

    public Collider2D jumpDisable;

	public float runSpeed = 40f;

	float horizontalMove = 0f;

	bool jump = false;
    bool landed = true;
	
	// Update is called once per frame
	void Update () {
		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

		if(Input.GetButtonDown("Jump"))
		{
			Debug.Log("jump");
			jump = true;
            animator.SetBool("Jump", true);
            jumpDisable.enabled = false;
            landed = false;
        }
	}

	void FixedUpdate()
	{
		controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
		jump = false;
    }

    public void OnLanding()
    {
        landed = true;
        animator.SetBool("Jump", false);
        jumpDisable.enabled = true;
    }
}
