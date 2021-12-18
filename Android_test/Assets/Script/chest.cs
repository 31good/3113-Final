using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chest : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject item;
    public GameObject player;
    public Sprite openbox;
    public string type = null;
    private int random_num;

    public bool notopen=true;
    public AudioSource open;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void open_chest(){
        open.Play();
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
        //print("chest");
        gameObject.GetComponent<SpriteRenderer>().sprite = openbox;
        notopen=false;
    }
}
