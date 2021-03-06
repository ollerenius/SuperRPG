﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraRaycaster : MonoBehaviour {

    // INSPECTOR PROPERTIES RENDERED BY CUSTOM EDITOR SCRIPT
    [SerializeField] int[] layerPriorities;

    float maxRaycastDepth = 100f; // Hard coded value
    int topPriorityLayerLastFrame = -1;

    // Setup delegates for broadcasting layer changes to other classes
    public delegate void OnCursorLayerChange(int newLayer);
    public event OnCursorLayerChange notifyLayerChangeObservers;

    public delegate void OnClickPriorityLayer(RaycastHit raycastHit, int layerHit);
    public event OnClickPriorityLayer notifyMouseClickObservers;


    void Update() {
        // Check if pointer is over an interactable UI element
        if (EventSystem.current.IsPointerOverGameObject()) {
            NotifyObserersIfLayerChanged(5);
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] raycastHits = Physics.RaycastAll(ray, maxRaycastDepth);

        RaycastHit? priorityHit = FindTopPriorityHit(raycastHits);
        if (!priorityHit.HasValue) { // if hit no priority object
            NotifyObserersIfLayerChanged(0); // broadcast default layer
            return;
        }

        // Notify delegates of layer change
        var layerHit = priorityHit.Value.collider.gameObject.layer;
        NotifyObserersIfLayerChanged(layerHit);

        // Notify delegates of highest priority game object under mouse when clicked
        if (Input.GetMouseButton(0)) {
            notifyMouseClickObservers(priorityHit.Value, layerHit);
        }
    }

    void NotifyObserersIfLayerChanged(int newLayer) {
        if (newLayer != topPriorityLayerLastFrame) {
            topPriorityLayerLastFrame = newLayer;
            notifyLayerChangeObservers(newLayer);
        }
    }

    RaycastHit? FindTopPriorityHit(RaycastHit[] raycastHits) {
        // Form list of layer numbers hit
        List<int> layersOfHitColliders = new List<int>();
        foreach (RaycastHit hit in raycastHits) {
            layersOfHitColliders.Add(hit.collider.gameObject.layer);
        }

        // Step through layers in order of priority looking for a gameobject with that layer
        foreach (int layer in layerPriorities) {
            foreach (RaycastHit hit in raycastHits) {
                if (hit.collider.gameObject.layer == layer) {
                    return hit; // stop looking
                }
            }
        }
        return null; // because cannot use GameObject? nullable
    }
}
