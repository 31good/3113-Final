using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rangeenemy : MonoBehaviour
{
    public int roomIndex;
    public float speed;
    public float detectrange;
    public float shootingrange;
    public GameObject bullet;
    public GameObject bullet_pos;
    public float health = 50;
    private float nextfiretime;
    private Transform player;
    Animator _animator;
    bool if_attack = true;
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
     public void taken_damage(float damage){
        health -= damage;
        Knockback();
        _animator.SetTrigger("hit");
     }
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
    void Knockback() { 
        float x_distance = player.position.x - this.transform.position.x;
        float y_distance = player.position.y - this.transform.position.y;
        float hypotenuse = Mathf.Sqrt(x_distance*x_distance + y_distance*y_distance);
        Vector2 Knockback = new Vector2(5*(this.transform.position.x - x_distance/hypotenuse), 5*(this.transform.position.y -y_distance/hypotenuse));
        transform.position = Vector2.MoveTowards(this.transform.position, Knockback, 15*Time.deltaTime); 
    }
}