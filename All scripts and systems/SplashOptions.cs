using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplashOptions : MonoBehaviour {

    private static IntroSequence[] intros;
    private static int i;

    public void StartIntro() {
        intros = GetComponentsInChildren<IntroSequence>();
        foreach (Canvas canvas in GetComponentsInChildren<Canvas>())
            canvas.enabled = false;
        i = 0;
        GetComponent<Button>().interactable = false;
        DisplayIntro();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        GameManager.AdvanceScene(0);
    }

    public void DisplayIntro()
    {
        if (i == 0)
        {
            intros[0].InitializeIntro();
            i++;
        }
        else if (i < intros.Length)
        {
            intros[i - 1].EndIntro();
            intros[i].InitializeIntro();
            i++;
        }
        else
        {
            foreach (Button b in FindObjectsOfTypeAll(typeof(Button)))
                b.interactable = !b.interactable;
        }
    }
}
