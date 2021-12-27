using UnityEngine;
using Unity.Mathematics;

public class CameraController : MonoBehaviour
{

    [Header("Target")] 
    [SerializeField] 
    private Transform playerTransform;

    
    [Header("Camera Speed")] 
    [SerializeField] 
    private float camFollowSpeed = 15f;

    
    //Base Component
    private Transform camTransform;

    private void Start()
    {
        camTransform = this.transform;
    }

    private void LateUpdate()
    {
        camTransform.localRotation =
            math.slerp(camTransform.localRotation, playerTransform.localRotation, camFollowSpeed * Time.deltaTime);
    }
}
