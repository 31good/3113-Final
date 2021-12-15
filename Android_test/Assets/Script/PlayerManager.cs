using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerManager : MonoBehaviour
{
    // Start is called before the first frame update

    private static PlayerManager Instance;
    public static PlayerManager GetInstance()
    {
        return Instance;
    }
    private void Awake()
    {
        Instance = this;
    }
    public GameObject room001;
    public GameObject room002;

    public int roomIndex = 1;//玩家所在的房间号
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
            enemys002.Add(room001.GetComponentsInChildren<SpriteRenderer>()[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            foreach (var item in enemys001)
            {
                item.GetComponent<meleeenemy>().taken_damage(5);
            }
        }
    }
}
