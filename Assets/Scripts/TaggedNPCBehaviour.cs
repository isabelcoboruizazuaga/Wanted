using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class TaggedNPCBehaviour : MonoBehaviour
{
    [SerializeField] Vector3 destination;
    [SerializeField] Vector3 min, max;
    public float detectionRange = 4f;

    private NavMeshAgent agent;
    private GameObject player;
    private bool isRunning = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");


        destination = RandomDestination();
        agent.SetDestination(destination);

        //StartCoroutine(IncreaseSpeedPerSecond(5));
    }

    private void Update()
    {
        //Check player and enemy closeness
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if(distance < detectionRange)
        {
            //Make enemy run away
            Vector3 dirToPlayer= transform.position - player.transform.position;
            Vector3 newPos= transform.position + dirToPlayer;
            destination = newPos;

            agent.SetDestination(newPos);
        }
        else
        {
            RandomMovement();
        }
        
    }
    IEnumerator IncreaseSpeedPerSecond(float waitTime)
    {
        //while agent's speed is less than the speedCap
        while (agent.speed < 30)
        {
            //wait "waitTime"
            yield return new WaitForSeconds(waitTime);
            //add 0.5f to currentSpeed every loop 
            agent.speed = agent.speed + 0.5f;
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
