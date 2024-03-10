using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Serialization;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject rayPrefab;
    public GameObject rightControllerGameObject;
    public List<GameObject> disableOnGameStart;
    public List<GameObject> enableOnGameStart;
    public bool rayShootingEnabled = true;
    private InputDevice rightController;
    private bool rayGenerated;
    private List<TeleportationArea> _teleportationAreas;
    public PlayableDirector cutScene;

    void Start()
    {
        rayGenerated = false;
        rayShootingEnabled = false;
        List<InputDevice> devices = new List<InputDevice>();
        InputDeviceCharacteristics rightControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, devices);

        if (devices.Count > 0) {
            rightController = devices[0];
        }

        _teleportationAreas = new List<TeleportationArea>();
        foreach(GameObject obj in FindObjectsByType<GameObject>(FindObjectsInactive.Include, FindObjectsSortMode.None))
        {
            TeleportationArea comp = obj.GetComponent<TeleportationArea>();
            if (comp != null)
            {
                _teleportationAreas.Add(comp);
                comp.enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        rightController.TryGetFeatureValue(CommonUsages.trigger, out float rightTriggerValue);
        if (rightTriggerValue > 0.5f && rayShootingEnabled) {
            if (!rayGenerated) {
                Instantiate(rayPrefab, rightControllerGameObject.transform.position, rightControllerGameObject.transform.rotation * Quaternion.Euler(90, 0, 0));
                rayGenerated = true;
            }
        } else {
            rayGenerated = false;
        }
    }

    public void GameStarted()
    {
        EnableRayShooting();
        _teleportationAreas.ForEach(area => area.enabled = true);
        enableOnGameStart.ForEach(obj => obj.SetActive(true));
        disableOnGameStart.ForEach(obj => obj.SetActive(false));
        cutScene.Play();
    }

    public void EnableRayShooting()
    {
        rayShootingEnabled = true;
    }

    public void DisableRayShooting()
    {
        rayShootingEnabled = false;
    }
}
