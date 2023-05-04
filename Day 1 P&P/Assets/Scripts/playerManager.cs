using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerManager : MonoBehaviour , IDamage
{

    [Header("------------Player Stats------------")]
    [SerializeField] int HP = 3;
    public float jumpStrength = 2;
    public float dashStrength = 2;
    public int dashInterval = 3;

    int hpOrig;

    // Start is called before the first frame update
    void Start()
    {
        hpOrig = HP;
        SpawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void heal (int ammount)
    {
        HP += ammount;
    }

    public void takeDamage(int ammount) {
        HP -= ammount;
        if (HP< 0)
        {
            GameManager.instance.lose();
        }

    }

    public void SpawnPlayer()
    {
        transform.position = GameManager.instance.playerSpawnPoint.transform.position;
        HP = hpOrig;
    }
}
