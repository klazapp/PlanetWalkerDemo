using UnityEngine;

public class UtilityManager : MonoBehaviour
{

    public static float FixedDeltaTime;
    public static float DeltaTime;


    private void FixedUpdate()
    {
        FixedDeltaTime = Time.fixedDeltaTime;
    }

    private void Update()
    {
        DeltaTime = Time.deltaTime;
    }
}
