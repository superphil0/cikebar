using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GameController : MonoBehaviour {
	public GameObject car;
	public GameObject bike;
	public GameObject camera;

	Camera cam;
	private GameObject active;

	// Use this for initialization
	void Start () {
		cam = camera.GetComponentInChildren<Camera>();
		car.SetActive (false);
		active = bike;
		cam.orthographicSize = active.GetComponent<VehicleController>().camSize;
		BackgroundBehaviour.speed = active.GetComponent<VehicleController> ().speed;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			switchUser();
				}
		Vector3 pos = camera.transform.position;
		pos.x = active.transform.position.x;
	
		//camera
		camera.transform.position =pos ;
		handleInput ();
	}
	void handleInput()
	{
				float offset = 0.0f;
				if (Input.GetKey (KeyCode.LeftArrow)) {
						offset = - 0.1f;
				}
				if (Input.GetKey (KeyCode.RightArrow)) {
						offset = 0.1f;
				}
				if (Input.touchCount > 0) {
						if (Input.touchCount > 1)
							switchUser();
						else
								foreach (Touch t in Input.touches) {
										// speed * pos - halbe ( renormalization mddle center
										offset = active.GetComponent<VehicleController> ().speed * 
													(t.position.x / Screen.width - 0.5f);
										offset = Mathf.Sign (offset) * 0.1f;
										Debug.Log (offset);
										Debug.Log ("offset" + offset);
								}
				}
				
		active.GetComponent<VehicleController> ().move (offset);
		}
	public void switchUser()
	{

		if (active.name.Equals ("car")) 
		{
			active.GetComponent<VehicleController> ().activateCar (false);
			active = bike;
			active.GetComponent<VehicleController> ().activateBike (false);
		}
		else
		{
			active.GetComponent<VehicleController> ().activateBike (false);
			active = car;
			active.GetComponent<VehicleController> ().activateCar (false);
		}
		BackgroundBehaviour.speed = active.GetComponent<VehicleController> ().speed;
		cam.orthographicSize = active.GetComponent<VehicleController>().camSize;
	
	}
}


