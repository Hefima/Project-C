using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    public float damage;
    public float speed;
    public Rigidbody rb;
    public float timeAllive;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * speed, ForceMode.Impulse);
    }

    IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(timeAllive);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        var hit = other.GetComponent<IDamagable>();

        if (hit != null)
        {
            hit.TakeDamage(damage);
        }
    }
}
