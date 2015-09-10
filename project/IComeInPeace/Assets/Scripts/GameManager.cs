using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace MainGame
{
	public class GameManager : MonoBehaviour
	{
		
		public static GameManager instance = null;
		//level and score, constant trough reset
		private int level = 1;
		private int score = 0;
		private bool isGameOver = false;
		//player
		public GameObject playerRef;
		private GameObject instancePlayer;
		private PlayerManager playerScript;
		public GameObject[] levelList;
		//enemies
		public GameObject[] enemyList;
		int totalEnemies;
		//current level
		private GameObject currentLvl;
		//canvas
		public GameObject canvasRef;
		private Text enemiesLeft;

		void Awake ()
		{
			if (instance == null)
				instance = this;
			else if (instance != this)
				Destroy (gameObject);    
			
			DontDestroyOnLoad (gameObject);

			//IMPORTANT!
			InitGame ();
		}
		//To restart or level change
		/*void OnLevelWasLoaded ()
	{
		//Call InitGame to initialize our level.
		InitGame ();
	}*/
		
		//Initializes the game for each level.
		public void InitGame ()
		{
			//orden: canvas, player, level (arreglar todo esto)
			Instantiate (canvasRef);
			instancePlayer = Instantiate (playerRef,transform.position,Quaternion.identity) as GameObject;
			playerScript = instancePlayer.GetComponent<PlayerManager> ();
			LevelSet ();
			isGameOver = false;
		}

		void LevelSet ()
		{
			currentLvl = Instantiate (levelList [level - 1], transform.position, transform.rotation) as GameObject;
			foreach (Transform child in currentLvl.transform) {
				if (child.tag == "MapPositions") {
					foreach (Transform granChild in child) {
						if (granChild.tag == "PlayerMapPos")
							instancePlayer.transform.position = granChild.position;
						else if (granChild.tag == "EnemyMapPos")
							EnemySet (granChild);
					}
				}
			}
			enemiesLeft = GameObject.Find ("EnemiesLeft").GetComponent<Text> ();
			enabled = true;
		}

		void EnemySet (Transform newPos)
		{
			int enemyRandom = (int)Mathf.Round (Random.Range (0, 1));
			GameObject newEnemy = Instantiate (enemyList [enemyRandom], newPos.position, Quaternion.identity) as GameObject;
			newEnemy.transform.SetParent (GameObject.FindGameObjectWithTag ("MapLevel").GetComponent<Transform> ());
			totalEnemies++;
		}

		public void Restart ()
		{
			Destroy (currentLvl);
			Invoke("LevelSet",2f);
		}
		
		public void GameOver (bool win)
		{
			enabled = false;
			if (win)
				level++;
			playerScript.playerLife = 100;
			Restart ();
		}

		void Update ()
		{
			totalEnemies = GameObject.FindGameObjectsWithTag ("Enemy").Length;
			enemiesLeft.text = "Enemies: " + totalEnemies;
			if (totalEnemies == 0)
				GameOver (true);
			else if (playerScript.playerLife <= 0)
				GameOver (false);
		}
	}
}