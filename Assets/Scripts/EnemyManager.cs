using Unity.Mathematics;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    #region MISC
    private readonly float3 rightF3 = new float3(1, 0, 0);
    #endregion

    [Header("Player")] 
    [SerializeField]
    private PlayerController playerController;


    [Header("Enemy Prefab")]
    [Space(40)]
    [SerializeField]
    private GameObject EnemyPrefabGO;
    
    [Header("Total Enemy Count (1 ~ 20)")] 
    [Space(10)]
    [SerializeField]
    [Range(1, 20)]
    private int TotalEnemyCount = 20;
    
    [Header("Max Activated Enemy (1 ~ 8)")] 
    [Space(10)]
    [SerializeField]
    [Range(1, 8)]
    private int MaxActivatedEnemies = 4;
    
    
    [Header("Enemy Speed")] 
    [Space(10)]
    [SerializeField]
    private float EnemySpeed = 4f;



    
    //Enemy Spawn location and rotation vals
    private readonly float3 EnemyStartingSpawnPos = new float3(0, 0, 0);
    private readonly quaternion EnemyStartingSpawnRot = quaternion.Euler(math.radians(new float3(-60f, 150f,0)));




    //Enemy base components
    private GameObject[] EnemyGO;
    private Rigidbody[] EnemyRB;
    private Transform[] EnemyChildTransform;
    private GameObject[] EnemyChildGO;


    //Enemy child direction components
    private float3 enemyChildToPlayerDir;
    private float enemyChildToPlayerRotationAngle;

    

    private void Awake()
    {
        //Initialize Array Values
        EnemyGO = new GameObject[TotalEnemyCount];
        EnemyRB = new Rigidbody[TotalEnemyCount];
        EnemyChildTransform = new Transform[TotalEnemyCount];
        EnemyChildGO = new GameObject[TotalEnemyCount];
    }

    private void Start()
    {
        for (var i = 0; i < TotalEnemyCount; i++)
        {
            //Instantiate Enemy
            EnemyGO[i] = Instantiate(EnemyPrefabGO, EnemyStartingSpawnPos, EnemyStartingSpawnRot);
           
            
            //Assign Array values
            EnemyRB[i] = EnemyGO[i].GetComponent<Rigidbody>();
            EnemyChildTransform[i] = EnemyGO[i].transform.GetChild(0);
            EnemyChildGO[i] = EnemyChildTransform[i].gameObject;


            //Activate max no of enemies
            if (i < MaxActivatedEnemies)
            {
                EnemyGO[i].SetActive(true);
                
                //set enemy rb centre of mass = 0
                EnemyRB[i].centerOfMass = new float3(0);
            }
            else
            {
                EnemyGO[i].SetActive(false);
            }
    
        }
    }

    private void FixedUpdate()
    {
        for (var i = 0; i < TotalEnemyCount; i++)
        {
            //Move enemy parent in direction of child
            EnemyRB[i].AddTorque(-(EnemyChildTransform[i].rotation * rightF3) * UtilityManager.FixedDeltaTime * EnemySpeed,
                ForceMode.VelocityChange);
        }
    }


    private void Update()
    {
        for (var i = 0; i < TotalEnemyCount; i++)
        {
            //Get relative direction from player
            enemyChildToPlayerDir = EnemyChildTransform[i].InverseTransformPoint(playerController.PlayerChildPosition);

            //Convert direction to angle
            enemyChildToPlayerRotationAngle =
                math.atan2(enemyChildToPlayerDir.y, enemyChildToPlayerDir.x) - math.radians(90f);

            //Apply rotation to enemy child
            EnemyChildTransform[i].localRotation *= quaternion.Euler(0, 0, enemyChildToPlayerRotationAngle);
        }
    }









    
    
  



}
