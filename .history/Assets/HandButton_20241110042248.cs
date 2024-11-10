using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class HandButton : MonoBehaviour
{
    public Button startButton; // Reference to the Start button
    public XRDirectInteractor leftHandInteractor; // Reference to the left hand interactor
    public XRDirectInteractor rightHandInteractor; // Reference to the right hand interactor

    private bool isLeftHandInteracting = false;
    private bool isRightHandInteracting = false;

    void Start()
    {
        // Subscribe to interaction events
        leftHandInteractor.selectEntered.AddListener(OnHandInteractStart);
        leftHandInteractor.selectExited.AddListener(OnHandInteractEnd);
        rightHandInteractor.selectEntered.AddListener(OnHandInteractStart);
        rightHandInteractor.selectExited.AddListener(OnHandInteractEnd);
    }

    void OnDestroy()
    {
        // Unsubscribe from interaction events to prevent memory leaks
        leftHandInteractor.selectEntered.RemoveListener(OnHandInteractStart);
        leftHandInteractor.selectExited.RemoveListener(OnHandInteractEnd);
        rightHandInteractor.selectEntered.RemoveListener(OnHandInteractStart);
        rightHandInteractor.selectExited.RemoveListener(OnHandInteractEnd);
    }

    void Update()
    {
        if (isLeftHandInteracting || isRightHandInteracting)
        {
            // Simulate button click
            startButton.onClick.Invoke();
        }
    }

    private void OnHandInteractStart(SelectEnterEventArgs args)
    {
        if (args.interactorObject == leftHandInteractor)
        {
            isLeftHandInteracting = true;
        }
        else if (args.interactorObject == rightHandInteractor)
        {
            isRightHandInteracting = true;
        }
    }

    private void OnHandInteractEnd(SelectExitEventArgs args)
    {
        if (args.interactorObject == leftHandInteractor)
        {
            isLeftHandInteracting = false;
        }
        else if (args.interactorObject == rightHandInteractor)
        {
            isRightHandInteracting = false;
        }
    }
}
