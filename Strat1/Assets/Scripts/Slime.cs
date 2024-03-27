using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public int hp = 100;
    public Animator anim;
    public Rigidbody2D rigid;
    public int nextMove;
    public float speed = 6f;

    
    void Awake()
    {
        
        anim = GetComponent<Animator>();
        Invoke("think",2);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.rotation = Quaternion.identity;
        if(nextMove == -1)
            transform.localScale = new Vector2(-1,1);
        else if(nextMove == 1)
            transform.localScale = new Vector2(1,1);
        transform.Translate(nextMove*0.1f,0,0);


        //monster automatic movement
        Vector2 frontVec = new Vector2(transform.position.x+nextMove*0.2f,transform.position.y);
        Debug.DrawRay(frontVec,Vector3.down,new Color(0,1,0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec,Vector3.down,1,LayerMask.GetMask("Default"));
        if(rayHit.collider == null)
        {
            nextMove*= -1;
            CancelInvoke();
            Invoke("think",3);
        }
    }

    void think()
    {
        nextMove = Random.Range(-1,2);
        float nextThinkTime = Random.Range(2f,4f);
        Invoke("think",nextThinkTime);
    }
}
