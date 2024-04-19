using UnityEngine;
using UnityEngine.AI;

public class TaggedNPCBehaviour : MonoBehaviour
{
    [SerializeField] Transform path;
    [SerializeField] int childrenIndex;
    [SerializeField] Vector3 destination;
    [SerializeField] Vector3 min, max;
    [SerializeField] float detectionRange;

    public bool _running = false;
    public bool running
    {
        get { return _running; }
        set
        {
            if (value == _running)
                return;

            _running = value;
            if (_running)
            {
                RunAway();
            }
            else
            {
                GetComponent<NavMeshAgent>().speed = 2;
            }
        }
    }

    private void Start()
    {
        // destination = path.GetChild(childrenIndex).position;
        destination = RandomDestination();
        GetComponent<NavMeshAgent>().SetDestination(destination);
    }

    private void Update()
    {
        CheckPlayer();

        if (!running) { RandomMovement(); }
    }

    private void CheckPlayer()
    {
        var playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        var thisPos = transform.position;

        //It has to be inside the distance range
        if ((Mathf.Abs(playerPos.z - thisPos.z) < detectionRange) && (Mathf.Abs(playerPos.x - thisPos.x) < detectionRange))
        {
            running = true;

        }
        else
        {
            running = false;
        }
    }
    private void RunAway()
    {
        destination = RandomFarDestination();
        GetComponent<NavMeshAgent>().speed = 20;
        GetComponent<NavMeshAgent>().SetDestination(destination);

        //If it's near the new destination it stops running
        /*if (Vector3.Distance(transform.position, destination) < 1.5f)
        {
            running = false;
        }*/
    }

    private void RandomMovement()
    {
        if (Vector3.Distance(transform.position, destination) < 1.5f)
        {
            destination = RandomDestination();
            GetComponent<NavMeshAgent>().SetDestination(destination);
        }

    }
    Vector3 RandomFarDestination()
    {
        var playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        var thisPos = transform.position;

        //Tenemos nuestra posición y la del player, hay que ver si estas delante o detras del player, si estas frente a el debes correr hacia adelante, si no hacia atras
        //entonces: 
        /* si px= 1 y mx= 2 -> estás frente al player, corre hacia x= 3, 4, etc
         * si px= 1 y mx= -1 -> estás tras el player, corre hacia x= -2, -3, etc
         * si px= -1 y mx= 2 -> estás frente al player, ve a x= 3,4, etc
         * si px = -1 y mx= -2 -> estás tras el player, ve a x= -3, -4, etc
         */


        float posx = transform.position.x;
        float posz = transform.position.z;

        //The new position must be generated inside world bounds
        if (posx + 10 > max.x) { posx -= 20; }
        if (posx - 10 < min.x) { posx += 20; }

        if (posz + 10 > max.z) { posz -= 20; }
        if (posz - 10 < min.z) { posz += 20; }

        //Generate new random position in a 5 meters range of npc
        return new Vector3(Random.Range(posx - 10, posx + 10), 0, Random.Range(posz - 10, posz + 10));
    }

    Vector3 RandomDestination()
    {
        float posx = transform.position.x;
        float posz = transform.position.z;

        //The new position must be generated inside world bounds
        if (posx + 5 > max.x) { posx -= 10; }
        if (posx - 5 < min.x) { posx += 10; }

        if (posz + 5 > max.z) { posz -= 10; }
        if (posz - 5 < min.z) { posz += 10; }

        //Generate new random position in a 5 meters range of npc
        return new Vector3(Random.Range(posx - 5, posx + 5), 0, Random.Range(posz - 5, posz + 5));
    }
}
