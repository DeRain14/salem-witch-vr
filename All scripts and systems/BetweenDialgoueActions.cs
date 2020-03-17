using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Methods to be implemented for each conversation
/// that are called between player choices. 
/// Ryan Quistorff
/// </summary>
public abstract class DialogueActions : MonoBehaviour{
    public delegate void DialogueDelegate(int chosen);
    public delegate void PreConversationDelegate();
    private List<DialogueDelegate> callbacks;
    public abstract List<DialogueDelegate> getDialogueMethods();
    public abstract PreConversationDelegate GetPreConversationDelegate();
}
