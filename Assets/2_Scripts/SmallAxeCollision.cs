using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallAxeCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyBlue"))
        {
            int _dmg = Random.Range(3, 5);
            other.GetComponent<Enemy>().TakeDamage(_dmg);
        }
    }
}
