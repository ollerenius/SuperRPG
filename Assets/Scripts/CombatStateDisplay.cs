using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This script is used only for testing combat states.
// TODO: Remove this script once testing is done
public class CombatStateDisplay : MonoBehaviour {

    Text text;
    CameraRaycaster cameraRaycaster;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();

        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        cameraRaycaster.notifyLayerChangeObservers += OnLayerChange;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnLayerChange(int newLayer) {
        text.text = newLayer.ToString();
    }


}
