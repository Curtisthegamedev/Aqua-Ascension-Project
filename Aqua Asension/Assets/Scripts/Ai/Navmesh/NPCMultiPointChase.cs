using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class NPCMultiPointChase : MonoBehaviour
{
    [SerializeField] bool StallMove;

    [SerializeField] float StallTime = 4f;

    [SerializeField] float Switch = 0.1f;

    [SerializeField] List<Waypoint> patrolpoint; 

    NavMeshAgent navMeshAgent;


    int currentPatrolIndex;
    bool move;
    bool Wait;
    bool patrolF;
    float waitimmer;

    // Start is called before the first frame update
    public void  Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        if(navMeshAgent == null)
        {
            Debug.LogError("No nav mesh on. " + gameObject.name + " wtf.");
        }
        else
        {
            if (patrolpoint != null && patrolpoint.Count >= 2)
            {
                currentPatrolIndex = 0;
                SetDestination();
            }
            else
            {

                Debug.Log("Not enough points");
            }
        }


    }

    // Update is called once per frame
   public void Update()
    {
       if(move && navMeshAgent.remainingDistance <= 1.0f)
        {

            move = false;

            if(StallMove)
            {

                Wait = true;
               waitimmer = 0f;

            }
            else
            {
                ChangePatrolPoint();
                SetDestination();
            }

        }
        if (Wait)
        {
            waitimmer += Time.deltaTime;
            if(waitimmer >= StallTime)
            {
                Wait = false;

                ChangePatrolPoint();
                SetDestination();
            }
        }
    }

    private void SetDestination()
    {
        if(patrolpoint != null)
        {

            Vector3 targetVector = patrolpoint[currentPatrolIndex].transform.position;
            navMeshAgent.SetDestination(targetVector);
            move = true;
        }

    }

    private void ChangePatrolPoint()
    {
        if (UnityEngine.Random.Range(0f,1f)<= Switch)
        {
            patrolF = !patrolF;
        }
        if (patrolF)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolpoint.Count;
        }
        else
        {
            if(--currentPatrolIndex < 0)
            {
                currentPatrolIndex = patrolpoint.Count - 1;
            }
        }

    }
}
//https://youtu.be/5q4JHuJAAcQ?list=PL8lV_joQZ5sfqiNwoJcokJlcrgplW8uSs