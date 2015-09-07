using UnityEngine;
using System.Collections;

public class HealthBarBasic : MonoBehaviour {

	public Transform targetObject;
	RectTransform rt;
	float initWidth;

	void Start()
	{
		rt = gameObject.GetComponent<RectTransform> ();
		initWidth = rt.rect.width;
	}

	public void ResizeBar(float lifeValue)
	{
		float newW = (initWidth / 100) * lifeValue;
		rt.sizeDelta = new Vector2( newW, rt.rect.height);
	}
	void Update ()
	{
		if (targetObject) {
			Vector3 wantedPos =  Camera.main.WorldToScreenPoint (targetObject.position);
			transform.position = new Vector3(wantedPos.x-15f, wantedPos.y, wantedPos.z);//-15f ajuste para centrar sobre personaje, necesita calcularse
		}
	}
}
