using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

public class TouchTestDialogueActions : DialogueActions {
    bool waitingForGaze = false;
    Collider col;
    public override List<DialogueDelegate> getDialogueMethods()
    {
        return new List<DialogueDelegate>() { ForceLook };
    }

    public override PreConversationDelegate GetPreConversationDelegate()
    {
        return MoveGirls;
    }

    void MoveGirls() {
        TouchTest.MoveGirls();
    }

    void Start() {
        GetComponent<VRInteractiveItem>().OnOver += OnOver;
        col = GetComponent<Collider>();
        col.enabled = false;
    }

    void OnOver() {
        waitingForGaze = false;
        Destroy(col);
        GameObject.Find("TouchTest2").GetComponent<Conversation>().StartConversation();
    }

    void ForceLook(int choice) {
        waitingForGaze = true;
        col.enabled = true;
        GameManager.PlayerCanMove(false);
    }

}
