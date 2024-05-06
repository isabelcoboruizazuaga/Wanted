using UnityEngine;
using UnityEngine.AI;

public class TaggedNPCBehaviour : MonoBehaviour
{
    [SerializeField] Vector3 destination;
    [SerializeField] Vector3 min, max;
    public float detectionRange = 4f;
    public float runRange = 3.9f;

    private NavMeshAgent agent;
    private GameObject player;
    private Animator anim;

    private bool playerSeen=false;

    void Start()
    {
         anim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");


        destination = RandomDestination();
        agent.SetDestination(destination);

    }

    private void Update()
    {
        //Check player and enemy closeness
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance < detectionRange)
        {
            if(!playerSeen)
            {
                //anim escape
                anim.SetTrigger("SeenPlayer");
                playerSeen = true;
            }
            if(distance < runRange)
            {
                //Make enemy run away
                Vector3 dirToPlayer = transform.position - player.transform.position;
                Vector3 newPos = transform.position + dirToPlayer;
                destination = newPos;

                agent.SetDestination(newPos);
            }
                 
        }
        else
        { 
            playerSeen = false;
            RandomMovement();
        }

    }

    private void RandomMovement()
    {
        if (Vector3.Distance(transform.position, destination) < 1.5f)
        {
            destination = RandomDestination();
            agent.SetDestination(destination);
        }

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
