using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{       
    public int hp;
    public int stamina;
    public float speed;
    public float atk;
    public float def;
    public float jumpPower;
    public Vector2 standard_direction = new Vector2(1,1);
    public Vector2 reflect_direction = new Vector2(-1,1);
    public enum Skill{};
    public Skill equippedskill;
    public bool isGrounded;
    public Animator anim;
    public Rigidbody2D rigid;
}
