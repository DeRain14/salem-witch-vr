using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

/// <summary>
/// Method to allow the camera to blur in and out and vibrate 
/// the touch controllers, simulating
/// the player being hit in the head
/// Ryan Quistorff
/// </summary>
public class HitBlur : MonoBehaviour {
    private static BlurOptimized blur;
    private static bool blurring;
    private const float MAX_SIZE = 6;
    private static bool increasing = true;
    private static int curIteration = 1;
    [Range(1,5)]
    public int iterations;
    public AudioClip vibration;
    private static AudioClip clip;
    private static HitBlur instance;
	// Use this for initialization
	void Start () {
        blur = GetComponentInChildren<BlurOptimized>();
        blur.enabled = false;
        clip = vibration;
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		if (blurring)
        {
            if (increasing)
            {
                blur.blurSize += MAX_SIZE / curIteration * Time.deltaTime * 3;
                if (blur.blurSize >= MAX_SIZE/curIteration)
                {
                    increasing = false;
                }
            } else
            {
                blur.blurSize -= MAX_SIZE / curIteration * Time.deltaTime;
                if (blur.blurSize <= 0)
                {
                    increasing = true;
                    if (curIteration >= iterations)
                    {
                        curIteration = 1;
                        blur.blurSize = 0;
                        blurring = false;
                        blur.enabled = false;

                    } else
                    {
                        curIteration++;
                    }
                }
            }
        }
	}

    public static void HitPlayer(AudioClip clip, AudioSource source)
    {
        blur.enabled = true;
        blur.blurSize = 0;
        blurring = true;
        increasing = true;
        if (GameManager.InVR && OVRInput.GetActiveController() != OVRInput.Controller.Touch) {
            OVRHaptics.LeftChannel.Preempt(new OVRHapticsClip(clip, 0));
            OVRHaptics.RightChannel.Preempt(new OVRHapticsClip(clip, 0));
            instance.Invoke("ClearVibration", clip.length);
        }
        if (source != null) {
            source.clip = clip;
            source.Play();
        }
    }

    void clearVibration() {
        OVRHaptics.LeftChannel.Clear();
        OVRHaptics.RightChannel.Clear();
    }
}
