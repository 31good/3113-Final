using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootenemy : MonoBehaviour
{
    public float speed;
    public float detectrange;
    public float shootingrange;
    public GameObject bullet;
    public GameObject bullet_pos;
    public float health = 50;
    private float nextfiretime;
    private Transform player;
    Animator _animator;
    bool if_attack = false;
    Transform curr_bullet_pos;
    enum State{
        idle,
        move,
        attack}
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        _animator = GetComponent<Animator>();
        curr_bullet_pos = bullet_pos.transform;
    }
    // Update is called once per frame
    void Update()
    {   
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if(distanceFromPlayer < detectrange&&distanceFromPlayer > shootingrange){
            if(if_attack == true){
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f,0f);
            }
            else{
                rotation();
                transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed*Time.deltaTime);
                _animator.SetTrigger("move");
            }

        }
        else if (distanceFromPlayer <= shootingrange && nextfiretime < Time.time){
            _animator.SetTrigger("attack");
        }
        else{
            _animator.SetTrigger("idle");
        }

    }
    void shoot1(){
        Instantiate(bullet, curr_bullet_pos.transform.position, Quaternion.identity);
    }
    void rotation(){
        if (player.transform.position.x-transform.position.x <= 0){
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            curr_bullet_pos.position = new Vector2((2*transform.position.x - bullet_pos.transform.position.x) ,bullet_pos.transform.position.y);
        }
        else{
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
            curr_bullet_pos.position = bullet_pos.transform.position;
        }
    }
    private void OnDrawGizmosSelected(){
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectrange);
        Gizmos.DrawWireSphere(transform.position, shootingrange);
    }   
    void stop1(){
        rotation();
        if_attack = true;
    }
    void end_stop1(){
        if_attack = false;
    }
}