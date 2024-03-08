using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WizardController : MonoBehaviour
{
    private float wizardSpeed = 10f;
    private Rigidbody2D rigid;
    public int jumpPower = 30;
    private bool isGrounded = true;
    public Animator anim = null;
    public Vector3 standard_direction = new Vector3(1,1,1);
    public Vector3 reflect_direction = new Vector3(-1,1,1);
    
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //move
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.Translate(-wizardSpeed*Time.deltaTime,0,0);
            this.transform.localScale = reflect_direction;
            anim.SetBool("isRun", true);
            if(!isGrounded)
            {
                anim.SetBool("isRun", false);
            }
        }
         else if(Input.GetKeyUp(KeyCode.LeftArrow))
        {
            anim.SetBool("isRun",false);
        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.Translate(wizardSpeed*Time.deltaTime,0,0);
            this.transform.localScale = standard_direction;
            anim.SetBool("isRun", true);
            if(!isGrounded)
            {
                anim.SetBool("isRun", false);
            }
            
        }
        else if(Input.GetKeyUp(KeyCode.RightArrow))
        {
            anim.SetBool("isRun",false);
        }

        //jump
        if(Input.GetKeyDown(KeyCode.Space)&&isGrounded)
        {
            rigid.AddForce(Vector2.up*jumpPower,ForceMode2D.Impulse);
            isGrounded = false;
        }
        anim.SetBool("isJump",!isGrounded);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.CompareTag("Badak"))
        {
            isGrounded = true;
        }
    }

}
