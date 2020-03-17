using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchTest : MonoBehaviour {

    private static TremblingGirl[] girls;
    private static int numTouched;

    public static void MoveGirls() {
        girls = FindObjectsOfType<TremblingGirl>();
        foreach (TremblingGirl girl in girls)
        {
            girl.StartWalk();
        }
    }

    public static void StartTrembling() {
        numTouched = 0;
        foreach (TremblingGirl girl in girls)
        {
            girl.StartTrembling();
        }
    }

    public static void StopTrembling() {
        foreach (TremblingGirl girl in girls)
        {
            girl.StopTrembling();
        }
    }

    public static void CompletedTest() {
        numTouched++;
        if (numTouched == girls.Length) {
            GameObject.Find("PostTouchTest").GetComponent<Conversation>().StartConversation();
        }
    }
}
