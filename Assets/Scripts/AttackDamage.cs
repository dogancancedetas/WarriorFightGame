using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDamage : MonoBehaviour
{
    public LayerMask layer;

    public float radius = 1;
    public float damage = 1;

    // Update is called once per frame
    void Update()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, layer);

        if (hits.Length > 0)
        {
            gameObject.SetActive(false);

            hits[0].GetComponent<HealthScript>().ApplyDamage(damage);


        }
    }
}
