using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public int hp = 100;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.identity;
    }

    void Attack()
    {
        Collider2D[] enemy = Physics2D.OverlapCircleAll(transform.position,3f);
        foreach(Collider2D e in enemy)
        {
            if(e.CompareTag("player"))
            {
                Debug.Log("attack");
            }
        }
    }
}
