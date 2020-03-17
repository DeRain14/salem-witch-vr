using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiltyNegativeDialogueActions : DialogueActions {
    public override List<DialogueDelegate> getDialogueMethods()
    {
        return new List<DialogueDelegate>() {
        null, null, null, DeterminePath, null, null, FadeOut, null, null, null, DoEnd
        };
    }

    public override PreConversationDelegate GetPreConversationDelegate()
    {
        return null;
    }

    void DeterminePath(int choice) {
        if (choice == 1) {
            GameObject.Find("TouchTestDialogue").GetComponent<Conversation>().StartConversation();
            Destroy(this.gameObject);
        }
    }

    void FadeOut(int choice) {
        GameManager.DoCameraFade(PlaceSurgeon);
    }

    void PlaceSurgeon() {
        FindObjectOfType<Surgeon>().Show();
    }

    void DoEnd(int choice) {
        bool friendship = PlayerPrefs.GetInt("friend") == 1;
        string end = friendship ? 
            "You saved yourself and threw Bridget Bishop under the bus"
            : "You saved yourself and threw your cousin under the bus";
        GameManager.DoEnd(end);
    }
}
