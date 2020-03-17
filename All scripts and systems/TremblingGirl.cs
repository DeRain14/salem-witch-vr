using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TremblingGirl : MonoBehaviour {
    AudioSource audio;
    Animator anim;
    MoveBetweenPoints movement;
    bool completed = false;

    void Start() {
        audio = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        movement = GetComponent<MoveBetweenPoints>();
    }

    public void StartTrembling() {
        bool completed = false;
        anim.SetBool("trembling", true);
        if (audio != null) {
            audio.Play();
        }
    }

    public void StopTrembling() {
        anim.SetBool("trembling", false);
        if (!completed)
        {
            TouchTest.CompletedTest();
        }
        completed = true;
    }

    public void StartWalk() {
        movement.MoveToGoal();
    }
}
