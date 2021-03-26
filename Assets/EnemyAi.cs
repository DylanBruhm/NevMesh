using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyAi : MonoBehaviour
{
    public float lookRadius = 10f;
    public float lookRadius2 = 10f;
    public GameObject player;
    public Transform playerPos;
    public GameObject startPosition;
    public GameObject startPosition2;
    Transform target;
    Transform lastLocation;
    NavMeshAgent agent;
    private int start = 5;
    private int follow = 4;
    private bool following;
    private bool scouting;
    private Vector3 destination;

    // Start is called before the first frame update
    void Start()
    {
        lastLocation = startPosition.transform;
        target = player.transform;
        agent = GetComponent<NavMeshAgent>();
        startPosition2.transform.position = startPosition.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        switch (start)
        {
            case 5:// sees player

                gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.green);
                if (distance <= lookRadius)
                {
                    start = 4;
                }
                break;
            case 4: // following player
                gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);

                agent.SetDestination(target.position);
                if (distance >= lookRadius)
                {
                    start = 3;
                    lastLocation.transform.position = playerPos.transform.position;
                }
                if (distance <= lookRadius2)// cant figure out how to not make color switch so fast
                {
                    start = 2;
                }
                    break;
            case 3:// retreating
                gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.yellow);
                agent.SetDestination(lastLocation.position);
                //Debug.Log("Here" +lastLocation.position);
                 
                
                if (distance <= lookRadius )// cant figure out how to not make color switch so fast
                {
                  
                    start = 5;
                }
                else if(distance >= lookRadius)
                {
                    Invoke("Retreat", 6.0f);

                }
                 
                
                    
                
                
                break;
            case 2:// retreating
                gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.black);
                agent.SetDestination(lastLocation.position);
                //Debug.Log("Here" +lastLocation.position);

               

                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                
                break;
            case 1:// retreating
                gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.blue);
                if (distance >= lookRadius)
                {
                    lastLocation.position = startPosition2.transform.position;
                    agent.SetDestination(lastLocation.position);
                }
                //Debug.Log("Here" +lastLocation.position);
                if (agent.velocity == new Vector3(0,0,0)) 
                {
                    start = 5;
                }
                if (distance <= lookRadius )// cant figure out how to not make color switch so fast
                {

                    start = 4;
                }
                break;
        }
        
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lookRadius2);
    }
    public void Retreat()
    {
        Debug.Log("fsdfdf");
        start = 1;
    }
    
}


