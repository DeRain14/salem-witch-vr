using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

public class TouchTestDialogueActions2 : DialogueActions {
    public override List<DialogueDelegate> getDialogueMethods()
    {
        return new List<DialogueDelegate>()
        {
            null, null, StartTouchTest
        };
    }

    public override PreConversationDelegate GetPreConversationDelegate()
    {
        return StartTrembling;
    }

    void StartTrembling() {
        TouchTest.StartTrembling();
    }

    void StartTouchTest(int choice) {

    }

}
