using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HandTrackingButton : MonoBehaviour
{
    public XRGrabInteractable grabInteractable;

    void OnEnable()
    {
        grabInteractable.selectEntered.AddListener(OnButtonPressed);
    }

    void OnDisable()
    {
        grabInteractable.selectEntered.RemoveListener(OnButtonPressed);
    }

    void OnButtonPressed(SelectEnterEventArgs args)
    {
        // ボタンが押されたときの処理
        Debug.Log("Button Pressed");
    }
}