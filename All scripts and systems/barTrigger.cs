using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barTrigger : MonoBehaviour {

    private GameObject kylePanel;
    [TextArea(15,20)] private string KyleText;
    private bool isTriggered = false;
	// Use this for initialization
	void Start () {
       
	}

	// Update is called once per frame
	void Update () {
        
	}

    private void onTriggerEnter(Collider other) {
        if (other.gameObject.GetComponent<OVRPlayerController>() && Application.isEditor) {
            //UIManager.EnablePanel(KyleText);
            Debug.Log("triggered");
            isTriggered = true;
        }
    }

    private void onTriggerExit(Collider other) {
        if (other.gameObject.GetComponent<OVRPlayerController>() && Application.isEditor) {
            //UIManager.DisablePanel();
            isTriggered = false;
        }
    }
}
