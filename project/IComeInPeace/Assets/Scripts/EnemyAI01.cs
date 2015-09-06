using UnityEngine;
using System.Collections;

public class EnemyAI01 : MonoBehaviour {

	public int enemyLife = 40;
	public GameObject EnemyBullet;
	Transform target;
	public int moveSpeed = 100;
	float range =200f;
	Transform thisTransform;
	Rigidbody2D thisBody;
	float fireRate = 2f;
	float nextShoot = 0;

	void Awake()
	{
		thisBody = GetComponent<Rigidbody2D> ();
		thisTransform = GetComponent<Transform>();
	}
	
	void Start()
	{
		target = GameObject.Find ("PlayerTest01").GetComponent<Transform>();		
	}
	void ApplyDamage(int damage)
	{
		enemyLife -= damage;
		Destroy (gameObject);
	}
	void AttackOnPlayer()
	{
		if (Time.time >= nextShoot) 
		{
			nextShoot = Time.time + fireRate;
			GameObject go = Instantiate(EnemyBullet, transform.position,transform.rotation) as GameObject;
			//go.transform.SetParent(gameObject.transform);
			Vector2 toTarget = new Vector2(target.position.x - transform.position.x , target.position.y- transform.position.y);
			go.GetComponent<Rigidbody2D>().AddForce(toTarget * 100, ForceMode2D.Force);
		}
	}
	void Update () 
	{
		if (target) {
			if (Vector3.Distance (target.position, thisTransform.position) > range) {
				Vector3 dir = target.position - thisTransform.position;
				dir.Normalize ();
				//thisTransform.position += dir * moveSpeed * Time.deltaTime;
				thisBody.velocity = dir * moveSpeed;
			} else {
				AttackOnPlayer ();
				thisBody.velocity = new Vector3 (0, 0);
			}
		}
	}
}