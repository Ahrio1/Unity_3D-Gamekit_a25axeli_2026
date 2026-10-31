using UnityEngine;
using Cinemachine;

public class CameraReset : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook freeLookCamera;

    private void Start()
    {
        //If there is no freeLookCamera, get the component from the specified object
        if (freeLookCamera == null)
        {
            GameObject cameraObject = GameObject.Find("KeyboardAndMouseFreeLookRig");

            if (cameraObject != null)
            {
                freeLookCamera = cameraObject.GetComponent<CinemachineFreeLook>();
            }
        }
    }

    public void CameraResetStart()
    {
        if (freeLookCamera != null)
        {
            // Get the TransitionDestination rotation value
            float playerFacingAngle = transform.eulerAngles.y;

            // Set the camera's horizontal axis to match the rotation value
            freeLookCamera.m_XAxis.Value = playerFacingAngle;

            //Debug.Log("Camera reset");
        }
        else
        {
            Debug.LogWarning("No CinemachineFreeLook camera found.");
        }
    }
}