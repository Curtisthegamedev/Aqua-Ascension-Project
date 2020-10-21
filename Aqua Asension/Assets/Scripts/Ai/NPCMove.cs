using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMove : MonoBehaviour
{
 [SerializeField] float angularVel = 45.0f;
    [SerializeField] Transform _destination;
    [SerializeField] GameObject Player;
    [SerializeField] Transform _destination2;
    [SerializeField] GameObject NPC;
    bool change = false;
    NavMeshAgent _navMeshAgent;

    // Start is called before the first frame update
    void Start()
    {
       

        _navMeshAgent = this.GetComponent<NavMeshAgent>();

        if (_navMeshAgent == null)
        {

            Debug.LogError("Nav Mesh Agent Component is not attached to " + gameObject.name);

        }

        else {

           
        }
    }
 
     void Update()
    {

       


    Vector3  TP = Player.transform.position;

        Vector3 D1 = _destination.transform.position;

        Vector3 NP = NPC.transform.position;

        Vector3 D2 = _destination2.transform.position;
       
        
    transform.Rotate(0, 0, angularVel * Time.deltaTime, Space.Self);

        
          if (change == false)
            {

                Vector3 targetVector = _destination.transform.position;

                _navMeshAgent.SetDestination(targetVector);
             if (NP.x == D1.x && NP.z == D1.z){

                change = true;

                }

            }
            if (change == true)
            {

                Vector3 targetVector = _destination2.transform.position;

                _navMeshAgent.SetDestination(targetVector);
                 if (NP.x ==D2.x && NP.z ==D2.z)
                 {
                
                change = false;

                }

             }
             if(NP.x - TP.x <= 5 && NP.x - TP.x >= -5 && NP.z - TP.z <= 5 && NP.z - TP.z >= -5)
        {
            Vector3 targetVector = Player.transform.position;

            _navMeshAgent.SetDestination(targetVector);

        }
      

             




    }





}
