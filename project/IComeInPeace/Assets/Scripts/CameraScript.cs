using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public Transform target;
	public float yOffset = 0;
	
	
	void Update() 
	{
		if(target)
			this.transform.position = new Vector3(target.position.x, target.position.y + yOffset, transform.position.z);
	}
}
