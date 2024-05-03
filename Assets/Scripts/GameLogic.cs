using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    [SerializeField] private Canvas wantedCanvas;
    [SerializeField] private GameObject NPC;
    [SerializeField] private GameObject EnemyNPC;
    [SerializeField] private GameObject player;
    [SerializeField] private Camera initialCamera;
    [SerializeField] private int npcNumber;

    private List<GameObject> NPCs = new List<GameObject>();
    private int score = -1;
    private float timeLeft = 30f;
    private bool playing = false;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timeText;

    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    void Update()
    {
        if (playing)
        {

            timeLeft -= Time.deltaTime;
            timeText.text = "00:" + System.Math.Floor(timeLeft); 

            if (timeLeft < 0)
            {
                //GameOver();
            }
        }
    }

    public void StartGame()
    {
        score++;
        scoreText.text = "Score: " + score;
        StartCoroutine(WantedCoroutine());
    }

    IEnumerator WantedCoroutine()
    {
        playing = false;
        DeleteNPCs();
        player.SetActive(false);
        initialCamera.enabled = true;
        wantedCanvas.enabled = true;

        yield return new WaitForSeconds(3);

        wantedCanvas.enabled = false;
        GenerateNPCs();
        initialCamera.enabled = false;
        player.SetActive(true);
        timeLeft = 30;
        playing = true;


    }

    private void GenerateNPCs()
    {
        for (int i = 0; i < npcNumber; i++)
        {
            var position = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));

            if (NPCs.Count < npcNumber)
            {
                NPCs.Add(Instantiate(NPC, position, Quaternion.identity));
            }
            else
            {
                NPCs[i].SetActive(true);
                NPCs[i].transform.position = position;
                NPCs[i].GetComponent<NPCBehaviour>().StartMoving();
            }
        }
        var enemyPosition = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
        Instantiate(EnemyNPC, enemyPosition, Quaternion.identity);
    }

    private void DeleteNPCs()
    {
        for (int i = 0; i < NPCs.Count; i++)
        {
            NPCs[i].SetActive(false);
        }
    }
}
