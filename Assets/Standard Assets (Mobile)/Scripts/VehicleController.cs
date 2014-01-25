using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class VehicleController : MonoBehaviour {

	Vector3 newPos;

	// Use this for initialization
	public float speed = 1.0f;
	public float camSize = 5.0f;
	public float initalYoffset = -3.0f;
	private LinkedList<Vector2> path;
	private BackgroundBehaviour behave;
	private bool activeLinerenderer;
	private LineRenderer line;
	// size = 10
	float borderX = 3f;
	void Start () {
		bool isAndroid = (Application.platform == RuntimePlatform.Android);

		newPos = this.gameObject.transform.position;
		newPos.y = initalYoffset;
		this.gameObject.transform.position = newPos;
		//this.gameObject.transform.position = pos;
		path = new LinkedList<Vector2> ();
		behave = GameObject.Find ("World").GetComponent<BackgroundBehaviour> ();
		line = this.gameObject.GetComponentInChildren<LineRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		newPos =this.gameObject.transform.position;
		path.AddLast (new Vector2( newPos.x,behave.getGameProgress ()));
		float offset = behave.getGameProgress()-initalYoffset;
		if(activeLinerenderer)
		{
			line.SetVertexCount(path.Count);
			int i = 0;
			foreach( Vector2 pos in path)
			{
				line.SetPosition(i,new Vector3(pos.x,pos.y-offset,-.01f));
				i++;
			}
		}
	}
	public void activateBike(bool state)
	{
		// TODO change sprite here
		activeLinerenderer = state;
		this.gameObject.renderer.enabled = !state;
		//this.gameObject.SetActive (state);
	}
	public void activateCar(bool state)
	{
		// TODO change sprite here
		activeLinerenderer = false;
		this.gameObject.renderer.enabled = state;
		//this.gameObject.SetActive (state);
		gameObject.SetActive (true);
	}
	public void reset()
	{
		path.Clear ();
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
