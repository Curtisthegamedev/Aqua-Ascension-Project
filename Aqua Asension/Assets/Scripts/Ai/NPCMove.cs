using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMove : MonoBehaviour
{

    [SerializeField] Transform _destination;
    [SerializeField] Transform Player;

    bool fine = false;

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


    private void Update()
    {
        if (fine == true)
        {


        }
        else
        {
            SetDestination();
        }
    }




    void OnTriggerEnter  (Collider collision)
    {
        

            if (collision.gameObject.CompareTag("Player"))
            {
            
            

            fine = true;

                Vector3 targetVector = Player.transform.position;
                _destination = null;
            }

        
 


    }




    void SetDestination()
    {
        
        if(_destination != null)
        {

            Vector3 targetVector = _destination.transform.position;
            _navMeshAgent.SetDestination(targetVector);

        }

    }
}
