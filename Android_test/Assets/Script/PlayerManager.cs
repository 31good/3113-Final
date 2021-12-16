using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager Instance;
    public static PlayerManager GetInstance()
    {
        return Instance;
    }
    private void Awake()
    {
        Instance = this;
    }
    public GameObject skillSelectView;
    public int roomIndex =1;
    public GameObject room001;
    public GameObject room002;

    public List<SpriteRenderer> enemys001 = new List<SpriteRenderer>();
    public List<SpriteRenderer> enemys002 = new List<SpriteRenderer>();

    void Start()
    {
        for (int i = 0; i < room001.GetComponentsInChildren<SpriteRenderer>().Length; i++)
        {
            enemys001.Add(room001.GetComponentsInChildren<SpriteRenderer>()[i]);
        }
        for (int i = 0; i < room002.GetComponentsInChildren<SpriteRenderer>().Length; i++)
        {
            enemys002.Add(room002.GetComponentsInChildren<SpriteRenderer>()[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (roomIndex == 1)
            {
                foreach (var item in enemys001)
                {
                    item.GetComponent<meleeenemy>().taken_damage(5);
                }
            }
            if (roomIndex == 2)
            {
                foreach (var item in enemys002)
                {
                    item.GetComponent<meleeenemy>().taken_damage(5);
                }
            }
        }
    }

    public void GetItems(Vector2 enemyPos)
    {
        List<int> items = new List<int>();
        while (items.Count < 2)
        {
            int itemType = Random.Range(0, 3);
            if (!items.Contains(itemType))
            {
                items.Add(itemType);
            }
        }

        for (int i = 0; i < items.Count; i++)
        {
            ItemType itemType = (ItemType)i;
            switch (itemType)
            {
                case ItemType.Coin:
                    Instantiate(Resources.Load("Items/Coin") as GameObject, enemyPos += new Vector2(0, i), Quaternion.identity);
                    break;
                case ItemType.Chest:
                    Instantiate(Resources.Load("Items/Chest") as GameObject, enemyPos += new Vector2(0, i), Quaternion.identity);

                    break;
                case ItemType.Key:
                    Instantiate(Resources.Load("Items/Key") as GameObject, enemyPos += new Vector2(0, i), Quaternion.identity);

                    break;
                default:
                    break;
            }

        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.name.Contains("Door"))
        {
            int a = int.Parse(other.gameObject.name.Substring(other.gameObject.name.Length - 1, 1));
            if (a == 2)
            {
                if (enemys001.Count == 0)
                {
                    roomIndex = 2;
                    Destroy(other.gameObject);
                }
            }
            if (a == 3)
            {
                if (enemys002.Count == 0)
                {
                    roomIndex = 3;
                    Destroy(other.gameObject);
                }
            }
        }
        if (other.gameObject.name.Contains("Portal"))
        {
            int a = int.Parse(other.gameObject.name.Substring(other.gameObject.name.Length - 1, 1));
            skillSelectView.SetActive(true);
            for (int i = 0; i < skillSelectView.GetComponentsInChildren<Button>().Length; i++)
            {
                skillSelectView.GetComponentsInChildren<Button>()[i].onClick.AddListener( ()=>{
                    Debug.Log("获取技能"+i);
                    SceneManager.LoadScene(a);
                });
            }
        }
    }
}
