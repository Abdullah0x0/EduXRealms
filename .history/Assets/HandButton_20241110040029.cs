using UnityEngine;
using UnityEngine.UI;
using OculusSampleFramework;

public class HandButton : MonoBehaviour
{
    public Button startButton;
    private OVRHand leftHand;
    private OVRHand rightHand;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        leftHand = GameObject.Find("LeftHandAnchor").GetComponent<OVRHnd>();
        rightHand = GameObject.Find("RightHandAnchor").GetComponent<OVRHnd>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
