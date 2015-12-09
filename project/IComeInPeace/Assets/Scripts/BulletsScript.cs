using UnityEngine;
using System.Collections;

public class BulletsScript : MonoBehaviour {

	public int damage = 20;

	void OnTriggerEnter2D(Collider2D coll) 
	{
		if (gameObject.tag == "EnemyShoot") 
		{
			if (coll.gameObject.tag == "Player")
			{
				coll.gameObject.SendMessage ("ApplyDamage", damage);
					Destroy(gameObject);
			}
		}
		if (gameObject.tag == "PlayerShoot") 
		{
			if (coll.gameObject.tag == "Enemy")
			{
				coll.gameObject.SendMessage ("ApplyDamage", damage);
					Destroy(gameObject);
			}
		}

	}

	public void ShootWeapon(Vector2 direction, int bulletSpeed)
	{
		GetComponent<Rigidbody2D> ().AddForce (direction * bulletSpeed, ForceMode2D.Force);
	}

	void OnBecameInvisible()
	{
		Destroy(gameObject);
	}
}
