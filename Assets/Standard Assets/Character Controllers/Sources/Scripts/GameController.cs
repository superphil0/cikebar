using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GameController : MonoBehaviour {
	public GameObject car;
	public GameObject bike;
	public GameObject camera;
	public Texture tex;
	private bool gameOver = false;
	Camera cam;
	bool playing = false;
	int completionStep = 0;
	private GameObject active;
	private bool runOnce = true;
	// Use this for initialization
	void Start () {
		cam = camera.GetComponentInChildren<Camera>();
		active = bike;
		car.renderer.enabled = false;
		car.collider2D.enabled = false;
		car.audio.Stop ();
		cam.orthographicSize = active.GetComponent<VehicleController>().camSize;
		BackgroundBehaviour.speed = active.GetComponent<VehicleController> ().speed;
	}
	
	// Update is called once per frame
	void Update () {
		if (completionStep > 1)
						playing = false;
		if (Input.GetKeyDown (KeyCode.Space)) {
			switchUser();
				}
		Vector3 pos = camera.transform.position;
		pos.x = active.transform.position.x;
	
		//camera
		camera.transform.position =pos ;
	
		handleInput ();
		if (!playing)
			return;
	}
	void OnGUI()
	{
				if (gameOver) {
						GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), tex);
						GUI.TextArea (new Rect (10, 10, Screen.width / 4, Screen.height / 4), "To Restart please touch the screen with 3 finger");
				}

				if (completionStep>1) {
						GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), tex);
						GUI.TextArea (new Rect (10, 10, Screen.width / 4, Screen.height / 4), "Congratulations, you have seen seen the rider");
				}
				if (!playing&& runOnce) {
						GUI.TextArea (new Rect (Screen.width / 4, Screen.height / 4, Screen.width / 2, Screen.height / 2),
			              "Your perspective riding a bike is completly different than while driving\n" +
			              "Our game illustrates the difference\n"+
			              "Move left by tapping in the left corner of the screen\n"+
			              "move right by tapping in the right corner");
			GUI.TextArea (new Rect (Screen.width / 4, Screen.height / 4*3, Screen.width / 2, Screen.height ),
			              "WATCH OUT FOR THE CATS!");
						GUI.backgroundColor = Color.blue;
				}
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
	public bool isPlaying()
	{
				return playing;
		}
	void restart()
	{
		completionStep = 0;
		playing = true;
		if (active.name.Equals ("car")) {
						switchUser ();
						GameObject.Find ("World").GetComponent<BackgroundBehaviour> ().remake ();
				}
				else {
			GameObject.Find("World").GetComponent<BackgroundBehaviour>().remake();
		}

		gameOver = false;
	}
	public void completePart()
	{
				completionStep += 1;
	}
	void handleInput()
	{
				float offset = 0.0f;
				if (Input.GetKey (KeyCode.LeftArrow)) {
						offset = - 0.1f;
				}
		if (Input.GetKey (KeyCode.R)) {
			restart();
		}
		if (Input.GetKey (KeyCode.A)) {
			playing = true;
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
						offset = 0.1f;
				}
				if (Input.touchCount > 0) {
					if(!playing) 
			{	playing = true;
				runOnce = false;
			}
						if (Input.touchCount > 2)
							restart();
						else if(Input.touchCount > 1)
							;//switchUser();
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


