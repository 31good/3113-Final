using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeenemy : MonoBehaviour
{
    public int roomIndex;
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
    bool on_hit = false;


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
    public void taken_damage(float damage){
        health -= damage;
        Knockback();
        on_hit = true;
        _animator.SetTrigger("hit");
    }
    void Update()
    {
        if (health <= 0 && if_die == false)
        {
            if_die = true;
            on_hit = false;
            _animator.SetTrigger("die");
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            Invoke("GoDie", 2);
        }
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if(on_hit == true && if_die == false){
            Knockback();
        }
        else if (distanceFromPlayer < detectrange && distanceFromPlayer > attack_range && if_die == false)
        {
            if (if_attack == true)
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            }
            else
            {
                rotation();
                transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
                _animator.SetTrigger("move");
            }
        }
        else if (distanceFromPlayer <= attack_range && if_die == false)
        {
            int attack_ram = Random.Range(1, 3);
            if (attack_ram == 1)
            {
                _animator.SetTrigger("attack1");
            }
            else
            {
                _animator.SetTrigger("attack2");
            }
        }
        else
        {
            _animator.SetTrigger("idle");
        }
    }

    private void GoDie()
    {
        Destroy(this.gameObject);
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

    private void OnTriggerEnter2D(Collider2D other){
        if(if_attack == true && if_do_damage == false && could_damage == true && other.gameObject.tag == "Player"){
            player.GetComponent<PlayerStats>().TakeDamage(0.5f);
            if_do_damage = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other){
        if(if_attack == true && if_do_damage == false && could_damage == true && other.gameObject.tag == "Player"){
            player.GetComponent<PlayerStats>().TakeDamage(0.5f);
            if_do_damage = true;
        }
    }
    void empty(){}
    void melee_trigger_on(){
        rotation();
        if_attack = true;
    }
    void melee_trigger_off(){
        if_attack = false;
        if_do_damage = false;
        could_damage = false;
    }
    
    void off_hit(){
        on_hit = false;
    }
    void could_do_damage(){
        could_damage = true;
    }
    void destroy_enemy(){
        Destroy(gameObject);
    }
    void Knockback() { 
        print(8);
        float x_distance = player.position.x - this.transform.position.x;
        float y_distance = player.position.y - this.transform.position.y;
        float hypotenuse = Mathf.Sqrt(x_distance*x_distance + y_distance*y_distance);
        Vector2 Knockback = new Vector2(10*((this.transform.position.x - x_distance)/hypotenuse), 10*((this.transform.position.y-y_distance)/hypotenuse));
        transform.position = Vector2.MoveTowards(this.transform.position, Knockback, 5f*Time.deltaTime); 
    }
}
