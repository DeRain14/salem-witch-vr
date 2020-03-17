using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnocPlea1DialogueActions : DialogueActions {
    public override List<DialogueDelegate> getDialogueMethods()
    {
        return new List<DialogueDelegate>()
        {
            MoveWitnesses, null, null, null, null, null,
            null, FadeOut, null, DoEnd
        };
    }

    public override PreConversationDelegate GetPreConversationDelegate()
    {
        return StartChoking;
    }

    void StartChoking() {
        TouchTest.StartTrembling();
        Invoke("StopChoking", 3);
    }

    void StopChoking() {
        TouchTest.StopTrembling();
    }

    void MoveWitnesses(int choice) {
        GameObject.Find("JohnCook").GetComponent<MoveBetweenPoints>().MoveToGoal();
        GameObject.Find("ThomasPutnam").GetComponent<MoveBetweenPoints>().MoveToGoal();
        GameObject.Find("Abraham").GetComponent<MoveBetweenPoints>().MoveToGoal();
    }

    void FadeOut(int choice) {
        GameManager.DoCameraFade(null);
    }

    void DoEnd(int choice) {
        GameManager.DoEnd("You died");
    }
}
