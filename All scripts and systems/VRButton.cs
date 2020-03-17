using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRStandardAssets.Utils;
/// <summary>
/// Makes a button highlighted and clicked 
/// through VR gaze
/// Ryan Quistorff
/// </summary>
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(VRInteractiveItem))]
[RequireComponent(typeof(Button))]
public class VRButton : MonoBehaviour {
    Button button;
    Color normalTint = new Color(0.5f, 0.5f, 0.5f);
    Color highlightTint = new Color(1, 1, 1);
    private VRInteractiveItem interactiveItem;

	// Use this for initialization
	void Start () {
        button = GetComponent<Button>();
        button.image.color = normalTint;
        interactiveItem = GetComponent<VRInteractiveItem>();
        interactiveItem.OnOver += HandleOver;
        interactiveItem.OnOut += HandleOut;
        interactiveItem.OnClick += HandleClick;
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        Rect btnDim = GetComponent<RectTransform>().rect;
        boxCollider.size = new Vector3(btnDim.width, btnDim.height, 1);
    }

    public void HandleClick()
    {
        button.onClick.Invoke();
    }

    public void HandleOver() {
        button.image.color = highlightTint;
    }

    public void HandleOut() {
        button.image.color = normalTint;
    }

}
