using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JailerConversation1 : DialogueActions {
    private bool finished;
    public override List<DialogueDelegate> getDialogueMethods()
    {
        return new List<DialogueDelegate>()
        {
            TurnOffLight
        };
    }

    public override PreConversationDelegate GetPreConversationDelegate()
    {
        return null;
    }

    void TurnOffLight(int choice) {
        FindObjectOfType<Light>().transform.parent.gameObject.SetActive(false);
        RenderSettings.ambientLight = new Color(0, 0, 0);
        GameObject.Find("Husband").GetComponent<Conversation>().Invoke("StartConversation", 15);
        PlayOnSchedule.allowed = false;
        finished = true;
    }

    void Update()
    {
        if (finished && PlayOnSchedule.allowed) {
            PlayOnSchedule.allowed = false;
        }
    }
}
