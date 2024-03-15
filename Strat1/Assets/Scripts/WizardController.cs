using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WizardController : MonoBehaviour
{
    private float wizardSpeed = 10f;
    private Rigidbody2D rigid;
    private bool isGrounded = true;
    public float jumpPower = 100f;
    public Animator anim = null;
    public Vector2 standard_direction = new Vector2(1,1);
    public Vector2 reflect_direction = new Vector2(-1,1);
    public Vector2 a = new Vector2(0,2);
    public float cooldu = 3f;
    public float lastused;
    public Skill equippedSkill;

    public enum Skill
    {
        Teleportation,
        Attack,
        HighJump
    }

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.gravityScale *= 6;
        anim = GetComponent<Animator>();
        lastused = -cooldu;
        equippedSkill = Skill.HighJump;
    }

    // Update is called once per frame
    void Update()
    {

        // if(!isGrounded)
        // {
        //     anim.SetBool("isJump",true);
        // }
        
        //Attack
        if(Input.GetKeyDown(KeyCode.D))
        {
            StartCoroutine(attack());
        }
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-wizardSpeed*Time.deltaTime,0,0);
            transform.localScale = reflect_direction;
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
            transform.Translate(wizardSpeed*Time.deltaTime,0,0);
            transform.localScale = standard_direction;
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
            rigid.AddForce(Vector2.up*jumpPower * 1.2f, ForceMode2D.Impulse);
            isGrounded = false;
        }
        anim.SetBool("isJump",!isGrounded);

        //special skill
        if(Input.GetKeyDown(KeyCode.T))
        {
            SkillSelector(equippedSkill);
        }
        
        else if(Input.GetKeyDown(KeyCode.R))
        {
            powerPush();
        }

    }


    /////////////////////////****************************************************************function


    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.CompareTag("material")||collision.gameObject.CompareTag("monster"))
        {
            isGrounded = true;
        }
    }

    void Teleportation(){
        if(Input.GetKey(KeyCode.UpArrow)){
            transform.position = new Vector3(transform.position.x,transform.position.y+5);
        }
        else{
            if(transform.localScale.x<0)
                transform.position = new Vector3(transform.position.x-6,transform.position.y);
            else
                transform.position = new Vector3(transform.position.x+6,transform.position.y);
        }
    }

    void HighJump()
    {
        rigid.AddForce(Vector2.up*30f,ForceMode2D.Impulse);
        isGrounded = false;
    }

    void powerPush()
    {
        float maxdis = 3f;
        if(Physics2D.Raycast(transform.position,Vector2.right,maxdis))
        {
            Debug.Log("true");
        }
    }

    void SkillSelector(Skill skill)
    {
        switch (skill)
        {
            case Skill.HighJump:
                HighJump();
                break;

            case Skill.Attack:
                Debug.Log("f");
                break;
            
            case Skill.Teleportation:
                Teleportation();
                break;
        }
    }

    IEnumerator attack()
    {
        Collider2D[] cc = Physics2D.OverlapCircleAll(transform.position,4f);
        
            anim.SetBool("attack",true);
            yield return new WaitForSeconds(0.3f);
            foreach(Collider2D a in cc)
            {
                if(a.CompareTag("monster"))
                {       
                    Debug.Log("hit");
                }
            }
    }
}
