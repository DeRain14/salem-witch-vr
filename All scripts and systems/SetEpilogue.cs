using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetEpilogue : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Text>().text = GameManager.endResult;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
