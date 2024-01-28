using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] private float _hp;
    public GameObject[] prefab;
    private Vector3 _enemySpawnPosition;
    private bool _isQueued;
    private Queue<GameObject> enemyQueue = new Queue<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        _enemySpawnPosition = new Vector3(9, 1.75f, 0);
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
            Debug.Log("Game Over");
            Destroy(gameObject);
        }
    }

    private void Spawn()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            Instantiate(prefab[0], _enemySpawnPosition, Quaternion.identity);
        }
    }

    private IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(1f);
        
        while (!_isQueued)
        {
            int randomIndex = Random.Range(0, prefab.Length);
            GameObject newEnemy = Instantiate(prefab[randomIndex], _enemySpawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(3f);
        }

        while(_isQueued)
        {

        }
        //newEnemy.transform.parent = _enemyContainer.transform;
        //yield return new WaitForSeconds(1f);


    }
}
