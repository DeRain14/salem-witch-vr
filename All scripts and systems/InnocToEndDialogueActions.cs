using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnocToEndDialogueActions : DialogueActions {
    public override List<DialogueDelegate> getDialogueMethods()
    {
        return new List<DialogueDelegate>()
        {
            null, Fade, null, DoEnd
        };
    }

    public override PreConversationDelegate GetPreConversationDelegate()
    {
        return null;
    }

    void Fade(int choice) {
        GameManager.DoCameraFade(null);
    }

    void DoEnd(int choice) {
        GameManager.DoEnd("You died");
    }

}
