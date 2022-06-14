using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [SerializeField] private float timeOffset;

    [SerializeField] private Vector2 posOffset;

    private Vector3 startPos;
    [SerializeField]private Vector3 endPos;

    private void Update()
    {
        //Camera current position
        startPos = transform.position;
        //Player current position
        endPos = player.transform.position;

        posOffset.x = PlayerMovement.mousePos.x/6;
        posOffset.y = PlayerMovement.mousePos.y/6;

        endPos.x += posOffset.x;
        endPos.y += posOffset.y;
        endPos.z = -10;

        
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(startPos, endPos, timeOffset * Time.deltaTime);
    }
}
