using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRoom : MonoBehaviour
{
    [Header("Walls")]
    public GameObject[] walls;

    [Header("Enemies")]
    public GameObject[] enemyTypes;
    public Transform[] enemySpawners;

    [Header("Powerups")]
    public Transform[] _bonusSpawners;
    public GameObject[] _bonusTypes;

    [HideInInspector] public List<GameObject> enemies;

    private RoomVariants roomVariants;
    private bool spawned;
    private bool wallsDestroyed;

    private void Start()
    {
        roomVariants = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomVariants>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !spawned)
        {
            spawned = true;

            foreach (var spawner in _bonusSpawners)
            {
                GameObject bonusType = _bonusTypes[Random.Range(0, _bonusTypes.Length)];
                GameObject bonus = Instantiate(bonusType, spawner.position, Quaternion.identity);
                bonus.transform.parent = transform;
            }

            foreach (var spawner in enemySpawners)
            {
                //int rand = Random.Range();
                GameObject enemyType = enemyTypes[Random.Range(0, enemyTypes.Length)];
                GameObject enemy = Instantiate(enemyType, spawner.position, Quaternion.identity);
                enemy.transform.parent = transform;
                enemies.Add(enemy);
            }
            StartCoroutine(CheckEnemies());
        }
    }
    IEnumerator CheckEnemies()
    {
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => enemies.Count == 0);
        DestroyWalls();
    }
    public void DestroyWalls()
    {
        foreach (var wall in walls)
        {
            if (wall != null && wall.transform.childCount != 0)
            {
                Destroy(wall);
            }
        }
        wallsDestroyed = true;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (wallsDestroyed && collision.CompareTag("Wall"))
        {
            Destroy(collision.gameObject);
        }
    }
}
