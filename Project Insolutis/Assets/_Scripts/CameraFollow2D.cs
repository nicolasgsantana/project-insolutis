using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [SerializeField] private float timeOffset = 5f;

    [SerializeField] private Vector2 posOffset;

    private Vector3 startPos;
    private Vector3 endPos;

    private Vector3 playerCenter;

    [SerializeField] private float leftLimit;
    [SerializeField] private float rightLimit;
    [SerializeField] private float bottomLimit;
    [SerializeField] private float topLimit;

    private void Update()
    {
        //Camera current position
        startPos = transform.position;
        //Player current position
        endPos = player.transform.position;

        playerCenter = player.GetComponent<Renderer>().bounds.center;

        posOffset.x = PlayerMovement.mousePos.x/2;
        posOffset.y = PlayerMovement.mousePos.y/2;

        endPos.x += posOffset.x;
        endPos.y += posOffset.y;
        endPos.z = -10;

        



    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(startPos, endPos, timeOffset * Time.deltaTime);

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, playerCenter.x - leftLimit, playerCenter.x + rightLimit),
            Mathf.Clamp(transform.position.y, playerCenter.y - bottomLimit, playerCenter.y + topLimit),
            transform.position.z
            ); ;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(new Vector2(playerCenter.x - leftLimit, playerCenter.y + topLimit), 
            new Vector2(playerCenter.x + rightLimit, playerCenter.y + topLimit));

        Gizmos.DrawLine(new Vector2(playerCenter.x + rightLimit, playerCenter.y + topLimit), 
            new Vector2(playerCenter.x + rightLimit, playerCenter.y - bottomLimit));

        Gizmos.DrawLine(new Vector2(playerCenter.x + rightLimit, playerCenter.y - bottomLimit),
            new Vector2(playerCenter.x - leftLimit, playerCenter.y - bottomLimit));

        Gizmos.DrawLine(new Vector2(playerCenter.x - leftLimit, playerCenter.y - bottomLimit),
            new Vector2(playerCenter.x - leftLimit, playerCenter.y + topLimit));

    }
}
