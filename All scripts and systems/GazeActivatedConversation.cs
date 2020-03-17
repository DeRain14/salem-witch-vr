using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;
using VRStandardAssets.Utils;

/// <summary>
/// Extension of conversation class
/// that starts a conversation after the 
/// object is looked at rather than by a trigger
/// Ryan Quistorff
/// </summary>
[RequireComponent(typeof(VRInteractiveItem))]
public class GazeActivatedConversation : Conversation {
    public int gazeSecondsToStartConversation;
    private VRInteractiveItem interactiveItem;
    private bool conversationStarted = false;
    private bool gazing = false;
    private float gazeTime = 0;
    // Use this for initialization
    public override void Start () {
        base.Start();
        interactiveItem = GetComponent<VRInteractiveItem>();
        interactiveItem.OnOver += HandleOver;
        interactiveItem.OnOut += HandleOut;
    }

    // Update is called once per frame
    void Update() {
        if (conversationStarted) {
            return;
        }
        if (gazing) {
            gazeTime += Time.deltaTime;
        } if (gazeTime >= gazeSecondsToStartConversation) {
            conversationStarted = true;
            StartConversation();
        }
	}

    public void HandleOver() {
        gazing = true;
        ShowHighlight(true, gameObject, 1);
    }

    public void HandleOut() {
        gazeTime = 0;
        gazing = false;
        ShowHighlight(false, gameObject, 0);
    }

}
