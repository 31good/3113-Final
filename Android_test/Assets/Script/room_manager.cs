using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class room_manager : MonoBehaviour
{
    public Transform upper_right;
    public Transform lower_left;
    public GameObject door1;
    public GameObject door2;
    public GameObject door3;
    public GameObject door4;
    public GameObject Portal;
    public GameObject enemy_list;
    private float top;
    private float bottom;
    private float left;
    private float right;
    private Transform player;
    private int count_enemy;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        top = upper_right.transform.position.y;
        right = upper_right.transform.position.x;
        bottom = lower_left.transform.position.y;
        left = lower_left.transform.position.x;
        set_false();
    }

    // Update is called once per frame
    void Update()
    {
        count_enemy = 0;
        if((player.position.x>left && player.position.x<right)&&(player.position.y>bottom && player.position.y<top)){
            foreach(Transform child in enemy_list.transform){
                if((child.position.x>left && child.position.x<right)&&(child.position.y>bottom && child.position.y<top)){
                    count_enemy+=1;
                }
            }
            if(count_enemy != 0){
                set_true();
            }
            else{
                set_false();
            }
        }
    }
    void set_false(){
        door1.SetActive(false);
        door2.SetActive(false);
        door3.SetActive(false);
        door4.SetActive(false);
        Portal.SetActive(false);
    }

    void set_true(){
        door1.SetActive(true);
        door2.SetActive(true);
        door3.SetActive(true);
        door4.SetActive(true);
        Portal.SetActive(true);
    }
}
