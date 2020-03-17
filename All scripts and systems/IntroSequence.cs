using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

[RequireComponent(typeof(IntroSequence))]
public class IntroSequence : MonoBehaviour {

    public List<string> text;
    public TextMeshProUGUI textMesh;
    public Canvas canvas;
    private float textSpeed = .05f;
    public bool finished = false;

    public void InitializeIntro()
    {
        canvas.GetComponent<Canvas>().enabled = true;
        StartCoroutine(TypeSentences());
    }

    IEnumerator TypeSentences()
    {
        foreach (string sentence in text) {
            textMesh.text = "";
            for (int i = 0; i < sentence.Length; i++)
            {
                textMesh.text += sentence[i];
                if (i == sentence.Length - 1)
                    yield return new WaitForSeconds(textSpeed * 4); //pause longer at end of sentence
                yield return new WaitForSeconds(textSpeed);
            }
        }
        finished = true;
        GetComponentInChildren<Button>().interactable = true;
    }

    public void EndIntro()
    {
        canvas.GetComponent<Canvas>().enabled = false;
    }

}
