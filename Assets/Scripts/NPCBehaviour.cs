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
        // destination = path.GetChild(childrenIndex).position;
        destination = RandomDestination();
        GetComponent<NavMeshAgent>().SetDestination(destination);
    }

    private void Update()
    {
        //Patroll();
        //FollowClick();
        //RandomPatroll();
        RandomMovement();
    }
    private void RandomPatroll()
    {
        if (Vector3.Distance(transform.position, destination) < Random.Range(1, 1.5f))
        {
            childrenIndex++;
            childrenIndex = Random.Range(0, path.childCount);

            destination = path.GetChild(childrenIndex).position;
            GetComponent<NavMeshAgent>().SetDestination(destination);
        }
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

    private void Patroll()
    {
        if (Vector3.Distance(transform.position, destination) < 1.5f)
        {
            childrenIndex++;
            childrenIndex = childrenIndex % path.childCount;

            destination = path.GetChild(childrenIndex).position;
            GetComponent<NavMeshAgent>().SetDestination(destination);
        }
    }

    private void FollowClick()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(ray, out hit, 1000))
            {
                GetComponent<NavMeshAgent>().SetDestination(hit.point);
                GetComponent<NavMeshAgent>().speed = 10;
            }

        }
    }
}
