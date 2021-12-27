using UnityEngine;
using Unity.Mathematics;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{


    [Header("Joystick")]
    [SerializeField]
    private DynamicJoystick dynamicJoystick;

    private float JoystickInputX;
    private float JoystickInputY;
    private float3 JoystickDirectionF3;


  
    [Header("Player Speed")]  
    [Space(20)]
    [SerializeField] 
    private float PlayerRBMoveSpeed = 10f;
    [SerializeField] 
    private float PlayerChildRotSpeed = 10f;

    
    
    
    //Player Base Components
    private Rigidbody PlayerRB;
    private Transform PlayerChildTransform;
 
    

    private void Start()
    {
        //Assign Base Components
        PlayerRB = this.GetComponent<Rigidbody>();
        PlayerChildTransform = this.transform.GetChild(0);
        
        //Set player rb center of mass = 0
        PlayerRB.centerOfMass = new float3(0);
    }

    private void FixedUpdate()
    {
        #region Player RB Movement
        if (JoystickInputX != 0 && JoystickInputY != 0)
        {
            //move player parent rb in direction of joystick
            PlayerRB.AddRelativeTorque(JoystickDirectionF3 * UtilityManager.FixedDeltaTime * PlayerRBMoveSpeed, ForceMode.VelocityChange);
        }
        #endregion
    }

    private void Update()
    {
        #region joystick
        JoystickInputX = dynamicJoystick.Horizontal;
        JoystickInputY = dynamicJoystick.Vertical;
        JoystickDirectionF3 = new float3(-JoystickInputY, -JoystickInputX, 0);
        #endregion



        #region Player Child
        if (JoystickInputX != 0 && JoystickInputY != 0)
        {
            //Rotate player child based on joystick direction
            PlayerChildTransform.localRotation = math.slerp(PlayerChildTransform.localRotation, 
                quaternion.Euler(0, 0, (math.atan2(JoystickInputX, JoystickInputY))),
                PlayerChildRotSpeed * UtilityManager.DeltaTime);
        }
        #endregion
    }
    
    
    
    
    
    
    
    
    #region Properties
    
    public float3 PlayerChildPosition => PlayerChildTransform.position;
    public quaternion PlayerChildRotation => PlayerChildTransform.rotation;
    
    #endregion
}
