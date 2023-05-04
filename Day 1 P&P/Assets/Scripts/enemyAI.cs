using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyAI : MonoBehaviour , IDamage
{
    [Header("---------Components-------")]
    [SerializeField] Renderer Model;
    [SerializeField] NavMeshAgent Agent;
    Color colorOrig;
    bool isShooting;

    [Header("---------Enemy Stats-------")]
    [SerializeField] int HP = 3;
    [SerializeField] float fireDistance = 100;
    [SerializeField] float fireRate = 0.5f;
    [SerializeField] int fireDamage = 1;

    [Header("---------Prefabs---------")]
    [SerializeField] GameObject bullet;


    // Start is called before the first frame update
    void Start()
    {
        Model = GetComponent<Renderer>();
        colorOrig = Model.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        Agent.SetDestination(GameManager.instance.Player.transform.position);
        

        if (HP<= 0) {
            Destroy(gameObject);
        }

        if (!isShooting)
        {
            StartCoroutine(shoot());
        }


    }

    public void takeDamage(int damage)
    {
        HP -= damage;
        StartCoroutine(damageIndicator());
    }

    IEnumerator damageIndicator()
    {
        Model.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        Model.material.color = colorOrig;
        StopCoroutine(damageIndicator());
    }

    IEnumerator shoot()
    {
        isShooting = true;
        yield return new WaitForSeconds(fireRate);
        Instantiate(bullet, transform.position, transform.rotation);
        isShooting = false;
    }
}
