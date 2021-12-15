using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pet : MonoBehaviour
{
    public int pet_type; // 1 follow 2 attack 3 pick up
    public float speed;
    public float stop_distance;
    public GameObject Bullet;
    public GameObject fire_pos;
    private Transform player;
    enum State{
        up,
        left,
        right,
        down}
    Animator _animator;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        _animator = GetComponent<Animator>();
    }

    void move(){
        player = GameObject.FindGameObjectWithTag("Player").transform;
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        Vector2 targetDir = player.position - transform.position;
        float angle = Mathf.Atan2(player.position.y - transform.position.y,player.position.x - transform.position.x)* 180 / Mathf.PI;
        if (angle <= 45 && angle >= -45){
            _animator.SetTrigger("right");
        }
        else if(angle > 45 && angle <= 135){
            _animator.SetTrigger("up");
        }
        else if(angle < -45 && angle >= -135){
            _animator.SetTrigger("down");
        }
        else{
            _animator.SetTrigger("left");
        }
        if (distanceFromPlayer > stop_distance){
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed*Time.deltaTime);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (pet_type == 1){
            move();
        }
        else if(pet_type == 2){
            
        }
        else{

        }

    }
    private void OnDrawGizmosSelected(){
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, stop_distance);
    } 
}
