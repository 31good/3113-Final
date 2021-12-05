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
    public float firerate = 1f;
    private float nextfiretime;
    private Transform player;
    Animator _animator;
    enum State{
        idle,
        attack}
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if(distanceFromPlayer < detectrange&&distanceFromPlayer > shootingrange){
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed*Time.deltaTime);
            _animator.SetBool("idle", true);
        }
        else if (distanceFromPlayer <= shootingrange && nextfiretime < Time.time){
            Instantiate(bullet, bullet_pos.transform.position, Quaternion.identity);
            nextfiretime = Time.time + firerate;
            _animator.SetBool("idle", false);
        }
        else{
            _animator.SetBool("idle", true);
        }

    }
    private void OnDrawGizmosSelected(){
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectrange);
        Gizmos.DrawWireSphere(transform.position, shootingrange);
    }   
}