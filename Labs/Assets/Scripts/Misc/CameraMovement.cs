using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player;
    public float minXClamp = -2.16f;
    public float maxXClamp = 15.06f;
    
    // Update is called once per frame
    void LateUpdate()
    {
        if (GameManager.instance.playerInstance)
        {
            Vector3 cameraTransform;
            
            cameraTransform = transform.position;
            cameraTransform.x = GameManager.instance.playerInstance.transform.position.x;
            cameraTransform.x = Mathf.Clamp(cameraTransform.x, minXClamp, maxXClamp);           
            transform.position = cameraTransform;

        }
        
    }
}
