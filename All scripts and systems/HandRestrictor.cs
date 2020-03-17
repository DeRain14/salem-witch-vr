using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Limits the distance between touch controllers. 
/// After passing the time outside the valid range 
/// in the current index of penalties, 
/// the controllers vibrate and an audio clip is played. 
/// Ryan Quistorff
/// </summary>
public class HandRestrictor : MonoBehaviour {

    public float allowedDistance;
    public List<Penalty> penalties;
    Vector3 leftHand;
    Vector3 rightHand;
    float timeBreakingRule;
    int penaltyIndex = 0;
    bool active = false;

	// Use this for initialization
	void Start () {
        if (OVRInput.GetActiveController() != OVRInput.Controller.Touch)
            Debug.Log("Incompatible controller " + OVRInput.GetActiveController());
        
	}
	
	// Update is called once per frame
	void Update () {
        if (!active)
            return;
        leftHand = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
        rightHand = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
        if ((leftHand-rightHand).magnitude >= allowedDistance) {
            timeBreakingRule += Time.deltaTime;
            if (penalties[penaltyIndex].timeToStart <= timeBreakingRule) {
                penalties[penaltyIndex].speaker.clip = penalties[penaltyIndex].clipToPlay;
                penalties[penaltyIndex].speaker.Play();
                OVRHaptics.LeftChannel.Preempt(new OVRHapticsClip(penalties[penaltyIndex].handVibrationFrequency, 0));
                OVRHaptics.RightChannel.Preempt(new OVRHapticsClip(penalties[penaltyIndex].handVibrationFrequency, 0));
                // particle system?
                penaltyIndex++;
                if (penaltyIndex >= penalties.Count) {
                    Invoke("PlayerFailed", 5);
                }
            }
        } else {
            timeBreakingRule = 0;
            penaltyIndex = 0;
            OVRHaptics.LeftChannel.Clear();
            OVRHaptics.RightChannel.Clear();
        }
    }

    void PlayerFailed() {
        //Fade out, go to next room?
        OVRHaptics.LeftChannel.Clear();
        OVRHaptics.RightChannel.Clear();
        Destroy(this);
    }

    [System.Serializable]
    public struct Penalty{
        public int timeToStart;
        public AudioClip handVibrationFrequency;
        public AudioClip clipToPlay;
        public AudioSource speaker;
    }

    private void OnTriggerEnter(Collider other)
    {
        active = true;
    }
}
