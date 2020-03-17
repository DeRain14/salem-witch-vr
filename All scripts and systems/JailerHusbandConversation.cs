using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JailerHusbandConversation : DialogueActions {
    public override List<DialogueDelegate> getDialogueMethods()
    {
        return new List<DialogueDelegate>() { null, After2 };
           
    }

    public override PreConversationDelegate GetPreConversationDelegate()
    {
        return null;
    }

    void After2(int choice) {
        AudioSource src = GameObject.Find("DoorSound").GetComponent<AudioSource>();
        src.Play();
        GameManager.AdvanceScene((int)(src.clip.length+1));
    }
}
