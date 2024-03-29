using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class MainCamera : MonoBehaviour
{   
    Transform character;
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject target = GameObject.Find("Wizard");
        character = target.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(character.position.x,character.position.y+8,character.position.z-20);
    }
}
