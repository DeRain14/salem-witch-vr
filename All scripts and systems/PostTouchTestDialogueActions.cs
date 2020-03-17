using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostTouchTestDialogueActions : DialogueActions {
    public override List<DialogueDelegate> getDialogueMethods()
    {
        return new List<DialogueDelegate>() {
            SetPath
        };
    }

    public override PreConversationDelegate GetPreConversationDelegate()
    {
        return null;
    }

    void SetPath(int choice) {
        bool friendship = PlayerPrefs.GetInt("friend") == 1;
        if (choice == 0) {
            if (friendship) {
                GameObject.Find("InnocPlea1").GetComponent<Conversation>().StartConversation();
            } else {
                GameObject.Find("InnocPlea2").GetComponent<Conversation>().StartConversation();
            }

        } else {
            if (friendship) {
                GameObject.Find("InnocElab").GetComponent<Conversation>().StartConversation();
            } else {
                GameObject.Find("GuiltyNegative").GetComponent<Conversation>().StartConversation();
            }
        }
    }
}
