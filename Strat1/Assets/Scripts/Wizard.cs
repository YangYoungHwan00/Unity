using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public class Wizard : MonoBehaviour
{
    private float speed = 10f;
    public Skill equippedSkill;
    public float defense;
    public Vector2 standard_direction = new Vector2(1,1);
    public Vector2 reverse_direction = new Vector2(-1,1);
    public Animator anim;
    public Rigidbody2D rigid;
    public Collider2D playerCollider;
    public GameObject go;
    public bool isGrounded;
    public bool onLadder = false;
    public bool canLadder = false;
    public Vector2 ladderTop;
    public Vector2 ladderBottom;
    public bool isAttacking = false;
    public float jumpPower = 9f;
    public int hp;
    public int stamina;
    public float atk;
    public float def;

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
        
        anim = GetComponent<Animator>();
        playerCollider = GetComponent<Collider2D>();
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
        
        else if(Input.GetKeyUp(KeyCode.LeftArrow))
            anim.SetBool("isRun",false);
        
        else if(Input.GetKeyUp(KeyCode.RightArrow))
            anim.SetBool("isRun",false);

        //special skill
        if(Input.GetKeyDown(KeyCode.T))
        {
            SkillSelector(equippedSkill);
        }

        //jump
        if(isGrounded&&Input.GetKeyDown(KeyCode.Space))
        {
            rigid.AddForce(Vector2.up*jumpPower, ForceMode2D.Impulse);
            // isGrounded = false;
        }
        anim.SetBool("isJump",!isGrounded);

        if(canLadder&&(Input.GetKey(KeyCode.UpArrow))&&transform.position.y<ladderTop.y-3f)
        {
            onLadder = true;
        }
        if(canLadder&&(Input.GetKey(KeyCode.DownArrow))&&transform.position.y>ladderBottom.y)
        {
            onLadder = true;
        }
        if(onLadder&&Input.GetKeyDown(KeyCode.Space))
        {
            onLadder = false;
            rigid.gravityScale = 30;
            rigid.isKinematic = false;
            rigid.AddForce(Vector2.up*jumpPower*0.7f,ForceMode2D.Impulse);
        }
        
        if(!isGrounded)
        {
            if(rigid.velocity.y>0)
            {
                playerCollider.enabled = false;
            }
            else
            {
                playerCollider.enabled = true;
            }
        }
    }

    void FixedUpdate()
    {
        if(!isAttacking)
        {
            if(Input.GetKey(KeyCode.LeftArrow)&&!onLadder)
            {
                transform.Translate(-speed*Time.deltaTime,0,0);
                transform.localScale = reverse_direction;
                anim.SetBool("isRun", true);
                if(!isGrounded)
                {
                    anim.SetBool("isRun", false); 
                }
                
            }
            else if(Input.GetKey(KeyCode.RightArrow)&&!onLadder)
            {
                transform.Translate(speed*Time.deltaTime,0,0);
                transform.localScale = standard_direction;
                anim.SetBool("isRun", true);
                if(!isGrounded)
                {
                    anim.SetBool("isRun", false);
                }
            }
        }
        

        if(onLadder)
        {
            rigid.gravityScale = 0;
            rigid.isKinematic = true;
            if(Input.GetKey(KeyCode.UpArrow))
            {
                rigid.velocity = new Vector2(0,4f);
                upLadder();
            }
                
            if(Input.GetKey(KeyCode.DownArrow))
            {
                rigid.velocity = new Vector2(0,-4f);
                downLadder();
            }

            rigid.velocity = new Vector2(0,0);
        }
        
    }

    /////////////////////////****************************************************************function


    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.CompareTag("Badak"))
        {
            isGrounded = true;
        }

        
    }

    private void OnCollisionStay2D(Collision2D collision) {
        if(collision.gameObject.CompareTag("Badak"))
            isGrounded = true;
        if(collision.gameObject.CompareTag("Monster"))
        {
            if(gameObject.layer == 0)
            onDamaged(collision.transform.position);
        }
    }

    private void OnCollisionExit2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Badak"))
            isGrounded = false;
    }

    void onDamaged(Vector2 targetPos)
    {
        gameObject.layer = 3;
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc,1)*80,ForceMode2D.Impulse);
        Invoke("offDamaged",1f);
    }

    void offDamaged()
    {
        gameObject.layer = 0;
    }

    void Teleportation()
    {
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

    void Giant()
    {
        transform.localScale *= 2;
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
        speed += 10f;
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
                if(e.CompareTag("Monster"))
                {
                    Debug.Log("hit");
                }
            }
    }

    IEnumerator DeactivateSpeedUp()
    {
        yield return new WaitForSeconds(7f);
        speed -= 10f;
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

    void upLadder()
    {
        transform.Translate(0,0.5f,0);
    }

    void downLadder()
    {
        transform.Translate(0,-0.5f,0);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Ladder"))
        {
            canLadder = true;
            ladderTop = new Vector2(other.transform.position.x,other.transform.position.y+other.transform.localScale.y);
            ladderBottom = new Vector2(other.transform.position.x,other.transform.position.y-other.transform.localScale.y/2+transform.localScale.y);
        }
            
    }

    private void OnTriggerStay2D(Collider2D other){
        if(onLadder)
        {
            transform.position = new Vector2(other.transform.position.x,transform.position.y);
            if(transform.position.y>other.transform.position.y+other.transform.localScale.y)
            {
                onLadder = false;
                rigid.gravityScale = 30;
                rigid.isKinematic = false;
            }
            else if(transform.position.y<other.transform.position.y-other.transform.localScale.y/2+transform.localScale.y)
            {
                onLadder = false;
                rigid.gravityScale = 30;
                rigid.isKinematic = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        if(other.CompareTag("Ladder"))
        {
            canLadder = false;
            onLadder = false;
            rigid.gravityScale = 30;
            rigid.isKinematic = false;
        }
    }
}
