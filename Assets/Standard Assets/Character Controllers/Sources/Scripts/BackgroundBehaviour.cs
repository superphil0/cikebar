using UnityEngine;
using System.Collections;
using System.Collections.Generic;
// http://www.fraghalt.de/blog/unity3d-endless-runner-tutorial/
public class BackgroundBehaviour : MonoBehaviour {
	public Transform prefab;
	private Vector2 position;		

	private LinkedList<Transform> roads = new LinkedList<Transform>();
	
	private float posX = 0.0f;
	private int numberOfRoads = 3;
	// Use this for initialization
	void Start () {
		
		// Init the scene with some road-pieces
		for(int i=0;i < numberOfRoads;i++) {
			Transform road = Instantiate(prefab) as Transform;
			road.Translate(  posX,i * road.renderer.bounds.size.y, 0);
			roads.AddLast(road);
		}
	}

	// Use this for initialization
	void Update () {
		
		Transform firstRoad = roads.First.Value;
		Transform lastRoad = roads.Last.Value;
		
		// Create a new road if the first one is not 
		// in sight anymore and destroy the first one
		if(firstRoad.localPosition.y < -25f) {
			roads.Remove(firstRoad);
			Destroy(firstRoad.gameObject);
			
			Transform newRoad = Instantiate(prefab) as Transform;
			newRoad.Translate(posX,lastRoad.localPosition.y + lastRoad.renderer.bounds.size.y ,0);
		  roads.AddLast(newRoad);
		  
		  }
		
		// Move the available roads along the z-axis
		foreach(Transform road in roads) {
			road.Translate( 0f,-8f * Time.deltaTime,0f);      
		}
	}

	

}
