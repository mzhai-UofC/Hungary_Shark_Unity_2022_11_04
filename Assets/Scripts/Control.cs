using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Control : MonoBehaviour
{
    public FishFactory fishFactory;
    public float delayTime = 3f;
    float lastTime = 0f;
    public int levelLine = 20;

    public int hp = 200;
    [SerializeField] Image hpImage;

    public float hungry;//other object cannot use, so put the variable in playercontroller

    Vector3 leftBottomPoint, rightTopPoint;

    Fish player;

    public static int score = 0;

    int playerLevel =2;

    int maxLevel;

    void Awake()
    {
        maxLevel = fishFactory.fishes.Length - 1;

        MakePlayerFish();

        leftBottomPoint = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f,
            Mathf.Abs(Camera.main.transform.position.z)));
        rightTopPoint = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f,
                Mathf.Abs(Camera.main.transform.position.z)));
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        LevelUpdate();
        PlayerUpdate();

        lastTime += Time.deltaTime;
        if (lastTime >= delayTime)
        {
            lastTime = 0f;
            MakeFish();
            MakeFish_poision();
        }
    }

    void PlayerUpdate()
    {
        Vector3 playerInput = new Vector3(0, 0, 0);
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
        player.SetDesiredVelocity(playerInput);
    }

    void MakeFish()
    {
        Fish fish = fishFactory.Get(0);


        if (Random.Range(0, 2) == 0)
        {
            fish.transform.position = new Vector3(leftBottomPoint.x - 2f,
                        Random.Range(leftBottomPoint.y, rightTopPoint.y), 0f);
            fish.SetDesiredVelocity(Vector3.right);
        }
        else
        {
            fish.transform.position = new Vector3(rightTopPoint.x + 2f,
                        Random.Range(leftBottomPoint.y, rightTopPoint.y), 0f);
            fish.SetDesiredVelocity(Vector3.left);
        }
    }

    void MakeFish_poision()
    {
        Fish fish_poision = fishFactory.Get(1);


        if (Random.Range(0, 2) == 0)
        {
            fish_poision.transform.position = new Vector3(leftBottomPoint.x - 2f,
                        Random.Range(leftBottomPoint.y, rightTopPoint.y), 0f);
            fish_poision.SetDesiredVelocity(Vector3.right);
        }
        else
        {
            fish_poision.transform.position = new Vector3(rightTopPoint.x + 2f,
                        Random.Range(leftBottomPoint.y, rightTopPoint.y), 0f);
            fish_poision.SetDesiredVelocity(Vector3.left);
        }
    }

    void MakePlayerFish()
    {
        Vector3 playerPosition = new Vector3(0f, 0f, 0f);
        Vector3 playerEulerAngles = new Vector3(0f, 0f, 0f);
        if (player != null)
        {
            playerPosition = player.transform.localPosition;
            playerEulerAngles = player.transform.localEulerAngles;
            Destroy(player.gameObject);
        }

        player = fishFactory.Get(playerLevel);
        player.transform.localPosition = playerPosition;
        player.transform.localEulerAngles = playerEulerAngles;
        player.tag = "Player";
    }

    void LevelUpdate()
    {
       
            if ((score / levelLine + 1) > playerLevel && (playerLevel < maxLevel))
            {
                playerLevel++;
            hp += 50;
            hpImage.fillAmount = hp / 100;
            MakePlayerFish();
            }
        }

    private void UpdateHungry()
    {
        hungry -= Time.deltaTime * 3;
        if (hungry <= 0)
        {
            hungry = 0;
           hp += 40;
        }
        hpImage.fillAmount = hungry / 100;
    }
    }