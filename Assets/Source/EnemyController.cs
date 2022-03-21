using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    protected AppController appController;
    [SerializeField]
    protected EnemyObject enemyPref;
    [SerializeField]
    protected int enemyCount = 5;

    private List<EnemyObject> enemies;
    private Vector2 prefSize;

    private void Start()
    {
        prefSize = enemyPref.gameObject.GetComponent<SpriteRenderer>().size;

        enemies = new List<EnemyObject>();
        GenerateObj();
        StartCoroutine(Generator());
    }

    private IEnumerator Generator()
    {
        while (true)
        {
            int wait_time = Random.Range(5, 15);
            yield return new WaitForSeconds(wait_time);
            if (enemies.Count < enemyCount)
                GenerateObj();
        }
    }

    private void GenerateObj()
    {
        float spawnY = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y + prefSize.y / 2, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y - prefSize.y / 2);
        float spawnX = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x + prefSize.x / 2, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x - prefSize.x / 2);

        Vector2 spawnPosition = new Vector2(spawnX, spawnY);
        EnemyObject go = Instantiate(enemyPref, this.transform);
        go.transform.position = spawnPosition;
        go.OnDestroying += DestroyCurEnemy;
        enemies.Add(go);
    }

    private void DestroyCurEnemy(EnemyObject obj)
    {
        enemies.Remove(obj);
        Destroy(obj.gameObject);
        appController.UpdateScore();
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
