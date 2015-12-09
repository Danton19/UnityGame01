using UnityEngine;
using System.Collections;

public class WeaponBehavior : MonoBehaviour {

	public GameObject Munition;
	public int Energy = 100;
	public int BulletSpeed = 20000;

	public void Shoot(Vector2 shootDir)
	{
		GameObject go = Instantiate (Munition, transform.position, transform.rotation) as GameObject;
		go.GetComponent<BulletsScript> ().ShootWeapon (shootDir, BulletSpeed);
		Energy -= 10;
	}
}
