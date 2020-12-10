using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveDessision : MonoBehaviour
{

    public enum PreformAction
    {
        WAIT, MOVE, PLAYER
    }
    public PreformAction MS;

    [SerializeField] GameObject TargetPlayer;

    [SerializeField] bool StallMove;

    [SerializeField] float StallTime;

    [SerializeField] float Switch = 0.1f;

    [SerializeField] List<Waypoint> patrolpoint;

    NavMeshAgent navMeshAgent;
    bool tur = false;

    int currentPatrolIndex;
    bool move;
    
    bool patrolF;
    float waitimmer;
    bool player;

    // Start is called before the first frame update
    public void Start()
    {
        player = false;
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        if (navMeshAgent == null)
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

 MS = PreformAction.MOVE;
    }

    // Update is called once per frame
    public void Update()
    {
        Vector3 NP = TargetPlayer.transform.position;
        Vector3 TP = navMeshAgent.transform.position;
        switch (MS)
        {
            case (PreformAction.MOVE):
            

    if (move && navMeshAgent.remainingDistance <= 1.0f)
        {

            move = false;

           
              waitimmer = 0f;
            
              
                    MS = PreformAction.WAIT;

        }
                if (TP.x - NP.x <= 5 && TP.x - NP.x >= -5 && TP.z - NP.z <= 5 && TP.z - NP.z >= -5) {


                    MS = PreformAction.PLAYER;

                }
                

                break;


            case (PreformAction.WAIT):

              waitimmer += Time.deltaTime;

            if (waitimmer >= StallTime)
            {
               
                ChangePatrolPoint();
                SetDestination();
                    MS = PreformAction.MOVE;
                }
             
              
                break;


            case (PreformAction.PLAYER):

                navMeshAgent.SetDestination(NP);



                if (move && navMeshAgent.remainingDistance <= 1.0f)
                        {

                          Destroy(TargetPlayer);
                  
                            ChangePatrolPoint();
                            SetDestination();
                          MS = PreformAction.MOVE;
                         }



                    



                
               

                    break;
        }


      
           
       
    }

    private void play()
    {


        void OnTriggerStay(Collider collision)
        {
            Debug.Log("collision triggered");
            if (collision.gameObject.CompareTag("Player"))
            {


                MS = PreformAction.PLAYER;

            }



        }
    }

        private void SetDestination()
    {
        if (patrolpoint != null)
        {

            Vector3 targetVector = patrolpoint[currentPatrolIndex].transform.position;
            navMeshAgent.SetDestination(targetVector);
            move = true;
        }

    }

    private void ChangePatrolPoint()
    {
        if (UnityEngine.Random.Range(0f, 1f) <= Switch)
        {
            patrolF = !patrolF;
        }
        if (patrolF)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolpoint.Count;
        }
        else
        {
            if (--currentPatrolIndex < 0)
            {
                currentPatrolIndex = patrolpoint.Count - 1;
            }
        }

    }
    
    
}