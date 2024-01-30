using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyBase : MonoBehaviour
{
    [SerializeField] private float _hp;
    public GameObject[] prefab;
    private Vector3 _allySpawnPosition;
    private UIManager _uiManager;

    private float spawnRadius = 0.5f;
    private float _food = 4f;
    private float _foodRate = 0.5f;
    private float _foodProductionTime = 0.5f;

    private Queue<GameObject> allyQueue = new Queue<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _allySpawnPosition = new Vector3(-9, 1.75f, 0);
        _hp = 10;
        InvokeRepeating("AddFood", 0, _foodProductionTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (allyQueue.Count > 0)
        {
            SpawnFromQueue();
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
            Spawn(0);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            Spawn(1);
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            Spawn(2);
        
    }

    public void Damage(float damage)
    {
        _hp -= damage;
        if (_hp <= 0)
        {
            _uiManager.GameOver("Defeat");
            Destroy(gameObject);
        }
    }

    private void Spawn(int allyId)
    {
        Debug.Log("Queue Count: " + allyQueue.Count);
        float cost = prefab[allyId].GetComponent<AllyControl>().getData().cost;
        if (_food < cost)
        {
            return;
        } 
        
        bool canSpawn = checkSpace();
        Debug.Log("dada " + canSpawn);
        if (!canSpawn)
        {
            if (allyQueue.Count < 3)
            {
                allyQueue.Enqueue(prefab[allyId]);
            }
        }
        else
        {
            if (allyQueue.Count > 0 && allyQueue.Count < 3)
            {
                allyQueue.Enqueue(prefab[allyId]);
            }
            else
            {
                Instantiate(prefab[allyId], _allySpawnPosition, Quaternion.identity);
            }
            
        }

        Debug.Log(prefab[allyId].name + " " + cost + " yedi");
        _food -= cost;
        _uiManager.UpdateFood(_food);
    }

    private void SpawnFromQueue()
    {
        bool canSpawn = checkSpace();
        if (canSpawn)
        {
            Instantiate(allyQueue.Dequeue(), _allySpawnPosition, Quaternion.identity);
        }
    }
    private void AddFood()
    {
        _food += _foodRate;
        _uiManager.UpdateFood(_food);
    }

    private bool checkSpace()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_allySpawnPosition, spawnRadius);

        if (colliders.Length > 1)
        {
            return false;
        }
        return true;
    }
}
