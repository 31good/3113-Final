using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon_code : MonoBehaviour
{
    private GameObject player;
    private bool is_attacking;
    public float damage = 10;
    private bool if_damage = false;

    public float attack_span = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        is_attacking = player.GetComponent<JoyStickController>().isRotating;
    }
    public float get_attack_span(){
        return attack_span;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        is_attacking = player.GetComponent<JoyStickController>().isRotating;
        if(is_attacking == false){
            if_damage =false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "melee_enemy" && is_attacking == true && if_damage == false){
            other.gameObject.GetComponent<meleeenemy>().taken_damage(damage);
            if_damage = true;
            print("12345");
        }
        if(other.gameObject.tag == "range_enemy" && is_attacking == true && if_damage == false){
            other.gameObject.GetComponent<rangeenemy>().taken_damage(damage);
            if_damage = true;
        }
    }
    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag == "melee_enemy" && is_attacking == true && if_damage == false){
            other.gameObject.GetComponent<meleeenemy>().taken_damage(damage);
            if_damage = true;
            print("12345");
        }
        if(other.gameObject.tag == "range_enemy" && is_attacking == true && if_damage == false){
            other.gameObject.GetComponent<rangeenemy>().taken_damage(damage);
            if_damage = true;
        }
    }
}
