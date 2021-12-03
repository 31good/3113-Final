using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStickController : MonoBehaviour
{
    public FixedJoystick moveJoystick;
    public FixedJoystick lookJoystick;
    private float GameObjectRotation;
    public bool FacingRight =true;
    Transform weapon;

    public float speed=5;
    private BoxCollider2D boxCollider;
    // Update is called once per frame
    private void Start(){
        boxCollider = GetComponent<BoxCollider2D>();
    }
    
    void Update()
    {
        foreach(Transform child in this.transform){
            //print(child.tag);
            if(child.tag=="Weapon"){
                weapon=child;
            }
        }
        UpdateMoveJoystick();
        UpdateLookJoystick();
    }

    void UpdateMoveJoystick()
    {
        float hoz = moveJoystick.Horizontal;
        float ver = moveJoystick.Vertical;
        RaycastHit2D hit=Physics2D.BoxCast(transform.position,boxCollider.size,0,new Vector2(0,ver),Mathf.Abs(ver*Time.deltaTime*speed),LayerMask.GetMask("wall"));
        RaycastHit2D hit_2=Physics2D.BoxCast(transform.position,boxCollider.size,0,new Vector2(hoz,0),Mathf.Abs(hoz*Time.deltaTime*speed),LayerMask.GetMask("wall"));
        if(hit.collider==null){
            transform.Translate(0,speed*ver*Time.deltaTime,0);
        }
        if(hit_2.collider==null){
            if(FacingRight){
                transform.Translate(speed*hoz*Time.deltaTime,0,0);
            }
            else{
                transform.Translate(speed*-1*hoz*Time.deltaTime,0,0);
            }
        }
        /*
        Vector3 direction = new Vector3(0,0,0);
        if(hit.collider==null && hit_2.collider==null){
            Vector2 convertedXY = ConvertWithCamera(Camera.main.transform.position, hoz, ver);
            direction = new Vector3(convertedXY.x,convertedXY.y, 0).normalized;
        }
        else if(hit.collider!=null && hit_2.collider!=null){return;}
        else if(hit.collider!=null){
            Vector2 convertedXY = ConvertWithCamera(Camera.main.transform.position, hoz, ver);
            direction = new Vector3(convertedXY.x,Camera.main.transform.position.y, 0).normalized;
        }
        else if(hit_2.collider!=null){
            Vector2 convertedXY = ConvertWithCamera(Camera.main.transform.position, hoz, ver);
            direction = new Vector3(Camera.main.transform.position.x,convertedXY.y, 0).normalized;
        }*/
        if(hoz<0&&FacingRight){Flip();}
        if(hoz>0&&!FacingRight){Flip();}
        //transform.Translate(direction * 0.02f, Space.World);
    }



    void UpdateLookJoystick()
    {
        float hoz = lookJoystick.Horizontal;
        float ver = lookJoystick.Vertical;
        if(FacingRight){
            GameObjectRotation=hoz+ver*90;
            weapon.rotation=Quaternion.Euler(0f,0f,GameObjectRotation);
        }
        else{
            GameObjectRotation=hoz+ver*-90;
            weapon.rotation=Quaternion.Euler(0f,0f,GameObjectRotation); 
        }
        if(hoz<0&&FacingRight){
            Flip();
        }
        else if(hoz>0&&!FacingRight){
            Flip();
        }
    }

    private void Flip()
	{
		// Flips the player.
		FacingRight = !FacingRight;

		transform.Rotate(0, 180, 0);
	}

    private Vector2 ConvertWithCamera(Vector3 cameraPos, float hor, float ver)
    {
        Vector2 joyDirection = new Vector2(hor, ver).normalized;
        Vector2 camera2DPos = new Vector2(cameraPos.x, cameraPos.z);
        Vector2 playerPos = new Vector2(transform.position.x, transform.position.z);
        Vector2 cameraToPlayerDirection = (Vector2.zero - camera2DPos).normalized;
        float angle = Vector2.SignedAngle(cameraToPlayerDirection, new Vector2(0, 1));
        Vector2 finalDirection = RotateVector(joyDirection, -angle);
        return finalDirection;
    }

    public Vector2 RotateVector(Vector2 v, float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        float _x = v.x * Mathf.Cos(radian) - v.y * Mathf.Sin(radian);
        float _y = v.x * Mathf.Sin(radian) + v.y * Mathf.Cos(radian);
        return new Vector2(_x, _y);
    }
}
