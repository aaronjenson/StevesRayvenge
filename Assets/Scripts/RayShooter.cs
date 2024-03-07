using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject rayPrefab;
    public GameObject rightControllerGameObject;
    private InputDevice rightController;
    private bool rayGenerated;

    void Start()
    {
        rayGenerated = false;
        List<InputDevice> devices = new List<InputDevice>();
        InputDeviceCharacteristics rightControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, devices);

        if (devices.Count > 0) {
            rightController = devices[0];
        }
    }

    // Update is called once per frame
    void Update()
    {
        rightController.TryGetFeatureValue(CommonUsages.trigger, out float rightTriggerValue);
        if (rightTriggerValue > 0.5f) {
            if (!rayGenerated) {
                Instantiate(rayPrefab, rightControllerGameObject.transform.position, rightControllerGameObject.transform.rotation * Quaternion.Euler(90, 0, 0));
                rayGenerated = true;
            }
        } else {
            rayGenerated = false;
        }
    }
}
