using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

	public float speed = 5f;
	Rigidbody2D playerBody;
	Animator playerAnim;
	void Awake()
	{
		playerBody = GetComponent<Rigidbody2D> ();
		playerAnim = GetComponent<Animator> ();
	}
	// Use this for initialization
	private void PlayerlMovement()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		Vector2 movement = new Vector2 (moveHorizontal, moveVertical);
		playerAnim.SetFloat ("HDirection",moveHorizontal);
		playerAnim.SetFloat ("YDirection",moveVertical);
		playerBody.velocity = movement * speed;
	}
	
	// Update is called once per frame
	void Update () 
	{
		PlayerlMovement ();
	}
}
