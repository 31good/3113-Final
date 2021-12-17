using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shop : MonoBehaviour
{
    public GameObject item;
    public GameObject introduction;
    public GameObject player;
    public string type = null;
    private int random_num;
    void Start()
    {
        random_num = Random.Range(0,14);
        int count = 0;
        foreach(Transform child in this.transform){
            if(count == random_num){
                child.gameObject.SetActive(true);
                item = child.gameObject;
            }
            else{
                child.gameObject.SetActive(false);
            }
            count +=1;
        }
        foreach(Transform child in item.transform){
            if(child.tag == "introduction"){
                introduction = child.gameObject;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            print("shop");
            introduction.SetActive(true);
        } 
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Player"){
            introduction.SetActive(false);
        }
    }
}
