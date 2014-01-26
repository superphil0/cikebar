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
	public GameController controller;
	private Animator animator;
	// size = 10
	float borderX = 3f;
	void Start () {
		bool isAndroid = (Application.platform == RuntimePlatform.Android);
		animator = GetComponent<Animator> ();
		controller = GameObject.Find ("GameController").GetComponent<GameController> ();
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
		if(behave.getGameProgress ()>0 && !activeLinerenderer) 
			path.AddLast (new Vector2( newPos.x,behave.getGameProgress ()));
		float offset = behave.getGameProgress()-initalYoffset;
		if(activeLinerenderer)
		{
			line.SetVertexCount(path.Count);
			int i = 0;
			float xPath = 0.0f;
			float last = 0.0f;
			foreach( Vector2 pos in path)
			{
				if(behave.getGameProgress() >last && behave.getGameProgress()<=pos.y)
					xPath = pos.x;
				line.SetPosition(i,new Vector3(pos.x,pos.y-offset,-.01f));
				i++;
				last = pos.y;
			}
			newPos.x = xPath;
			this.gameObject.transform.position = newPos;
		}
	}
	public void activateBike(bool state)
	{
		if (state)
			reset ();
		// TODO change sprite here
		activeLinerenderer = !state;
		if (activeLinerenderer)
			path.RemoveLast ();
		this.gameObject.collider2D.enabled = state;
		this.gameObject.GetComponentInChildren<LineRenderer> ().enabled = !state;
		if (state)
						audio.Play ();
		else
				audio.Stop ();
	}
	
	public void activateCar(bool state)
	{
		if (state)
			reset ();
		// TODO change sprite here
		this.gameObject.collider2D.enabled = state;
		this.gameObject.renderer.enabled = state;
		if (state)
						audio.Play ();
				else
						audio.Stop ();
	}
	public void reset()
	{
		path.Clear ();
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log(other.name);
		controller.setGameOver ();

	}

	public void move(float offset)
	{
		animator.SetBool ("straight", offset == 0.0f);
		animator.SetFloat ("direction", offset);
		if (newPos == Vector3.zero)
						return;
		if (newPos.x + offset < borderX && newPos.x + offset > -1.0f * borderX) {
			newPos.x = newPos.x + offset;
			this.gameObject.transform.position = newPos;
		}
	}
}
