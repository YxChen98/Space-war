using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("shooting game/EnemyRocket")]
public class EnemyRocket : rocket {
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.CompareTo("Player") != 0)
        {
            return;
        }

            Destroy(this.gameObject);
        
    }
}
