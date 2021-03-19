using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float radius = 5f;
    public float force = 700f;
    // Start is called before the first frame update
    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
            foreach (Collider nearlyObject in colliders)
            {
                Rigidbody rb = nearlyObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(force, transform.position, radius);
                    Destroy(gameObject);
                }
            }
    }
    
    void OnCollisionEnter(Collision collision)
    {
        Explode();
    }
}
