using Fergicide;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    [SerializeField] private Canvas wantedCanvas;
    [SerializeField] public GameObject[] NPCsList;
    [SerializeField] private GameObject EnemyNPC;
    [SerializeField] private GameObject player;
    [SerializeField] private Camera initialCamera;
    [SerializeField] private int npcNumber;

    private List<GameObject> NPCs = new List<GameObject>();
    public static int score = -1;
    private float timeLeft = 30f;
    private bool playing = false;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timeText;

    [SerializeField] private Sprite[] imageList;
    [SerializeField] private Image imgWantedSmall, imgWantedBig;

    private Image wantedCharImage;
    /*
      Dbug
      Dcap
      Dcode
    Dcoy
    Dfrag
    Djay
    Dplane
    Dtox
    Dva
    Dvoid
    Dznuts
      
     * 
     * 
     */

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
                SceneManager.LoadScene("EndScene");
            }
        }
    }

    public void StartGame()
    {
        score++;
        scoreText.text = "Score: " + score;
        StartCoroutine(WantedCoroutine());
    }

    private void SetImage(string skinName)
    {
        for (int i = 0; i < imageList.Length; i++)
        {
            if (imageList[i].name == skinName)
            {
                imgWantedSmall.sprite = imageList[i];
                imgWantedBig.sprite = imageList[i];

            }
        }
    }
    private GameObject GenerateEnemy()
    {
        Vector3 enemyPosition = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
        GameObject enemy = Instantiate(EnemyNPC, enemyPosition, Quaternion.identity);
        string skin = enemy.GetComponentInChildren<DfaultsController1>().dfaultsConfig.name;
        SetImage(skin);

        return enemy;
    }

    IEnumerator WantedCoroutine()
    {
        //Game control
        playing = false;
        DeleteNPCs();

        //UI Controll
        initialCamera.enabled = true;
        wantedCanvas.enabled = true;

        //Enemy instantiation
        GameObject enemy = GenerateEnemy();
        enemy.SetActive(false);

        //Player controll
        player.SetActive(false);



        yield return new WaitForSeconds(3);


        wantedCanvas.enabled = false;

        //Player
        initialCamera.enabled = false;
        player.SetActive(true);

        //NPCs 
        GenerateNPCs();
        enemy.SetActive(true);

        //Game Controll
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
                NPCs.Add(Instantiate(NPCsList[Random.Range(0, NPCsList.Length)], position, Quaternion.identity));
            }
            else
            {
                NPCs[i].SetActive(true);
                NPCs[i].transform.position = position;
                NPCs[i].GetComponent<NPCBehaviour>().StartMoving();
            }
        }
    }

    private void DeleteNPCs()
    {
        for (int i = 0; i < NPCs.Count; i++)
        {
            NPCs[i].SetActive(false);
        }
    }
}
