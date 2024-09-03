using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlaySceneController: MonoBehaviour
{
    EntityHandler Handler;

    public States CurrentState;
    public States NextState;

    public GameObject Character;
    public GameObject[] enemyTypes;
    public GameObject boss;
    List<GameObject> Platforms = new List<GameObject>();
    List<GameObject> Ladders = new List<GameObject>();
    public GameOverUI GameOverUI;
    public GameObject GameWinUI;
    public GameObject tutorialUI;
    public GameObject storyUI;

    public int LevelNumber;
    int stateChangeTimer = 60;
    public bool StartFlag = false;
    public bool StartFlagTriggered = false;

    Dictionary<int, GameObject> enemies = new Dictionary<int, GameObject>();
    GameObject player;
    public GameObject ladder;
    public GameObject[] platforms;

    AudioManager aaudio;
    void Start()
    {
        Time.timeScale = 1.0f;
        Application.targetFrameRate = 60;
        LevelNumber = LevelData.LevelNumber;
        Handler = SetUpEntityGenerator();
        SetUpPlatforms();
        GenerateEntities();
        MakeLadderCollisionExclusive();
        MakePlayerEnemyCollisionExclusive();
        Camera.main.GetComponent<MoveWithPlayer>().setPlayer();
        CurrentState = States.WaitingForInput;
        aaudio = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();

        if (LevelNumber > 0 && LevelNumber < 4)
        {
            if (!("v1".Equals(aaudio.currentBGM)))
            {
                aaudio.SetBGM("v1");
            }
        }
        else if (LevelNumber > 3 && LevelNumber < 7)
        {
            if (!("v2".Equals(aaudio.currentBGM)))
            {
                aaudio.SetBGM("v2");
            }
        }
        else if (LevelNumber > 6 && LevelNumber < 10)
        {
            if (!("v3".Equals(aaudio.currentBGM)))
            {
                aaudio.SetBGM("v3");
            }
        }
        else
        {
            if (!("v4".Equals(aaudio.currentBGM)))
            {
                aaudio.SetBGM("v4");
            }
        }
    }

    private void MakePlayerEnemyCollisionExclusive()
    {
        foreach (GameObject enemy in enemies.Values)
        {
            Physics2D.IgnoreCollision(player.GetComponent<BoxCollider2D>(), enemy.GetComponent<BoxCollider2D>());
        }
    }

    private void Update()
    {
        if (NextState != CurrentState)
        {
            CurrentState = NextState;
        }
        if (CurrentState == States.Win)
        {
            Win();
        }
        if (CurrentState == States.WaitingForInput)
        {
            if (stateChangeTimer > 0)
            {
                stateChangeTimer--;
            }
            else
            {
                Handler.clickEnemy();
                stateChangeTimer = 60;
            }
        }
        try
        {
            if (StartFlag)
            {
                StartFlag = false;
                NextState = States.WaitingForInput;
            }
            else if (StartFlag == false && StartFlagTriggered == false)
            {
                if (tutorialUI == null && !storyUI.activeInHierarchy)
                {
                    StartFlag = true;
                    StartFlagTriggered = true;
                }
                else if (!tutorialUI.activeInHierarchy && !storyUI.activeInHierarchy)
                {
                    StartFlag = true;
                    StartFlagTriggered = true;
                }
            }
        }
        catch{}
    }

    private void MakeLadderCollisionExclusive()
    {
        try
        {
            foreach (GameObject ladder in Ladders)
            {
                Physics2D.IgnoreCollision(ladder.GetComponent<BoxCollider2D>(), player.GetComponent<BoxCollider2D>());
                foreach (GameObject enemy in enemies.Values)
                {
                    Physics2D.IgnoreCollision(ladder.GetComponent<BoxCollider2D>(), enemy.GetComponent<BoxCollider2D>());
                }
            }
        }
        catch{ }
    }

    private void GenerateEntities()
    {
        player = Handler.generatePlayer();
        enemies = Handler.generateEnemies(LevelNumber, Platforms);

        if (LevelNumber == 10)
        {
             enemies = Handler.GenerateBoss();
        }
    }

    private EntityHandler SetUpEntityGenerator()
    {
        this.transform.AddComponent<EntityHandler>();
        this.transform.GetComponent<EntityHandler>().character = Character;
        this.transform.GetComponent<EntityHandler>().enemyTypes = enemyTypes;
        this.transform.GetComponent<EntityHandler>().boss = boss;
        return this.transform.GetComponent<EntityHandler>();
    }

    private void SetUpPlatforms()
    {
        int platformNum = 0;
        switch (LevelNumber)
        {
            case 0: platformNum = 10; break;
            case 1: platformNum = 2; break;
            case 2: platformNum = 3; break;
            case 3: platformNum = 4; break;
            case 4: platformNum = 4; break;
            case 5: platformNum = 5; break;
            case 6: platformNum = 5; break;
            case 7: platformNum = 6; break;
            case 8: platformNum = 6; break;
            case 9: platformNum = 7; break;
            case 10: platformNum = 0; break;
        }
        float platformOffsetY = 7.2f;
        Sides prevPlatformSide = Sides.Nil;
        for (int i = 1; i <= platformNum; i++)
        {
            int a = LevelNumber / 2;
            int b = LevelNumber - a;

            bool left = ((((b + i)/2)^(a + i)) % 2 > 0) ? true : false;

            if (left)
            {
                Platforms.Add(Instantiate(platforms[UnityEngine.Random.Range(0,2)], new Vector2((float)(Mathf.Max((((b + i) / 2) ^ (a + i)), -2.8f) % 1.4), platformOffsetY + (i - 1) * 1.6f), Quaternion.identity, GameObject.FindWithTag("Environment").transform));
                /*if (prevPlatformSide == Sides.Left)
                {
                    Ladders.Add(Instantiate(ladder, new Vector2(0.3f, platformOffsetY - 0.5f + (i - 1) * 1.6f), Quaternion.identity, GameObject.FindWithTag("Environment").transform));
                }*/
                prevPlatformSide = Sides.Left;
            }
            else
            {
                Platforms.Add(Instantiate(platforms[UnityEngine.Random.Range(0, 2)], new Vector2((float)((((b + i) / 2) ^ (a + i)) % 1.4) + 4, platformOffsetY + (i - 1) * 1.6f), Quaternion.identity, GameObject.FindWithTag("Environment").transform));
                /*if (prevPlatformSide == Sides.Right)
                {
                    Ladders.Add(Instantiate(ladder, new Vector2(5.2f, platformOffsetY - 0.5f + (i - 1) * 1.6f), Quaternion.identity, GameObject.FindWithTag("Environment").transform));
                }*/
                prevPlatformSide = Sides.Right;
            }
        } 
    }

    public void CreateDrawingPanel()
    {
        DrawingPanel _temp = this.transform.GetComponent<DrawingPanel>();
        if (_temp != null )
        {
            Destroy(_temp);
        }
        if(transform.GetComponent<EntityHandler>().enemies.Count > 0)
        {
            this.transform.AddComponent<DrawingPanel>();
            Camera.main.gameObject.layer = LayerMask.NameToLayer("DrawingElements");
        }
    }

    public void KillEnemy(int killcount)
    {
        Handler.killEnemy(killcount);
        if(enemies.Count <= 0 )
        {
            Win();
        }
    }
    
    public void EnemyAttack()
    {
        Handler.EnemyAttack();
    }

    public void TakeDamage(int damage)
    {
        Handler.takeDamage(damage);
    }

    public void DestroyGameObject(GameObject obj)
    {
        Destroy(obj);
    }

    internal void SetDrawState(bool v)
    {
        if (v)
        {
            NextState = States.Drawing;
        }
        else
        {
            NextState = States.WaitingForInput;
        }
    }

    private void Win()
    {
        GameWinUI.SetActive(true);
        CurrentState = States.Win;
    }

    public void Lose()
    {
        DrawingPanel _temp = this.transform.GetComponent<DrawingPanel>();
        _temp.DestroyPanel();
        SetDrawState(false);
        GameOverUI.SetUp();
    }

    public void AddPlatform()
    {
        if (LevelData.LevelNumber == 0)
        {
            float lastY = Platforms.Last().transform.position.y;
            float lastX = Platforms.Last().transform.position.x;
            int a = LevelNumber / 2;
            int b = LevelNumber - a;

            if (UnityEngine.Random.Range(0, 2) == 1)
            {
                GameObject plt = Instantiate(platforms[UnityEngine.Random.Range(0, 2)], new Vector2(UnityEngine.Random.Range(0f, 1f), lastY + 1.6f), Quaternion.identity, GameObject.FindWithTag("Environment").transform);
                Platforms.Add(plt);
                Handler.AddEnemies(plt);
                /*if (lastX < 3)
                {
                    Ladders.Add(Instantiate(ladder, new Vector2(0.3f, platformOffsetY - 0.5f + (i - 1) * 1.6f), Quaternion.identity, GameObject.FindWithTag("Environment").transform));
                }*/
            }
            else
            {
                GameObject plt = Instantiate(platforms[UnityEngine.Random.Range(0, 2)], new Vector2(UnityEngine.Random.Range(4.5f, 5.2f), lastY + 1.6f), Quaternion.identity, GameObject.FindWithTag("Environment").transform);
                Platforms.Add(plt);
                Handler.AddEnemies(plt);
            } 
        }
    }
}

public enum States
{
    Drawing,
    WaitingForInput,
    Win,
    Loss
}

enum Sides
{
    Nil,
    Left,
    Right
}