using UnityEngine;
using Vuforia;

public class ResetOnTargetFound : MonoBehaviour
{
    [SerializeField] private GameObject modelToReset;

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Vector3 initialScale;

    private void Start()
    {
        if (modelToReset != null)
        {
            initialPosition = modelToReset.transform.localPosition;
            initialRotation = modelToReset.transform.localRotation;
            initialScale = modelToReset.transform.localScale;
        }

        // Subscribe to the status changed event
        var observer = GetComponent<ObserverBehaviour>();
        if (observer != null)
        {
            observer.OnTargetStatusChanged += OnTargetStatusChanged;
        }
    }

    private void OnDestroy()
    {
        var observer = GetComponent<ObserverBehaviour>();
        if (observer != null)
        {
            observer.OnTargetStatusChanged -= OnTargetStatusChanged;
        }
    }

    private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus status)
    {
        if (status.Status == Status.TRACKED || status.Status == Status.EXTENDED_TRACKED)
        {
            ResetModel();
        }
    }

    private void ResetModel()
    {
        if (modelToReset != null)
        {
            modelToReset.transform.localPosition = initialPosition;
            modelToReset.transform.localRotation = initialRotation;
            modelToReset.transform.localScale = initialScale;
        }
    }
}