using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon_code : MonoBehaviour
{
    private GameObject player;
    private bool is_attacking;
    private float damage;
    private bool if_damage = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        damage = player.GetComponent<PlayerStats>().damage;
        is_attacking = player.GetComponent<JoyStickController>().isRotating;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        damage = player.GetComponent<PlayerStats>().damage;
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
}
