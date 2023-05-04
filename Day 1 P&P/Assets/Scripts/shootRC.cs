using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootRC : MonoBehaviour
{

    bool canShoot = true;

    [Header("Muzzle Flash and Bullet Hole")]
    public Transform muzzleFlash;
    public Transform bulletDecal;
    public float shootInterval;

    public float bulletTolerance;


    Ray ray;
    Transform trans;

    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = transform.parent.GetComponent<Animator>();
        trans = GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(trans.position, trans.forward * 100);
        if (Input.GetAxis("Fire1") != 0 && !GameManager.instance.isPaused)
        {
            shoot();
        }
    }

    void shoot()
    {
        if (canShoot)
        {
            //shoot animation
            anim.SetTrigger("shoot");

            //muzzle flash
            Instantiate(muzzleFlash,trans.position,trans.rotation,trans);

            //Ray
            ray = new Ray(GetComponentInParent<Camera>().transform.position, GetComponentInParent<Camera>().transform.forward * 10);

            //fire cooldown
            canShoot = false;
            StartCoroutine(shootCooldown());

            //testing hit
            RaycastHit hit;
            if (Physics.SphereCast(ray, bulletTolerance, out hit, float.PositiveInfinity)) {
                IDamage damageable = hit.collider.GetComponent<IDamage>();
                if (damageable != null)
                {
                    damageable.takeDamage(1);
                }
                //make bullet hole
                if (hit.collider.gameObject.layer != 7)
                {
                    Instantiate(bulletDecal, hit.point + (hit.normal * 0.01f), Quaternion.FromToRotation(Vector3.forward, hit.normal));
                }

            }



        }
    }

    IEnumerator shootCooldown()
    {
        yield return new WaitForSeconds(shootInterval);
        canShoot = true;
        StopCoroutine(shootCooldown());
    }
}
