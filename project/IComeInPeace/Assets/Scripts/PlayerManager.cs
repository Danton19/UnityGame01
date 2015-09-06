using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {

	public int playerLife = 100;
	public GameObject PlayerBullet;
	public float speed = 5f;
	Rigidbody2D playerBody;
	Animator playerAnim;
	Vector2 movement;
	Vector2 shootDir;
	RectTransform lifeText;
	void Awake()
	{
		playerBody = GetComponent<Rigidbody2D> ();
		playerAnim = GetComponent<Animator> ();
		lifeText = GameObject.Find ("LifeText").GetComponent<RectTransform> ();
	}
	// Use this for initialization
	private void PlayerMovement()
	{
		float moveHorizontal = Input.GetAxisRaw  ("Horizontal");
		float moveVertical = Input.GetAxisRaw  ("Vertical");
		movement = new Vector2 (moveHorizontal, moveVertical);
		if (moveHorizontal == 0 && moveVertical == 0)
			shootDir = new Vector2 (0f, -1f);
		else
			shootDir = movement;
		playerAnim.SetFloat ("HDirection",moveHorizontal);
		playerAnim.SetFloat ("YDirection",moveVertical);
		playerBody.velocity = movement * speed;
	}
	void ApplyDamage(int damage)
	{
		playerLife -= damage;
		lifeText.sizeDelta = new Vector2( 60+playerLife, 30);
		if (playerLife <= 0)
			Destroy (gameObject);
	}
	// Update is called once per frame
	void Update () 
	{
		PlayerMovement ();
		if (Input.GetKeyDown ("space")) 
		{

			GameObject go = Instantiate(PlayerBullet, transform.position,transform.rotation) as GameObject;
			//go.transform.SetParent(gameObject.transform);
			go.GetComponent<Rigidbody2D>().AddForce(shootDir * 20000, ForceMode2D.Force);
		}
	}
}
