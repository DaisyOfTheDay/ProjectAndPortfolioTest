using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    Ray ray;
    Transform trans;

    bool canShoot = true;

    Animator anim;

    public GameObject bulletPrefab;
    public float shootInterval;

    // Start is called before the first frame update
    void Start()
    {
        trans = GetComponent<Transform>();
        anim = transform.parent.GetComponent<Animator>();
        ray = new Ray(trans.position, trans.forward);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(trans.position, trans.forward * 10);
        if (Input.GetAxis("Fire1") != 0)
        {
            shoot();
        }
    }

    void shoot()
    {
        if (canShoot)
        {
            anim.SetTrigger("shoot");
            canShoot = false;
            StartCoroutine(shootCooldown());
            Instantiate(bulletPrefab,trans.position,trans.rotation);

        }
    }

    IEnumerator shootCooldown()
    {
        yield return new WaitForSeconds(shootInterval);
        canShoot = true;
        StopCoroutine(shootCooldown());
    }
}
