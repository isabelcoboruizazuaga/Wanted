using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCGenerator : MonoBehaviour
{
    public GameObject NPC;
    public int npcNumber = 5;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < npcNumber; i++)
        {
            var position = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
            Instantiate(NPC, position, Quaternion.identity);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
