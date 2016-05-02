using UnityEngine;
using System.Collections;

public class MeleeTriggerScript : MonoBehaviour {

    public int damage = 20;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            col.gameObject.SendMessage("ApplyDamage", damage);
        }
        if (col.CompareTag("EnemyShoot"))
        {
            Destroy(col.gameObject);
        }
    }
}
