using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player;
    public float minXClamp = -2.13f;
    public float maxXClamp = 12.93f;
    
    // Update is called once per frame
    void LateUpdate()
    {
        if (player)
        {
            Vector3 cameraTransform;
            
            cameraTransform = transform.position;          
            cameraTransform.x = player.transform.position.x;
            cameraTransform.x = Mathf.Clamp(cameraTransform.x, minXClamp, maxXClamp);           
            transform.position = cameraTransform;

        }
        
    }
}
