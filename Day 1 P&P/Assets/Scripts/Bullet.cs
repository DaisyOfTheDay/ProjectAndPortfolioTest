using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]Rigidbody rb;
    [SerializeField] float speed;
    [SerializeField] int damage;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward* speed;
        Destroy(this.gameObject, 2);
    }



    private void OnTriggerEnter(Collider other)
    {
        IDamage damageable= other.GetComponent<IDamage>();
        if (damageable != null)
        {
            damageable.takeDamage(1);
        }

    }

}
