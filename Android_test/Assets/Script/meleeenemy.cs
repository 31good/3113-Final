using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeenemy : MonoBehaviour
{
    public float speed;
    public float detectrange;
    public float attack_range;
    public float health = 50;
    public bool if_2kind_attack = false;
    private Transform player;
    Animator _animator;
    bool if_attack = false;
    bool could_damage = false;
    bool if_do_damage = false;
    bool if_die = false;
    enum State{
        idle,
        move,
        attack}
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        _animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {   if (health<= 0){
            if_die = true;
            _animator.SetTrigger("die");
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f,0f);
        }
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if(distanceFromPlayer < detectrange&&distanceFromPlayer > attack_range&&if_die == false){
            if(if_attack == true){
                print(1);
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f,0f);
            }
            else{
                rotation();
                transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed*Time.deltaTime);
                _animator.SetTrigger("move");
            }

        }
        else if (distanceFromPlayer <= attack_range && if_die == false){
            int attack_ram = Random.Range(1,3);
            if (attack_ram == 1){
                _animator.SetTrigger("attack1");
            }
            else{
                _animator.SetTrigger("attack2");
            }
        }
        else{
            _animator.SetTrigger("idle");
        }

    }
    void rotation(){
        if (player.transform.position.x-transform.position.x <= 0){
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else{
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
    }
    private void OnDrawGizmosSelected(){
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectrange);
        Gizmos.DrawWireSphere(transform.position, attack_range);
    }  

    void OnTriggerEnter2D(Collision2D other){
        if(if_attack == true && if_do_damage == false && could_damage == true){
            player.GetComponent<PlayerStats>().TakeDamage(0.5f);
            if_do_damage = true;
        }
    }
    void taken_damage(float damage){
        health -= damage;
        _animator.SetTrigger("hit");
    }
    void melee_trigger_on(){
        rotation();
        if_attack = true;
    }
    void melee_trigger_off(){
        if_attack = false;
        if_do_damage = false;
        could_damage = false;
    }
    void could_do_damage(){
        could_damage = true;
    }
}
