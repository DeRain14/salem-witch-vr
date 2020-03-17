using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialPleaDetermineFriendshipBranch : DialogueActions {
    public override List<DialogueDelegate> getDialogueMethods()
    {
        return new List<DialogueDelegate>() {
            AfterPlea, AfterInnocentDialogue
        };
    }

    public override PreConversationDelegate GetPreConversationDelegate()
    {
        return StopApproachTheBar;
    }

    void StopApproachTheBar() {
        MovementChecker.finished = true;
    }

    void AfterPlea(int choice) {
        //Destroy obstacles that forced player into correct position so NPCs can move freely
        Destroy(GameObject.Find("InitialObstacles"));
        bool friendship = PlayerPrefs.GetInt("friend") == 1;
        GameObject nextConversation;
        //guilty
        if (choice == 0) {
            if (friendship) {
                nextConversation = GameObject.Find("GuiltyPositive");
            } else {
                nextConversation = GameObject.Find("GuiltyNegative");
            }
            GetComponent<Conversation>().Reset();
            nextConversation.GetComponent<Conversation>().StartConversation();
        } 
    }

    void AfterInnocentDialogue(int choice) {
        GameObject.Find("TouchTestDialogue").GetComponent<Conversation>().StartConversation();
        Destroy(this.gameObject);
    }
}
