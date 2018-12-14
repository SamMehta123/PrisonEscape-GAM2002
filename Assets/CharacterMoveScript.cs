using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoveScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 Direction = Vector2.zero;
        Direction.y = Input.GetAxis("Vertical");
        Direction.x = Input.GetAxis("Horizontal");
        print(Input.GetAxis("Vertical"));
        //print(Direction);
        transform.Translate(Direction);
	}
}
