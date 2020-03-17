using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour {

    delegate void DoCheat();
    struct CheatCode {
        public KeyCode key;
        public DoCheat method;
    }

    static CheatCode[] cheatCodes = {
        new CheatCode{key = KeyCode.T, method = SkipTouchTest},
        new CheatCode{key = KeyCode.N, method = NextLevel}
    };

	// Update is called once per frame
	void Update () {
        foreach (CheatCode code in cheatCodes) {
            if (Input.GetKeyUp(code.key)) {
                code.method();
            }
        }
	}

    static void SkipTouchTest() {
        foreach (TremblingGirl girl in FindObjectsOfType<TremblingGirl>()) {
            girl.StopTrembling();
        }
    }

    static void NextLevel() {
        GameManager.AdvanceScene(0);
    }
}
