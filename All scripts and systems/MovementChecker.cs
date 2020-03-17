using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementChecker : MonoBehaviour {
    private AudioSource audio;
    public int secondsToRepeat;
    public static bool finished;

	// Use this for initialization
	void Start () {
        InvokeRepeating("Alert", 1, secondsToRepeat);
        audio = GetComponent<AudioSource>();
        finished = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Alert() {
        if (finished) {
            CancelInvoke();
            Destroy(GetComponent<BoxCollider>());
        } else {
            audio.Play();
        }
    }


}
