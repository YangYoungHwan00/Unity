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
    public bool isGround
    {
        get{
            return isGrounded;
        }
        set{
            isGround = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //move
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.Translate(-wizardSpeed*Time.deltaTime,0,0);
        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.Translate(wizardSpeed*Time.deltaTime,0,0);
        }

        //jump
        if(Input.GetKeyDown(KeyCode.Space)&&isGrounded)
        {
            rigid.AddForce(Vector2.up*jumpPower,ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.CompareTag("Badak"))
        {
            isGrounded = true;
        }
    }

}
