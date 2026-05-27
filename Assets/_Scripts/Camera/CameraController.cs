using UnityEngine;

namespace TopDown.CameraControl
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform playerTransform;
        [SerializeField] private float displacementMultiplier = 0.15f;
        private float zPosition = -10.0f;

        private void Update()
        {
            //Calculate mouse position and then the camera displacement depending on the difference between the player and the mouse
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 cameraDisplacement = (mousePosition - playerTransform.position) * displacementMultiplier;
            
            //Determine final camera position
            Vector3 finalCameraPosition = playerTransform.position + cameraDisplacement;
            finalCameraPosition.z = zPosition;
            transform.position = finalCameraPosition;
        }
    }
}
