using UnityEngine;
using System.Collections;


public class VehicleController : MonoBehaviour {

	Vector3 newPos;

	// Use this for initialization
	public float speed = 1.0f;
	public float camSize = 5.0f;
	// size = 10
	float borderX = 3f;
	void Start () {
		bool isAndroid = (Application.platform == RuntimePlatform.Android);
		newPos = this.gameObject.transform.position;
		//this.gameObject.transform.position = pos;

	}
	
	// Update is called once per frame
	void Update () {

		
		newPos =this.gameObject.transform.position;


		
	}
	public void move(float offset)
	{
		if (newPos == Vector3.zero)
						return;
		if (newPos.x + offset < borderX && newPos.x + offset > -1.0f * borderX) {
			newPos.x = newPos.x + offset;
			this.gameObject.transform.position = newPos;
		}
	}
}
