using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{

    [SerializeField] float speed;
    [SerializeField] float nextWaypointDistance = 0.1f;
    [SerializeField] float radarRadius = 30f;

    [SerializeField] Transform playerTransform;
    [SerializeField] Transform redGeneratorTransform;
    [SerializeField] Transform greenGeneratorTransform;
    [SerializeField] Transform blueGeneratorTransform;

    [SerializeField] GameObject gameOverScreen;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    int nextPoint = 0;

    Seeker seeker;
    Rigidbody2D rb;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, .2f);
    }

    void FixedUpdate()
    {
        if(path == null)
            return;
        
        if(currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if(distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        Vector2 lookDir = (Vector2)path.vectorPath[currentWaypoint] - rb.position;
        lookDir = lookDir.normalized;

        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    void UpdatePath()
    {
        if(seeker.IsDone()){
            if(Vector2.Distance(playerTransform.position, transform.position) <= radarRadius){
                seeker.StartPath(rb.position, playerTransform.position, OnPathComplete);
            }
            else
            {
                switch(nextPoint)
                {
                    case 0:
                       seeker.StartPath(rb.position, redGeneratorTransform.position, OnPathComplete);
                       break;

                    case 1:
                        seeker.StartPath(rb.position, greenGeneratorTransform.position, OnPathComplete);
                        break;
                    
                    case 2:
                        seeker.StartPath(rb.position, blueGeneratorTransform.position, OnPathComplete);
                        break;
                }
            }
            
        }

            
    }

    void OnPathComplete(Path p)
    {
        if(!p.error)
        {
           path = p;
           currentWaypoint = 0; 
        }
            
    }

    private void OnCollisionEnter2D(Collision2D other) {
        
        if(other.gameObject.CompareTag("Player"))
            gameOverScreen.SetActive(true);

        else if(other.gameObject.CompareTag("Red Generator"))
            nextPoint++;

        else if(other.gameObject.CompareTag("Green Generator"))
            nextPoint++;

        else if(other.gameObject.CompareTag("Blue Generator"))   
            nextPoint = 0;
        
    }

}
