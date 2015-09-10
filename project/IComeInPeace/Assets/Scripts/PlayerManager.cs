using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace MainGame
{
	public class PlayerManager : MonoBehaviour
	{

		public int playerLife = 100;
		public GameObject PlayerBullet;
		public float speed = 5f;
		Rigidbody2D playerBody;
		Animator playerAnim;
		Vector2 movement;
		Vector2 shootDir;
		RectTransform lifePlayer;
		float initWidth;

		void Awake ()
		{
			playerBody = GetComponent<Rigidbody2D> ();
			playerAnim = GetComponent<Animator> ();
			lifePlayer = GameObject.Find ("LifePlayer").GetComponent<RectTransform> ();
			initWidth = lifePlayer.rect.width;
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

		void Update ()
		{
			PlayerMovement ();
			if (Input.GetKeyDown ("space")) {
				GameObject go = Instantiate (PlayerBullet, transform.position, transform.rotation) as GameObject;
				go.GetComponent<Rigidbody2D> ().AddForce (shootDir * 20000, ForceMode2D.Force);
			}
		}
	}
}
