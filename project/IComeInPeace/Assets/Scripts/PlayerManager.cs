using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace MainGame
{
	public class PlayerManager : MonoBehaviour
	{
		GameObject weaponRef;
		public int playerLife = 100;
		public float speed = 5f;
		Rigidbody2D playerBody;
		Animator playerAnim;
		Vector2 movement;
		//Ataque de fuego
		Vector2 shootDir; //se basa en el movimiento, diferenciar movimiento de direccion de ataque?
		bool isShooting = false;
		//Ataque cuerpo a cuerpo
		bool isAttacking = false;
		float attackTimer = 0;
		float attackCoolDown = 0.3f;
		public GameObject attackTrigger;
		//Barra de vida y energia
		RectTransform lifePlayer;
		float initWidth; //pesimo esta variable, hay q arreglar esto
		RectTransform weaponEnergy;

		void Awake ()
		{
			playerBody = GetComponent<Rigidbody2D> ();
			playerAnim = GetComponent<Animator> ();
			lifePlayer = GameObject.Find ("LifePlayer").GetComponent<RectTransform> ();
			weaponEnergy = GameObject.Find ("WeaponEnergy").GetComponent<RectTransform> ();
			initWidth = lifePlayer.rect.width;
			weaponRef = GameObject.Find ("Weapon");
			attackTrigger.GetComponent<Collider2D>().enabled = false;
		}
		// Use this for initialization
		private void PlayerMovement ()
		{
			float moveHorizontal = Input.GetAxisRaw ("Horizontal");
			float moveVertical = Input.GetAxisRaw ("Vertical");
			movement = new Vector2 (moveHorizontal, moveVertical);
			if (moveHorizontal == 0 && moveVertical == 0)
				shootDir = new Vector2 (0f, -1f);
			else
				shootDir = movement;
			playerAnim.SetFloat ("HDirection", moveHorizontal);
			playerAnim.SetFloat ("YDirection", moveVertical);
			playerBody.velocity = movement * speed;
		}

		void ApplyDamage (int damage)
		{
			playerLife -= damage;
			lifePlayer.sizeDelta = new Vector2 ((initWidth / 100) * playerLife, 30);
			if (playerLife <= 0)
				transform.position = new Vector3 (-100f, -100, 0);
		}

		void UseWeapon()
		{
			weaponRef.GetComponent<WeaponBehavior> ().Shoot (shootDir);
			weaponEnergy.sizeDelta = new Vector2 ((initWidth / 100) * weaponRef.GetComponent<WeaponBehavior> ().Energy, 30);
		}

        void MeleeAttack()
        {
            isAttacking = true;
            attackTimer = attackCoolDown;
            //ubica el trigger segun direccion del ataque
            attackTrigger.transform.localPosition = new Vector3(shootDir.x * 25f, shootDir.y * 25f, 0);
            float angle = (shootDir.y != 0) ? -45f : -90f;
            float direc = (shootDir.x != 0 && shootDir.y == 0) ? shootDir.x : shootDir.x * shootDir.y;
            attackTrigger.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, direc * angle));
            attackTrigger.GetComponent<Collider2D>().enabled = true;
        }

		void Update ()
		{
			PlayerMovement ();
			if (Input.GetKeyDown ("space")) {
				UseWeapon();
			}
            if (Input.GetKeyDown("k") && !isAttacking)
            {
                MeleeAttack();
            }
            //Controla que pueda atacar de nuevo segun el cooldown
            if (isAttacking)
            {
                if (attackTimer > 0)
                {
                    attackTimer -= Time.deltaTime;
                }
                else 
                {
                    isAttacking = false;
                    attackTrigger.GetComponent<Collider2D>().enabled = false;
                }
            }
		}
	}
}
