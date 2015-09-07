using UnityEngine;
using System.Collections;

public class BulletsScript : MonoBehaviour {
	GameObject bulletParent;
	public int damage;

	void OnTriggerEnter2D(Collider2D coll) 
	{
		if (gameObject.tag == "EnemyShoot") 
		{
			if (coll.gameObject.tag == "Player"){
				coll.gameObject.SendMessage ("ApplyDamage", damage);
				Destroy(gameObject);
			}
		}
		if (gameObject.tag == "PlayerShoot") 
		{
			if (coll.gameObject.tag == "Enemy"){
				coll.gameObject.SendMessage ("ApplyDamage", damage);
				Destroy(gameObject);
			}
		}

	}
	void OnBecameInvisible()
	{
		Destroy(gameObject);
	}
}
