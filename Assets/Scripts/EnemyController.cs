using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    CameraRaycaster cameraRaycaster;

    // Use this for initialization
    void Start () {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        cameraRaycaster.notifyMouseClickObservers += OnClick;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnClick(RaycastHit raycastHit, int layerHit) {
        Debug.Log("I AM CLICKED");
        
    }
}
