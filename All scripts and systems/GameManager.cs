using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Regulates player movement
/// Ryan Quistorff
/// </summary>
public class GameManager : MonoBehaviour {

    private static float initialPlayerAccel;
    private static OVRPlayerController player;
    private static OVRCameraRig cam;
    private static GameManager instance;
    public delegate void OnFadeOut();
    public static bool InVR { get; private set; }
    private static bool canFade;
    public static string endResult { get; private set; }

	// Use this for initialization
	void Awake () {
        player = FindObjectOfType<OVRPlayerController>();
        cam = player.GetComponentInChildren<OVRCameraRig>();
        InVR = OVRManager.isHmdPresent;
        initialPlayerAccel = player.Acceleration;
        instance = this;
        time = -1;
        try
        {
            overlay = GameObject.Find("Fade").GetComponent<Image>();
            overlay.gameObject.SetActive(false);
            canFade = true;
        } catch (Exception e)
        {
            canFade = false;
        }
        
    }

    internal static void AdvanceScene(float length)
    {
        throw new NotImplementedException();
    }

    public static void PlayerCanMove(bool canMove) {
        if (canMove) {
            player.Acceleration = initialPlayerAccel;
           // player.restrictAngle(null, null);
        } else {
            player.Acceleration = 0;
           // player.restrictAngle(-30, 30);
        }
    }

    public static Transform getPlayerTransform() {
        return player.transform;
    }

    public static float fixAngle(float value) {
        while(value > 180 || value < -180) {
            if (value > 180)
            {
                return value - 360;
            }
            else if (value < -180)
            {
                return value + 360;
            }
        }
        return value;
    }

    public static void AdvanceScene(int seconds) {
        instance.Invoke("NextLevel", seconds);
    }


    public static void DoEnd(String result) {
        endResult = result;
        instance.Invoke("NextLevel", 2);
        DoCameraFade(null);
    }

    private void NextLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private static Image overlay;
    private static float time;
    private static bool performedActions;
    private static OnFadeOut actions;
    public static void DoCameraFade(OnFadeOut methodDuringFadeOut) {
        if (!canFade)
        {
            Debug.Log("Couldn't find fade canvas");
            return;
        }
        overlay.gameObject.SetActive(true);
        time = 0;
        actions = methodDuringFadeOut;
        performedActions = false;
    }

    private void Update()
    {
        if (time >= 0) {
            time += Time.deltaTime;
            if (time >= 3)
            {
                if (!performedActions) {
                    if (actions != null) {
                        actions();
                    }
                    performedActions = true;
                }
                overlay.color = new Color(0, 0, 0, 4 - time);
                if (time >= 4)
                {
                    time = -1;
                    overlay.color = new Color(0, 0, 0, 0);
                    overlay.gameObject.SetActive(false);
                }
            }
            else if (time < 1)
            {
                overlay.color = new Color(0, 0, 0, time);
            }
        }

    }
}
