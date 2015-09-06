using UnityEngine;
using System.Collections;

public class BulletsScript : MonoBehaviour {
	GameObject bulletParent;
	void Start () 
	{
		/*bulletParent = this.transform.parent.gameObject;
		transform.forward = bulletParent.transform.forward; */
	}
	void OnTriggerEnter2D(Collider2D coll) 
	{
		if (gameObject.tag == "EnemyShoot") 
		{
			if (coll.gameObject.tag == "Player"){
				coll.gameObject.SendMessage ("ApplyDamage", 20);
				Destroy(gameObject);
			}
		}
		if (gameObject.tag == "PlayerShoot") 
		{
			if (coll.gameObject.tag == "Enemy"){
				coll.gameObject.SendMessage ("ApplyDamage", 20);
				Destroy(gameObject);
			}
		}

	}
	void OnBecameInvisible()
	{
		Destroy(gameObject);
	}
}
