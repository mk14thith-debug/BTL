using UnityEngine;

public class GridCamera : MonoBehaviour
{
    [Header("Cài đặt")]
    public Transform player; 
    public float screenHeight = 10f; 
    public float screenWidth = 18f;  
    public float transitionSpeed = 10f; 

    private Vector3 targetPos;

    void Start()
    {

        targetPos = transform.position;
    }

    void LateUpdate()
    {
        if (player == null) return;
        float targetY = Mathf.Round(player.position.y / screenHeight) * screenHeight;
        float targetX = transform.position.x;
        targetPos = new Vector3(targetX, targetY, -10);
        transform.position = Vector3.Lerp(transform.position, targetPos, transitionSpeed * Time.deltaTime);
    }
}