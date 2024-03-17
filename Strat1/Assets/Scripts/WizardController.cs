using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
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
    public Skill equippedSkill;
    public float defense;

    public enum Skill
    {
        Teleportation,
        Attack,
        HighJump,
        PowerPush,
        SpeedUp,
        DefenseUp,
        RubberMan,
        Magnet,
        Giant,
        NanJangI,
        FireBall,
        RailGun
    }

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.gravityScale *= 6;
        anim = GetComponent<Animator>();
        equippedSkill = Skill.SpeedUp;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.identity;

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
            rigid.AddForce(Vector2.up*jumpPower * 1.5f, ForceMode2D.Impulse);
            isGrounded = false;
        }
        anim.SetBool("isJump",!isGrounded);

        //special skill
        if(Input.GetKeyDown(KeyCode.T))
        {
            SkillSelector(equippedSkill);
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
        rigid.AddForce(Vector2.up*jumpPower*3f,ForceMode2D.Impulse);
        isGrounded = false;
    }

    void PowerPush()
    {
        Collider2D[] enemy = Physics2D.OverlapCircleAll(transform.position,6f);
        foreach(Collider2D e in enemy)
        {
            if(e.CompareTag("monster"))
            {
                Rigidbody2D rb = e.attachedRigidbody;
                Vector2 forceDirection = transform.localScale;
                rb.AddForce(forceDirection*10f,ForceMode2D.Impulse);
            }

            
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

            case Skill.PowerPush:
                PowerPush();
                break;

            case Skill.SpeedUp:
                SpeedUp();
                break;
            
            case Skill.DefenseUp:
                DefenseUp();
                break;
            
            case Skill.RubberMan:
                RubberMan();
                break;
        }
    }

    void SpeedUp()
    {
        wizardSpeed = 20f;
        StartCoroutine(DeactivateSpeedUp());
    }

    void DefenseUp()
    {
        defense += 1000;
        StartCoroutine(DeactivateDefenseUp());
    }

    void RubberMan()
    {
        while(isGrounded)
        {
            rigid.AddForce(Vector2.up,ForceMode2D.Impulse);
        }
        StartCoroutine(DeactivateRubberMan());
    }

    //attack
    IEnumerator attack()
    {
        Collider2D[] enemy = Physics2D.OverlapCircleAll(transform.position,4f);
        
            anim.SetBool("attack",true);
            yield return new WaitForSeconds(0.3f);
            foreach(Collider2D e in enemy)
            {
                if(e.CompareTag("monster"))
                {
                    Debug.Log("hit");
                }
            }
    }

    IEnumerator DeactivateSpeedUp()
    {
        yield return new WaitForSeconds(7f);
        wizardSpeed = 10f;
        yield return new WaitForSeconds(3f);
        Debug.Log("aa");
    }

    IEnumerator DeactivateDefenseUp()
    {
        yield return new WaitForSeconds(7f);
        defense -= 1000;
    }

    IEnumerator DeactivateRubberMan()
    {
        yield return new WaitForSeconds(7f);
    }

    void Damage()
    {
        float x = 3000;
        float damaged;
        float attk = 10000;
        damaged = attk * defense / (defense + x);
    }
}
