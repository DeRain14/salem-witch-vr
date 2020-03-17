using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Book with text UI. Picked up w/ touch controllers in VR. 
/// Ryan Quistorff
/// </summary>
public class Book : MonoBehaviour {
    [TextArea(15, 20)]
    public string bookText;
    private bool reading;
    private OVRGrabbable grabbed;
    private GameObject bookPanel;
    private GameObject ui;

	// Use this for initialization
	void Start () {
        reading = false;
        grabbed = GetComponent<OVRGrabbable>();
        ui = transform.GetChild(0).gameObject;
        ui.GetComponentInChildren<Text>().text = bookText;
        if (GameManager.InVR)
        {
            ui.transform.localPosition = new Vector3(0, 0, ui.transform.localPosition.z);
            ui.transform.localScale /= 3;
        }
        ui.SetActive(false);
	}

	void Update () {
        if (!grabbed.isGrabbed && reading) {
            ui.SetActive(false);
            reading = false;
        } else if (grabbed.isGrabbed && !reading) {
            ui.SetActive(true);
            reading = true;
        }
	}

    void OnTriggerEnter(Collider col) {
        if (col.gameObject.GetComponent<OVRPlayerController>()) {
            ui.SetActive(true);
            HitBlur.HitPlayer(null,null);

        }
    }

    void OnTriggerExit(Collider col) {
        if (col.gameObject.GetComponent<OVRPlayerController>())
        {
            ui.SetActive(false);
        }
    }
}
