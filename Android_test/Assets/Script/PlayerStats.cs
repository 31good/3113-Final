/*
 *  Author: ariel oliveira [o.arielg@gmail.com]
 */

using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerStats : MonoBehaviour
{
    public delegate void OnHealthChangedDelegate();
    public OnHealthChangedDelegate onHealthChangedCallback;

    #region Sigleton
    private static PlayerStats instance;
    public AnimationClip _walk;
    public Animation _Legs;
    public GameObject hit_body;
    public Transform pet_trans;
    public GameObject pet1;
    public GameObject pet2;
    private Rigidbody2D rig;
    private Vector3 pre_position;
    public float damage_add = 0;
    public float stack_speed = 0;
    public static PlayerStats Instance

    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<PlayerStats>();
            return instance;
        }
    }
    #endregion

    [SerializeField]
    private float health;
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private float maxTotalHealth;

    public float Health { get { return health; } }
    public float MaxHealth { get { return maxHealth; } }
    public float MaxTotalHealth { get { return maxTotalHealth; } }

    public TextMeshProUGUI key_count_text;
    public int key_count=0;
    public TextMeshProUGUI coin_count_text;
    public int coin_count=0;
    private bool if_attack = false;
    private GameObject shop;
    private GameObject shop_item;
    private void Update() {
        key_count_text.text="x"+key_count;
        coin_count_text.text="x"+coin_count;
    }

    public void Heal(float health)
    {
        this.health += health;
        ClampHealth();
    }

    public void TakeDamage(float dmg)
    {   
        hit_body.SetActive(true);
        Invoke("set_trigger",0.5f);
        health -= dmg;
        ClampHealth();
    }

    public void AddHealth()
    {
        if (maxHealth < maxTotalHealth)
        {
            maxHealth += 1;
            health = maxHealth;
            if (onHealthChangedCallback != null)
                onHealthChangedCallback.Invoke();
        }
    }

    public void minusHealth()
    {
        if (health<=1)
        {
            maxHealth -= 1;

            if (onHealthChangedCallback != null)
                onHealthChangedCallback.Invoke();
        }
        else{
            maxHealth -= 1;
            health -=1;
            if (onHealthChangedCallback != null)
                onHealthChangedCallback.Invoke();
        }
    }

    void ClampHealth()
    {
        health = Mathf.Clamp(health, 0, maxHealth);

        if (onHealthChangedCallback != null)
            onHealthChangedCallback.Invoke();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag=="bullet"){
            TakeDamage(0.5f);
        }

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag=="health"){
            //AddHealth();
            if(Health==maxHealth){return;}
            Heal(1f);
            Destroy(other.gameObject);
        }

        if(other.gameObject.tag=="key"){
            key_count+=1;
            Destroy(other.gameObject);
        }

        if(other.gameObject.tag=="coin"){
            coin_count+=1;
            Destroy(other.gameObject);
        }
        if(other.gameObject.tag == "shop"){
            shop = other.gameObject;
        }

    }
    void Start ()
    {
        rig = gameObject.GetComponent<Rigidbody2D>();
        pre_position = gameObject.transform.position;
        hit_body.SetActive(false);
	}
    void FixedUpdate(){
        print("walk1");
        if(gameObject.transform.position != pre_position){
            pre_position = gameObject.transform.position;
            print("walk");
            _Legs.clip = _walk;
            _Legs.Play();
        }
    }
    void set_trigger(){
        hit_body.SetActive(false);
    }

    public void shopping(){
        shop_item = shop.GetComponent<shop>().item;
        string type = shop_item.GetComponent<shop_items>().type;
        int num = shop_item.GetComponent<shop_items>().num;
        int price = shop_item.GetComponent<shop_items>().price;
        if(coin_count >= price){
            coin_count-= price;
            if(type == "health"){
                if(Health<maxHealth){Heal(1f);}
            }
            if(type == "weapon"){
                this.GetComponent<JoyStickController>().swap_weapon(num);
            }
            if(type == "pet"){
                if(num ==1){
                    Instantiate(pet1);
                }
                if(num==2){
                    Instantiate(pet2);
                }
            }
            if(type == "skill"){
                if(num ==1){
                    AddHealth();
                }
                if(num ==2){
                    damage_add+=3;
                }
                if(num ==3){
                    stack_speed += 0.05f;
                }
                if(num ==4){
                    damage_add+=5;
                    minusHealth();
                }
                if(num ==5){
                    this.GetComponent<JoyStickController>().speed +=1;
                }
            }
            shop_item.SetActive(false);
        }
    }
}
