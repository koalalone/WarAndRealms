using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyBase : MonoBehaviour
{

    [SerializeField] private float _hp;
    public GameObject[] prefab;
    private Vector2 _enemySpawnPosition;
    private float spawnRadius = 0.5f;
    private float food = 4;
    private float foodRate = 0.2f;
    private Queue<GameObject> enemyQueue = new Queue<GameObject>();

    private UIManager _uiManager;
    // Start is called before the first frame update
    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _enemySpawnPosition = new Vector2(9, 1.75f);
        _hp = 10;
        StartCoroutine(SpawnEnemyRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        Spawn();
    }

    public void Damage(float damage)
    {
        _hp -= damage;
        if (_hp <= 0) 
        {
            _uiManager.GameOver("Victory");
            Destroy(gameObject);
        }
    }

    private void Spawn()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Instantiate(prefab[0], _enemySpawnPosition, Quaternion.identity);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Instantiate(prefab[1], _enemySpawnPosition, Quaternion.identity);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Instantiate(prefab[2], _enemySpawnPosition, Quaternion.identity);
        }
    }

    private IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(1f);

        
        while (true)
        {
            float luck = Random.value;
            bool canSpawn = checkSpace();
            //Debug.Log(canSpawn);
            if (enemyQueue.Count < 3)
            {
                int index;
                if (luck >= 0.5f)
                {
                    index = 0;
                }
                else if(luck >= 0.2f)
                {
                    index = 1;
                }
                else
                {
                    index = 2;
                }
                enemyQueue.Enqueue(prefab[index]);
            }
            
            if (canSpawn)
            {
                Instantiate(enemyQueue.Dequeue(), _enemySpawnPosition, Quaternion.identity);
            }
            
            yield return new WaitForSeconds(5f);
        }

        //newEnemy.transform.parent = _enemyContainer.transform;
        //yield return new WaitForSeconds(1f);


    }
    private bool checkSpace()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_enemySpawnPosition, spawnRadius);
        
        if (colliders.Length > 1)
        {
            return false;
        }
        return true;
    }
}
