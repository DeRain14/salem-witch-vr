using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnocPlea2DialogueActions : DialogueActions {
    public override List<DialogueDelegate> getDialogueMethods()
    {
        return new List<DialogueDelegate>()
        {
            MoveWitnesses, null, null, null, null, null, null,
            MoveCousin, null, null, DeterminePath
        };
    }
    public override PreConversationDelegate GetPreConversationDelegate()
    {
        return StartChoking;
    }

    void StartChoking()
    {
        TouchTest.StartTrembling();
        Invoke("StopChoking", 3);
    }

    void StopChoking()
    {
        TouchTest.StopTrembling();
    }
    void MoveCousin(int choice) {
        GameObject.Find("Cousin").GetComponent<MoveBetweenPoints>().MoveToGoal();
    }

    void MoveWitnesses(int choice)
    {
        GameObject.Find("JohnCook").GetComponent<MoveBetweenPoints>().MoveToGoal();
        GameObject.Find("ThomasPutnam").GetComponent<MoveBetweenPoints>().MoveToGoal();
        GameObject.Find("Abraham").GetComponent<MoveBetweenPoints>().MoveToGoal();
    }

    void DeterminePath(int choice) {
        if (choice == 0) {
            GameObject.Find("HowManyWillDie").GetComponent<Conversation>().StartConversation();
        } else {
            GameObject.Find("Innoc2End").GetComponent<Conversation>().StartConversation();
        }
    }

}
