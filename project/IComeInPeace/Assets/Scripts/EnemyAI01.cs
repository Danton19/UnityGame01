﻿using UnityEngine;
using System.Collections;

namespace MainGame
{
	public class EnemyAI01 : MonoBehaviour
	{

		int enemyLife;
		public int totalLife;
		public GameObject EnemyBullet;
		Transform target;
		public int moveSpeed;
		public float attackRange;
		Transform thisTransform;
		Rigidbody2D thisBody;
		float fireRate = 2f;
		float nextShoot = 0;
		Animator enemyAnimator;
		public GameObject healthBar;
		GameObject healthBarInstance;

		void Awake ()
		{
			thisBody = GetComponent<Rigidbody2D> ();
			thisTransform = GetComponent<Transform> ();
			enemyAnimator = GetComponent<Animator> ();
			enemyLife = totalLife;
		}
	
		void Start ()
		{
			target = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
			healthBarInstance = Instantiate (healthBar, transform.position, transform.rotation) as GameObject;
			healthBarInstance.transform.SetParent (GameObject.FindGameObjectWithTag ("CanvasInGame").GetComponent<Transform> ());
			healthBarInstance.GetComponent<HealthBarBasic> ().targetObject = gameObject.transform;
		}

		void ApplyDamage (int damage)
		{
			enemyLife -= damage;
			float lifePer = (enemyLife * 100) / totalLife;
			healthBarInstance.GetComponent<HealthBarBasic> ().ResizeBar (lifePer);
			if (enemyLife <= 0) {
				Destroy (gameObject);
				Destroy (healthBarInstance);
			}
		}

		void AttackOnPlayer ()
		{
			if (Time.time >= nextShoot) {
				nextShoot = Time.time + fireRate;
				GameObject go = Instantiate (EnemyBullet, transform.position, transform.rotation) as GameObject;
				//go.transform.SetParent(gameObject.transform);
				Vector2 toTarget = new Vector2 (target.position.x - transform.position.x, target.position.y - transform.position.y);
				toTarget.Normalize ();
				go.GetComponent<Rigidbody2D> ().AddForce (toTarget * 20000, ForceMode2D.Force);
			}
		}

		void DirectionAnim (Vector3 direction)
		{
			if (Mathf.Abs (direction.x) < Mathf.Abs (direction.y)) {
				enemyAnimator.SetFloat ("HDirection", 0);
				enemyAnimator.SetFloat ("YDirection", direction.y);
			} else {
				enemyAnimator.SetFloat ("YDirection", 0);
				enemyAnimator.SetFloat ("HDirection", direction.x);
			}
		}

		void Update ()
		{
			if (target && Vector3.Distance (target.position, thisTransform.position) < attackRange + 100f) {//followrange
				if (Vector3.Distance (target.position, thisTransform.position) > attackRange) {
					Vector3 dir = target.position - thisTransform.position;
					dir.Normalize ();
					DirectionAnim (dir);
					thisBody.velocity = dir * moveSpeed;
				} else {
					AttackOnPlayer ();
					thisBody.velocity = new Vector3 (0, 0);
				}
			}
		}
	}
}