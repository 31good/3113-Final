using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class attackbutton : MonoBehaviour
{
    public GameObject weapon;
    public bool isattacking=true;
    //need to find out how to detect the weapon
    // Start is called before the first frame update
    public void attack()
    {
        isattacking=true;
        StartCoroutine(RotateImage(225.0f));
        //print("end");
    }

    IEnumerator RotateImage(float angle){
     float moveSpeed = 1000f;
     //print("yes");
     //print(weapon.transform.rotation.z);
     while(Math.Abs(weapon.transform.eulerAngles.z-angle) > 1)
     {//print(Math.Abs(weapon.transform.rotation.z-angle));
        //print(weapon.transform.eulerAngles.z);
        weapon.transform.rotation = Quaternion.RotateTowards(weapon.transform.rotation, Quaternion.Euler(0, 0, angle), moveSpeed * Time.deltaTime);
        //print(moveSpeed*Time.deltaTime);
        yield return null;
     }
     weapon.transform.rotation = Quaternion.Euler(0, 0, angle);
     isattacking=false;
     yield break;
    }

    private void FixedUpdate() {
        if (!isattacking){
            StartCoroutine(RotateImage(350.0f));
        }
    }

}
