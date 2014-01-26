using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GameController : MonoBehaviour {
	public GameObject car;
	public GameObject bike;
	public GameObject camera;
	private bool gameOver = false;
	Camera cam;
	private GameObject active;

	// Use this for initialization
	void Start () {
		cam = camera.GetComponentInChildren<Camera>();
		active = bike;
		car.renderer.enabled = false;
		car.collider2D.enabled = false;
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
	public void setGameOver()
	{
		// todo add explanation
		gameOver = true;

	}


	public bool isGameOver()
	{
		return gameOver;
	}
	public void reset()
	{
		switchUser ();
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
			active.GetComponent<VehicleController> ().activateBike (true);
			GameObject.Find("World").GetComponent<BackgroundBehaviour>().remake();
		}
		else
		{
			active.GetComponent<VehicleController> ().activateBike (false);
			active = car;
			active.GetComponent<VehicleController> ().activateCar (true);
		}
		BackgroundBehaviour.speed = active.GetComponent<VehicleController> ().speed;
		cam.orthographicSize = active.GetComponent<VehicleController>().camSize;
	
	}
}


