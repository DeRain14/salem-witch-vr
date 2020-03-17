using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Play the provided audio clip repeatedly, waiting based on provided arguments. 
/// Allows only one PlayOnSchedule clip to play at a time
/// Ryan Quistorff
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class PlayOnSchedule : MonoBehaviour {

    [Range(5, 60)]
    [Tooltip("Average amount of time to wait between playing the clip")]
    public int waitTime;
    [Range(5, 60)]
    [Tooltip("Max deviation from the wait time, chosen randomly from 0 to this value")]
    public int variation;
    private AudioSource audioSrc;
    private static PlayOnSchedule playing;
    public static bool allowed;

	// Use this for initialization
	void Start () {
        allowed = true;
        audioSrc = GetComponent<AudioSource>();
        Invoke("PlayAudio", getWaitTime());
	}
	
    void PlayAudio() {
        if (playing != null) {
            Invoke("PlayAudio", 10);
        } else {
            playing = this;
            if (allowed) {
                audioSrc.Play();
            }
            Invoke("FreeClip", audioSrc.clip.length);
            Invoke("PlayAudio", getWaitTime());

        }
    }

    void FreeClip() {
        if (playing == this) {
            playing = null;
        }
    }

    private float getWaitTime() {
        return Mathf.Max(5, waitTime + audioSrc.clip.length + Random.Range(-1 * variation, variation));
    }
}
