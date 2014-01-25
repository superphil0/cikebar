using UnityEngine;
using System.Collections;


public class CarController : MonoBehaviour {
	Vector3 newPos;
	// Use this for initialization
	float speed = 1.0f;
	// size = 10
	float borderX = 3f;
	void Start () {
		bool isAndroid = (Application.platform == RuntimePlatform.Android);
		Vector3 pos = new Vector3 (0, 0 ,0);
		//this.gameObject.transform.position = pos;

	}
	
	// Update is called once per frame
	void Update () {
		newPos =this.gameObject.transform.position;
		float offset = 0.0f;
		if (Input.GetKey (KeyCode.LeftArrow)) {
			offset = - 0.1f;
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			offset = 0.1f;
		}
		if(Input.touchCount > 0)
		{
			foreach(Touch t in Input.touches)
			{
				// speed * pos - halbe ( renormalization mddle center
				offset =  speed *(t.position.x / Screen.width - 0.5f);
				Debug.Log(offset);
				Debug.Log("offset" + offset);
			}
		}
		if( newPos.x + offset < borderX && newPos.x + offset > -1.0f*borderX)
		{
			newPos.x = newPos.x + offset;
			this.gameObject.transform.position = newPos;
		}
		
	}
}
