using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

/// <summary>
/// Allows for simple non-branching dialogue. Triggered by the required collider component. 
/// Create a method to be run after the player makes a choice in each step of the dialogue 
/// as the BetweenDialogueActions component.If nothing needs to be done between dialogue, 
/// set that index to null (because the methods and conversation length must be 1:1). 
/// If you have multiple audio clips corresponding to the choice the player made, 
/// you can call changeAudioClip in that index's method to adjust it accordingly. 
/// To somewhat accomplish branching dialogue, you can make your last method call 
/// StartSpeaker for another Conversation component representing a branch.
/// Ryan Quistorff
/// </summary>
public class Conversation : MonoBehaviour, Dialogue {

    public DialogueActions methodsBetweenConversation;
    public List<Dialogue> conversation;
    public bool allowPlayerMovement;
    List<DialogueActions.DialogueDelegate> methods;
    DialogueActions.PreConversationDelegate preConversation;
    int currentDialogue = 0;
    bool stopped = false;

    // Use this for initialization
    public virtual void Start () {
        if (gameObject.GetComponent<DialogueActions>() == null) {
            methods = new List<DialogueActions.DialogueDelegate>();
            for (int i = 0; i < conversation.Count; i++)
            {
                methods.Add(null);
            }
        }
        else if ((methods = GetComponent<DialogueActions>().getDialogueMethods()).Count != conversation.Count)
        {
            throw new System.Exception("Conversation length and number of methods between choices differ");
        }
        else {
            preConversation = GetComponent<DialogueActions>().GetPreConversationDelegate();
        }
        if (conversation[conversation.Count - 1].choices.Count == 0) {
            conversation[conversation.Count - 1].choices.Add("Done");
        }

	}
	
    //start audio when triggered
    public virtual void OnTriggerEnter(Collider col) {
        if (col.gameObject.GetComponent<OVRPlayerController>())
        {
            Destroy(GetComponent<Collider>());
            StartConversation();
        }
    }

    public void StartConversation() {
        currentDialogue = 0;
        stopped = false;
        if (!allowPlayerMovement) {
            GameManager.PlayerCanMove(false);
        }
        PlayOnSchedule.allowed = false;
        StartSpeaker();
        ShowHighlight(true, conversation[currentDialogue].speaker.gameObject, 0);
    }

    //play current audio source
    public void StartSpeaker() {
        if (stopped) {
            return;
        }
        if (currentDialogue == 0)
        {
            if (preConversation != null) {
                preConversation();
            }
        } else if (!conversation[currentDialogue].speaker.Equals(conversation[currentDialogue-1].speaker)) {
            ShowHighlight(false, conversation[currentDialogue - 1].speaker.gameObject, 0);
            ShowHighlight(false, conversation[currentDialogue].speaker.gameObject, 0);

        }
        conversation[currentDialogue].speaker.clip = conversation[currentDialogue].dialogue;
        conversation[currentDialogue].speaker.Play();
        Invoke("DisplayChoices", conversation[currentDialogue].speaker.clip.length);

    }

    //After dialogue has finished playing, show the player their choices
    void DisplayChoices() {
        if (currentDialogue < conversation.Count)
        {
            List<string> choices = new List<string>(conversation[currentDialogue].choices);
            if (choices.Count == 0)
            {
                choices.Add("Continue");
            }
            choices.Add("Pardon?");
            DialogueChoices.ActivateDialogueOptions(choices, this);
        } else {
            //conversation is finished
            if (methods[currentDialogue] != null) {
                methods[currentDialogue](0);
            }
            GameManager.PlayerCanMove(true);
            PlayOnSchedule.allowed = true;
          
            foreach (Renderer r in GetComponentsInChildren<Renderer>())
            {
                Destroy(r.gameObject.GetComponent<Outline>());
            }
            Destroy(this);
        }
    }

    //perform any actions resulting from the player's choice, then start playing the next audio clip
    public void advanceDialogue(int value)
    {
        int numChoices = conversation[currentDialogue].choices.Count;
        //if no choices, choose between 'continue' and 'done'
        if (numChoices == 0) {
            if (value == 0) {
                if (methods[currentDialogue] != null)
                {
                    methods[currentDialogue](value);
                }
                currentDialogue++;
            }
            StartSpeaker();
            return;
        }
        //replay
        if (value == conversation[currentDialogue].choices.Count) {
            StartSpeaker();
            return;
        } else if (currentDialogue == conversation.Count - 1) {
            //conversation is finished
            if (methods[currentDialogue] != null)
            {
                methods[currentDialogue](value);
            }
            GameManager.PlayerCanMove(true);
            PlayOnSchedule.allowed = true;

            ShowHighlight(false, conversation[currentDialogue].speaker.gameObject, 0);
            Destroy(this);
            return;
        }
        if (methods[currentDialogue] != null) {
            methods[currentDialogue](value);
        }
        currentDialogue++;
        StartSpeaker();
    }

    public void ShowHighlight(bool wantHighlight, GameObject speaker, int color) {
        foreach (Renderer r in speaker.GetComponentsInChildren<Renderer>())
        {
            if (!wantHighlight) {
                Destroy(r.gameObject.GetComponent<Outline>());
            } else {
                Outline o = r.gameObject.AddComponent<Outline>();
                o.color = color;
            }

        }
    }

    //support for changing what the NPC says next in response to player's choice
    public void changeAudioClip(string newClipName) {
        if (currentDialogue >= conversation.Count - 1) {
            return;
        }
        conversation[currentDialogue + 1] = new Dialogue
        {
            dialogue = Resources.Load("Audio/" + newClipName) as AudioClip,
            choices = conversation[currentDialogue + 1].choices,
            speaker = conversation[currentDialogue + 1].speaker
        };
    }

    public void Reset()
    {
        currentDialogue = 0;
        stopped = true;
    }

    [System.Serializable] public struct Dialogue {
        public AudioClip dialogue;
        public List<string> choices;
        public AudioSource speaker;
    }
}
