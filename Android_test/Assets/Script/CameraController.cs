using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    public float smoothing;
    public Vector3 offset;

    // Update is called once per frame
    void FixedUpdate()
    {
        if(player!=null){
            Vector3 newPosition= Vector3.Lerp(transform.position,player.transform.position+offset,smoothing);
            transform.position=newPosition;
            print("1");
        }
    }
}
