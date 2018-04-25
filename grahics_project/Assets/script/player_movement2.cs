using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement2 : MonoBehaviour
{

	
    public bool isGrounded;
    private float speed;
    public float rotSpeed;
    public float jumpHeight;
    //walk speed    
    private float w_speed = 0.5f;
    //rotation speed    
    private float rot_speed = 7.0f;
    Rigidbody rb;
    Animator anim;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        isGrounded = true; //indicate that we are in the ground 

    }

    void movementControl(string state)
    {
        switch (state)
        {
            case "walk":
                anim.SetBool("isWalkingForward", true);
                anim.SetBool("isWalkingBackward", false);
                anim.SetBool("isIdle", false);
                break;
            case "walkback":
                anim.SetBool("isWalkingForward", false);
                anim.SetBool("isWalkingBackward", true);
                anim.SetBool("isIdle", false);
                break;
            case "idle":
                anim.SetBool("isWalkingForward", false);
                anim.SetBool("isWalkingBackward", false);
                anim.SetBool("isIdle", true);
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (isGrounded)
        {             //moving forward and backward
            if (Input.GetKey(KeyCode.W))
            {
                speed = w_speed;
                movementControl("walk");
            }
            else if (Input.GetKey(KeyCode.S))
            {
                speed = w_speed;
                movementControl("walkback");
            }
            else
            {
                movementControl("idle");
            }
            //moving right and left   
            if (Input.GetKey(KeyCode.A))
            {
                rotSpeed = rot_speed;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                rotSpeed = rot_speed;
            }
            else
            {
                rotSpeed = 0;
            }

        }
        var z = Input.GetAxis("Vertical") * speed;
        var y = Input.GetAxis("Horizontal") * rotSpeed;
        transform.Translate(0, 0, z);
        transform.Rotate(0, y, 0);
        //jumping function       
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            anim.SetTrigger("jumping");
            rb.AddForce(0, jumpHeight * Time.deltaTime, 0);
            //isGrounded = false;
        }

    }
    void OnCollisionEnter()
    {
        isGrounded = true;

    }
}
