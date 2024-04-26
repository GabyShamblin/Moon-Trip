using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ThrottleInteract : XRGrabInteractable
{
    public float maxGrabDistance = 0.25f;
    private IXRSelectInteractor cachedInteractor;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        cachedInteractor = args.interactorObject;
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        cachedInteractor = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (cachedInteractor != null) {
            if (Vector3.Distance(cachedInteractor.transform.position, colliders[0].transform.position) > maxGrabDistance) {
                interactionManager.SelectExit(cachedInteractor, this);
            }
        }
    }
}
