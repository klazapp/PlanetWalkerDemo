using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    
    [Header("Player")] 
    [SerializeField]
    private PlayerController playerController;

    

    [Header("Bullet Prefab")] 
    [SerializeField]
    private GameObject BulletPrefabGO;

    [Range(5, 10)]
    [SerializeField] 
    private int NoOfBulletsToSpawn = 10;
    
    
    //Bullet Spawn location and rotation vals
    private readonly float3 BulletStartingSpawnPos = new float3(0, 0, 0);
    private readonly quaternion BulletStartingSpawnRot = quaternion.Euler(0, 0, 0);
    
    
    //Base components
    private GameObject[] BulletGO;
    private Transform[] BulletTransform;
    private Rigidbody[] BulletRB;
    private Transform[] BulletChildTransform;
    
    //Activated components
    private bool[] BulletIsActivated;
    private float[] BulletIsActivatedTimer;
    
    //Bullet Activated max duration
    [SerializeField] 
    private float BulletIsActivatedInterval = 0.5f;
    
    
    

    //Bullet Spawn Timer
    [SerializeField] 
    private float BulletSpawnInterval = 0.1f;
    private float BulletSpawnDuration;
    
    
    
    //Current Bullet Counter
    private int CurrentBulletCounter;


    [SerializeField] 
    private float BulletFlySpeed = 30f;
    
    
    

  



    private void Awake()
    {
        //Initialize Array Values
        BulletGO = new GameObject[NoOfBulletsToSpawn];
        BulletTransform = new Transform[NoOfBulletsToSpawn];
        BulletRB = new Rigidbody[NoOfBulletsToSpawn];
        BulletChildTransform = new Transform[NoOfBulletsToSpawn];
        BulletIsActivated = new bool[NoOfBulletsToSpawn];
        BulletIsActivatedTimer = new float[NoOfBulletsToSpawn];
    }

    private void Start()
    {
        for (var i = 0; i < NoOfBulletsToSpawn; i++)
        {
            //Instantiate Bullet
            BulletGO[i] = Instantiate(BulletPrefabGO, BulletStartingSpawnPos, BulletStartingSpawnRot);
            BulletGO[i].SetActive(false);
            
            //Assign Array values
            BulletTransform[i] = BulletGO[i].transform;
            BulletRB[i] = BulletGO[i].GetComponent<Rigidbody>();
            BulletChildTransform[i] = BulletGO[i].transform.GetChild(0);
        }
    }

    private void FixedUpdate()
    {
        BulletSpawnDuration +=  UtilityManager.FixedDeltaTime;

        if (BulletSpawnDuration >= BulletSpawnInterval)
        {
            //Spawn Next Bullet
            ActivateNextBullet();
            
            
            //Reset duration
            BulletSpawnDuration = 0;
        }


        for (var i = 0; i < NoOfBulletsToSpawn; i++)
        {
            if (!BulletIsActivated[i])
                continue;
            
            
            BulletRB[i].AddTorque( 
                -(BulletChildTransform[i].rotation * new float3(1,0, 0)) *
                UtilityManager.FixedDeltaTime * BulletFlySpeed, ForceMode.VelocityChange);


            //Deactivate Bullet After Time Passed
            BulletIsActivatedTimer[i] += UtilityManager.FixedDeltaTime;
            if (BulletIsActivatedTimer[i] >= BulletIsActivatedInterval)
            {
                DeactivateCurrentBullet(i);
            }
        }
        
        
    }



    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void ActivateNextBullet()
    {
        BulletTransform[CurrentBulletCounter].rotation = playerController.PlayerChildRotation;
        BulletGO[CurrentBulletCounter].SetActive(true);
        BulletRB[CurrentBulletCounter].centerOfMass = new float3(0);
        
        //Reset to prevent spillage from previously activated velocity
        BulletRB[CurrentBulletCounter].angularVelocity = new float3(0);
        
        BulletIsActivated[CurrentBulletCounter] = true;
        BulletIsActivatedTimer[CurrentBulletCounter] = 0;


        //Increase counter
        CurrentBulletCounter++;
        if (CurrentBulletCounter > NoOfBulletsToSpawn - 1)
            CurrentBulletCounter = 0;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void DeactivateCurrentBullet(int BulletIndex)
    {
        BulletGO[BulletIndex].SetActive(false);

        BulletIsActivated[BulletIndex] = false;
    }

}
