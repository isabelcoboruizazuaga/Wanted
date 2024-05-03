using UnityEngine;
using UnityEngine.AI;

public class NPCBehaviour : MonoBehaviour
{
    [SerializeField] Transform path;
    [SerializeField] int childrenIndex;
    [SerializeField] Vector3 destination;
    [SerializeField] Vector3 min, max;

    private void Start()
    {
        StartMoving();
        
    }

    public void StartMoving()
    {
        destination = RandomDestination();
        GetComponent<NavMeshAgent>().SetDestination(destination);
    }

    private void Update()
    {
        RandomMovement();
    }

    private void RandomMovement()
    {
        if (Vector3.Distance(transform.position, destination) < 1.5f)
        {
            destination = RandomDestination();
            GetComponent<NavMeshAgent>().SetDestination(destination);


        }

    }

    Vector3 RandomDestination()
    {
       float posx= transform.position.x;
       float posz= transform.position.z;

        //The new position must be generated inside world bounds
        if (posx + 5 > max.x) { posx -=10; }
        if(posx - 5 < min.x) { posx +=10;}

        if (posz + 5 > max.z) { posz -= 10; }
        if (posz - 5 < min.z) { posz += 10; }

        //Generate new random position in a 5 meters range of npc
        return new Vector3(Random.Range(posx-5, posx+5), 0, Random.Range(posz-5, posz+5));
    }
}
