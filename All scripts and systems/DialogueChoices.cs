using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles player's clicking for dialogue choices,
/// then calls the passed in Dialogue instance to handle 
/// the response. 
/// Ryan Quistorff
/// </summary>
public class DialogueChoices : MonoBehaviour {

    private static GameObject bookPanel;
    private static GameObject[] choices;
    private static Dialogue callback;
    private static List<GameObject> dialogueChoices;
    private static GameObject dialoguePanel;
    private static bool canClick;

    // Use this for initialization
    void Awake()
    {
        dialogueChoices = new List<GameObject>();
    }

    public static void ActivateDialogueOptions(List<string> choices, Dialogue callback)
    {
        DialogueChoices.callback = callback;
        dialoguePanel = Instantiate(Resources.Load("DialogueSelector") as GameObject).transform.GetChild(0).gameObject;
        GameObject player = FindObjectOfType<OVRPlayerController>().gameObject;
        GameManager.PlayerCanMove(false);
        dialoguePanel.transform.position = player.transform.position + player.transform.forward;
        dialoguePanel.transform.rotation = player.transform.rotation;//Quaternion.Euler(player.transform.eulerAngles + new Vector3(0, 180, 0));
        GameObject dialogueChoiceButton = Resources.Load("DialogueChoiceButton") as GameObject;
        canClick = true;
        for (int i = 0; i < choices.Count; i++)
        {
            GameObject option = Instantiate(dialogueChoiceButton, dialoguePanel.transform);
            option.GetComponentInChildren<Text>().text = choices[i];
            dialogueChoices.Add(option);
            int index = i;
            option.GetComponent<Button>().onClick.AddListener(delegate { handleDialogueClick(index); });
            option.transform.parent = dialoguePanel.transform;
            option.SetActive(true);
        }
    }

    public static void handleDialogueClick(int value)
    {
        if (canClick) {
            canClick = false;
            dialogueChoices = new List<GameObject>();
            Destroy(dialoguePanel.transform.parent.gameObject);
            dialoguePanel = null;
            callback.advanceDialogue(value);
        }
    }
}
