using UnityEngine;
using System.Collections;


public class CarController : MonoBehaviour {
	Vector3 newPos;
	// Use this for initialization
	void Start () {
		bool isAndroid = (Application.platform == RuntimePlatform.Android);
		Vector3 pos = new Vector3 (0, 0 ,0);
		//this.gameObject.transform.position = pos;

	}
	
	// Update is called once per frame
	void Update () {
		if(Input.touchCount > 0)
		{
			foreach(Touch t in Input.touches)
			{
				newPos =this.gameObject.transform.position;
				newPos.y = t.position.x / Screen.width;		
				Debug.Log(t.position);
				this.gameObject.transform.position = newPos;
			}
		}

	}
}
