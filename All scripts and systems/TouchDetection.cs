using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDetection : MonoBehaviour {

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("triggered");
        TremblingGirl girl = col.gameObject.GetComponent<TremblingGirl>();
        if (girl != null) {
            Debug.Log("found girl");
            girl.StopTrembling();
        } else
        {
            GameObject colObj = col.gameObject;
            foreach(Component c in colObj.GetComponents<Component>()) {
                Debug.Log(c.name);
            }
            Debug.Log("found " + col.name);
        }
    }
}
