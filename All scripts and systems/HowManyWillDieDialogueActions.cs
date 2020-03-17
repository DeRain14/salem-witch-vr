using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowManyWillDieDialogueActions : DialogueActions {
    public override List<DialogueDelegate> getDialogueMethods()
    {
        return new List<DialogueDelegate>()
        {
            null, null, null, null, null, MoveSurgeon, null, null,
            Fade, null, DoEnd
        };
    }

    public override PreConversationDelegate GetPreConversationDelegate()
    {
        return null;
    }

    void MoveSurgeon(int choice)
    {
        GameManager.DoCameraFade(ActivateSurgeon);
    }

    void ActivateSurgeon() {
        FindObjectOfType<Surgeon>().Show();
    }

    void Fade(int choice) {
        GameManager.DoCameraFade(null);
    }

    void DoEnd(int choice) {
        GameManager.DoEnd("You and your cousin both died");
    }


}
