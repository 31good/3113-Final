using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        PlayerStats[] players = FindObjectsOfType<PlayerStats>();
        foreach (var thing in players){
            thing.gameObject.transform.position=transform.position;
        }
    }

}
