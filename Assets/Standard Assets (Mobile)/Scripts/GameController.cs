using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public GameObject car;
	public GameObject bike;
	// Use this for initialization
	void Start () {
		bike.SetActive (false);

	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
						car.SetActive (false);
						bike.SetActive (true);
				}
	
	}
}
