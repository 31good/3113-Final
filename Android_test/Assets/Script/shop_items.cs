using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shop_items : MonoBehaviour
{
    // Start is called before the first frame update
    enum State{
        weapon,
        skill,
        health,
        pet}
    public string type;
    public int num;
    public int price;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}
