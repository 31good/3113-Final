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
    private Rigidbody2D rig;
    private Vector3 pre_position;
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
    public float damage = 10f;
    private bool if_attack = false;
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
    }
    void Start ()
    {
        rig = gameObject.GetComponent<Rigidbody2D>();
        pre_position = gameObject.transform.position;
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
}
