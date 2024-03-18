using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{

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

    protected void SkillSelector(Skill skill)
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
        speed = 20f;
        StartCoroutine(DeactivateSpeedUp());
    }

    void DefenseUp()
    {
        def += 1000;
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
    protected IEnumerator attack()
    {
        Collider2D[] enemy = Physics2D.OverlapCircleAll(transform.position,4.2f);
        
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

    protected IEnumerator DeactivateSpeedUp()
    {
        yield return new WaitForSeconds(7f);
        speed = 10f;
        yield return new WaitForSeconds(3f);
        Debug.Log("aa");
    }

    protected IEnumerator DeactivateDefenseUp()
    {
        yield return new WaitForSeconds(7f);
        def -= 1000;
    }

    protected IEnumerator DeactivateRubberMan()
    {
        yield return new WaitForSeconds(7f);
    }

    void Damage()
    {
        float x = 3000;
        float damaged;
        atk = 10000;
        damaged = atk * def / (def + x);
    }
}
