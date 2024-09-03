using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class EntityHandler : MonoBehaviour
{
    int EnemyToKill = 1;
    int enemyID = 1;
    public GameObject character;
    public GameObject[] enemyTypes;
    public GameObject boss;

    GameObject player;
    public Dictionary<int, GameObject> enemies = new Dictionary<int, GameObject>();

    public Dictionary<int, GameObject> generateEnemies(int level, List<GameObject> platforms)
    {
        int enemyCount;
        foreach (GameObject platform in platforms)
        {
            bool left = platform.transform.position.x > 2f ? false : true;
            float lengthInPlay;
            if (left)
            {
                lengthInPlay = 4.8f / 2 + platform.transform.position.x;
            }
            else
            {
                lengthInPlay = 4.8f / 2 + (5.7f - platform.transform.position.x);
            }

            if (level == 1)
            {
                enemyCount = 1;
            }
            else if (level < 5)
            {
                if (lengthInPlay > 4) { enemyCount = 2; }
                else if (lengthInPlay > 2) { enemyCount = 1; }
                else { enemyCount = 1; }
            }
            else
            {
                if (lengthInPlay > 3.5) { enemyCount = 2; }
                else if (lengthInPlay > 2) { enemyCount = 1; }
                else { enemyCount = 1; }
            }

            if (left)
            {
                for (int i = 1; i <= enemyCount; i++)
                {
                    GameObject enemy = GameObject.Instantiate(enemyTypes[UnityEngine.Random.Range(0, enemyTypes.Count())],
                        new Vector2(platform.transform.position.x + (enemyCount > 2 ? 2.2f : 1.6f) - 0.8f * i,
                                    platform.transform.position.y + 0.7f), Quaternion.identity);
                    enemy.transform.parent = transform;
                    enemy.GetComponent<Rigidbody2D>().freezeRotation = true;
                    enemy.AddComponent<Enemy>();
                    enemies.Add(enemyID, enemy);
                    enemyID++;
                }
            }
            else
            {
                for (int i = 1; i <= enemyCount; i++)
                {
                    GameObject enemy = GameObject.Instantiate(enemyTypes[UnityEngine.Random.Range(0, enemyTypes.Count())],
                        new Vector2(platform.transform.position.x - 1 + 0.8f * i,
                                    platform.transform.position.y + 0.7f), Quaternion.identity);
                    enemy.transform.parent = transform;
                    enemy.GetComponent<Rigidbody2D>().freezeRotation = true;
                    enemy.AddComponent<Enemy>();
                    enemies.Add(enemyID, enemy);
                    enemyID++;
                }
            }
        }
        try
        {
            enemies[1].transform.GetComponent<Enemy>().clickable = true;
        }
        catch {}
        return enemies;
    }

    public GameObject generatePlayer()
    {
        player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            GameObject plr = GameObject.Instantiate(character, new Vector2(1, 7), Quaternion.identity);
            plr.AddComponent<Player>();
            plr.layer = 5;
            plr.tag = "Player";
            player = plr;
            return plr;
        }
        else
        {
            return player;
        }
    }

    public void killEnemy(int killcount)
    {
        if (enemies.Count > 0)
        {
            if (enemies.Count <= killcount)
            {
                killcount = enemies.Count;
            }
        }
        for(int i = 0; i < killcount; i++)
        {
            enemies[i + EnemyToKill].transform.GetComponent<Enemy>().Die();
            player.GetComponent<Player>().Attack();
            enemies.Remove(i + EnemyToKill);
            EnemyToKill++;
            try
            {
                enemies[i + EnemyToKill].transform.GetComponent<Enemy>().clickable = true;
            }
            catch
            {
                Debug.Log("No more enemies");
            }
        }
    }

    public void EnemyAttack()
    {
        enemies[EnemyToKill].transform.GetComponent<Enemy>().Attack();
    }

    public void takeDamage(int damage)
    {
        player.GetComponent<Player>().TakeDamage(damage);
    }

    internal void clickEnemy()
    {
        try
        {
            enemies[EnemyToKill].GetComponent<InputHandlerEnemy>().click();
        }
        catch { }
    }

    internal Dictionary<int, GameObject> GenerateBoss()
    {
        int enemyID = 1;
        GameObject enemy = GameObject.Instantiate(boss, new Vector2(4, 7), Quaternion.identity);
        enemy.transform.parent = transform;
        enemy.AddComponent<Enemy>();
        enemies.Add(enemyID, enemy);
        enemyID++;

        try
        {
            enemies[1].transform.GetComponent<Enemy>().clickable = true;
        }
        catch { }

        return enemies;
    }

    internal void AddEnemies(GameObject platform)
    {
        int enemyCount;
        bool left = platform.transform.position.x > 2f ? false : true;
        float lengthInPlay;
        if (left)
        {
            lengthInPlay = 4.8f / 2 + platform.transform.position.x;
        }
        else
        {
            lengthInPlay = 4.8f / 2 + (5.7f - platform.transform.position.x);
        }
        if (lengthInPlay > 3.5) { enemyCount = 2; }
        else if (lengthInPlay > 2) { enemyCount = 1; }
        else { enemyCount = 1; }

        if (left)
        {
            for (int i = 1; i <= enemyCount; i++)
            {
                GameObject enemy = GameObject.Instantiate(enemyTypes[UnityEngine.Random.Range(0, enemyTypes.Count())],
                    new Vector2(platform.transform.position.x + (enemyCount > 2 ? 2.2f : 1.6f) - 0.8f * i,
                                platform.transform.position.y + 0.7f), Quaternion.identity);
                enemy.transform.parent = transform;
                enemy.GetComponent<Rigidbody2D>().freezeRotation = true;
                enemy.AddComponent<Enemy>();
                Physics2D.IgnoreCollision(player.GetComponent<BoxCollider2D>(), enemy.GetComponent<BoxCollider2D>());
                enemies.Add(enemyID, enemy);
                enemyID++;
            }
        }
        else
        {
            for (int i = 1; i <= enemyCount; i++)
            {
                GameObject enemy = GameObject.Instantiate(enemyTypes[UnityEngine.Random.Range(0, enemyTypes.Count())],
                    new Vector2(platform.transform.position.x - 1 + 0.8f * i,
                                platform.transform.position.y + 0.7f), Quaternion.identity);
                enemy.transform.parent = transform;
                enemy.GetComponent<Rigidbody2D>().freezeRotation = true;
                enemy.AddComponent<Enemy>();
                Physics2D.IgnoreCollision(player.GetComponent<BoxCollider2D>(), enemy.GetComponent<BoxCollider2D>());
                enemies.Add(enemyID, enemy);
                enemyID++;
            }
        }
    }
}
