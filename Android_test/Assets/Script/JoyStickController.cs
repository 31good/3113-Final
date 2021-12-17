using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class JoyStickController : MonoBehaviour
{
    public FixedJoystick moveJoystick;
    public FixedJoystick lookJoystick;
    private float GameObjectRotation;
    public bool FacingRight =true;
    Transform weapon;
    public bool isRotating=false;
    public float speed=5;
    private BoxCollider2D boxCollider;
    public float attack_span;

    public Button interaction_button;
    // Update is called once per frame
    private void Start(){
        boxCollider = GetComponent<BoxCollider2D>();
    }
    
    void Update()
    {
        print(isRotating);
        foreach(Transform child in this.transform){
            //print(child.tag);
            if(child.tag=="Weapon" && child.gameObject.activeSelf == true){
                print(123);
                weapon=child;
                attack_span=weapon.GetComponent<weapon_code>().get_attack_span();   
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
        if(hoz<0&&FacingRight&&!isRotating){Flip();}
        if(hoz>0&&!FacingRight&&!isRotating){Flip();}
        //transform.Translate(direction * 0.02f, Space.World);
    }



    void UpdateLookJoystick()
    {
        float hoz = lookJoystick.Horizontal;
        float ver = lookJoystick.Vertical;
        //print(hoz);
        //print(ver);
        if(hoz<0&&FacingRight&&!isRotating){
            Flip();
        }
        else if(hoz>0&&!FacingRight&&!isRotating){
            Flip();
        }
        if(Mathf.Abs(hoz)>=0.2||Mathf.Abs(ver)>=0.2){
            if(FacingRight){
                //GameObjectRotation=hoz+ver*90;
                //print(GameObjectRotation);
                float degrees = Mathf.Rad2Deg*(Mathf.Atan2(ver,hoz));
                StartCoroutine(Rotate(degrees,attack_span));
                //print(GameObjectRotation);
                //weapon.rotation=Quaternion.Euler(0f,0f,GameObjectRotation);
            }
            else{
                //GameObjectRotation=hoz+ver*-90;
                //print(GameObjectRotation);
                float degrees = Mathf.Rad2Deg*(Mathf.Atan2(ver,hoz));
                StartCoroutine(Rotate(degrees,attack_span));
                //weapon.rotation=Quaternion.Euler(0f,0f,GameObjectRotation); 
            }
        }
    }

    private void Flip()
	{
		// Flips the player.
		FacingRight = !FacingRight;

		transform.Rotate(0, 180, 0);
        weapon.gameObject.transform.Rotate(0, 180, 0);
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


    private IEnumerator Rotate(float degrees, float duration)
    {
    if(isRotating) yield break;
    isRotating = true;

    float passedTime = 0f;
    //print(degrees+15-135);
    //print(degrees-15-135);
    var startRotation = Quaternion.Euler(0f,0f,degrees +80);
    var targetRotation = Quaternion.Euler(0f, 0f, degrees-80);

    while(passedTime < duration)
        {
        // this will always be a linear value between 0 and 1
        var lerpFactor = passedTime / duration;
        //optionally you can add ease-in and ease-out
        //lerpFactor = Mathf.SmoothStep(0, 1, lerFactor);

        // This rotates linear 
        //weapon.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, lerpFactor);
        // OR This already rotates smoothed using a spherical interpolation
        weapon.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, lerpFactor);

        // Here you see it is again - Time.deltaTime
        // increase the passedTime by the time passed since last frame
        // to avoid overshooting we clamp it
        passedTime += Mathf.Min(duration - passedTime, Time.deltaTime);

        // yield in a Coroutine reads like
        // pause here, render this frame and continue from here in the next frame
        yield return null;
        }

    // just to be sure
    weapon.transform.rotation = targetRotation;
    isRotating = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "shop"||other.tag=="box"){
            interaction_button.gameObject.SetActive(true);
        } 
    }

    private void OnTriggerstay2D(Collider2D other) {
        if(other.tag == "shop"||other.tag=="box"){
            interaction_button.gameObject.SetActive(true);
        } 
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "shop"||other.tag=="box"){
            interaction_button.gameObject.SetActive(false);
        }
    }

    public void swap_weapon(int num){
        foreach(Transform child in this.transform){
            if(child.tag=="Weapon"){
                child.gameObject.SetActive(true);
                print("weapon"+num);
                if(child.GetComponent<weapon_code>().weapon_id == num){
                    child.gameObject.SetActive(true);
                    print("weapon1");
                }
                else{
                    child.gameObject.SetActive(false);
                }   
            }
        }
    }
}
