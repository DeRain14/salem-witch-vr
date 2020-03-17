using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sequence for nitial dialogue for cousin character in jail.
/// Ryan Quistorff
/// </summary>
public class CousinJailDialogue : DialogueActions {
    public override List<DialogueDelegate> getDialogueMethods()
    {
        return new List<DialogueDelegate>() {
            After1, null, null, After4, After5
        };
    }

    public override PreConversationDelegate GetPreConversationDelegate()
    {
        return PreConversation;
    }

    void PreConversation() {

    }

    void After1(int choice) {
        if (choice != 0) {
            GetComponent<Conversation>().changeAudioClip("char2b");
        }
    }
    void After4(int choice)
    {
        if (choice != 0)
        {
            GetComponent<Conversation>().changeAudioClip("char5b");
            PlayerPrefs.SetInt("friend", 0);
        } else {
            PlayerPrefs.SetInt("friend", 1);
        }
    }
    void After5(int choice) {
        GameObject.Find("Jailer").GetComponent<Conversation>().Invoke("StartConversation", 15);
    }
}
