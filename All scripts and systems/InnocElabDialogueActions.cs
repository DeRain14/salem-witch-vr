using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnocElabDialogueActions : DialogueActions {
    public override List<DialogueDelegate> getDialogueMethods()
    {
        return new List<DialogueDelegate>() {
            null, null, null, ChangePath, null, null, null, null, null, null, DoEnd
        };
    }

    public override PreConversationDelegate GetPreConversationDelegate()
    {
        return null;
    }

    void ChangePath(int choice)
    {
        if (choice == 1)
        {
            GameObject.Find("TouchTestDialogue").GetComponent<Conversation>().StartConversation();
            Destroy(this.gameObject);
        }
    }

    void DoEnd(int choice) {
        GameManager.DoEnd("You saved yourself and threw Bridget Bishop under the bus");
    }
}
