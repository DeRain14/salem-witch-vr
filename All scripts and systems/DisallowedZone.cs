using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;


public class DisallowedZone : MonoBehaviour {

    public static bool inValidZone;
    private bool fadingCam = false;
    private static AudioClip offPathClip;
    private static AudioSource speaker;
    private static BlurOptimized blur;

    void Start()
    {
        offPathClip = Resources.Load("Audio/WrongWay") as AudioClip;
        inValidZone = true;
        speaker = FindObjectOfType<FollowPlayer>().GetComponent<AudioSource>();
        blur = FindObjectOfType<BlurOptimized>();
    }


	void Update () {
        if (fadingCam && blur.blurSize < 6) {
            blur.blurSize += Time.deltaTime;
        }
	}

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<OVRPlayerController>())
        {
            inValidZone = true;
            CancelInvoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<OVRPlayerController>()) {
            inValidZone = false;
            HitBlur.HitPlayer(offPathClip, speaker);
            Invoke("CheckValid", 5);
        }

    }

    //restart
    void CheckValid() {
        if (!inValidZone) {
            speaker.Play();
            Invoke("Retry", 2);
            HitBlur.HitPlayer(offPathClip, speaker);
            fadingCam = true;
        }
    }

    void Retry() {
        Application.LoadLevel(Application.loadedLevel);
    }
}
