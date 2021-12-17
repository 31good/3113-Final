using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon_code : MonoBehaviour
{
    private GameObject player;
    private bool is_attacking;
    public float damage = 10;
    private bool if_damage = false;
    private float final_damage;
    private float final_span;
    public int weapon_id;
    public AudioSource weaponSound;
    public float attack_span = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        final_span = attack_span;
        final_damage = damage;
        player = GameObject.FindGameObjectWithTag("Player");
        is_attacking = player.GetComponent<JoyStickController>().isRotating;
    }
    public float get_attack_span(){
        return attack_span;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        final_damage = damage + player.GetComponent<PlayerStats>().damage_add;
        final_span = attack_span - player.GetComponent<PlayerStats>().stack_speed;
        if (final_span <= 0.2f){
            final_span = 0.2f;
        }
        is_attacking = player.GetComponent<JoyStickController>().isRotating;
        if(is_attacking == false){
            if_damage =false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "melee_enemy" && is_attacking == true && if_damage == false){
            other.gameObject.GetComponent<meleeenemy>().taken_damage(final_damage);
            if_damage = true;
            weaponSound.Play();
            Invoke("offset_if_damage",final_span);
            print("12345");
        }
        if(other.gameObject.tag == "range_enemy" && is_attacking == true && if_damage == false){
            other.gameObject.GetComponent<rangeenemy>().taken_damage(final_damage);
            if_damage = true;
            weaponSound.Play();
        }
    }
    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag == "melee_enemy" && is_attacking == true && if_damage == false){
            other.gameObject.GetComponent<meleeenemy>().taken_damage(final_damage);
            if_damage = true;
            weaponSound.Play();
            print("12345");
        }
        if(other.gameObject.tag == "range_enemy" && is_attacking == true && if_damage == false){
            other.gameObject.GetComponent<rangeenemy>().taken_damage(final_damage);
            if_damage = true;
            weaponSound.Play();
        }
    }
    private void offset_if_damage(){
        if_damage = false;
    }
}
