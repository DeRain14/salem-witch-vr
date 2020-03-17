using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sequence for jailer dialogue in scene 2.
/// Derian Green
/// </summary>
public class JailerDialogue : DialogueActions
{
    public override List<DialogueDelegate> getDialogueMethods()
    {
        return new List<DialogueDelegate>() {
            ChangeCousinResponse, NextScene
        };
    }

    public void ChangeCousinResponse(int choice)
    {
        if (choice != 0)
        {
            GetComponent<Conversation>().changeAudioClip("PayForBlanket");
        }
    }

    public void NextScene(int choice) {
        GameManager.AdvanceScene(1);
    }

    public override PreConversationDelegate GetPreConversationDelegate()
    {
        return SetInitialPose;
    }

    public void SetInitialPose()
    {
        GetComponent<Animator>().Play("HumanoidCrouchIdle");
    }
}