using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardAnimation : MonoBehaviour
{
    public Animator anim = null;
    public Vector3 standard_direction = new Vector3(1,1,1);
    public Vector3 reflect_direction = new Vector3(-1,1,1);

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.localScale = reflect_direction;
            anim.SetBool("isRun",true);
        }
        else if(Input.GetKeyUp(KeyCode.LeftArrow))
        {
            anim.SetBool("isRun",false);
        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.localScale = standard_direction;
            anim.SetBool("isRun",true);
        }
        else if(Input.GetKeyUp(KeyCode.RightArrow))
        {
            anim.SetBool("isRun",false);
        }
        else if(Input.GetKey(KeyCode.Space))
        {
            anim.SetBool("isJump",true);
        }
    }
}
