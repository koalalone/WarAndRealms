using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyBase : MonoBehaviour
{
    [SerializeField] private float _hp;
    public GameObject[] prefab;
    private Vector3 _allySpawnPosition;
    private UIManager _uiManager;
    
    private float _food = 4f;
    private float _foodRate = 0.2f;
    private float _foodProductionTime = 0.5f;
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
        Spawn();
    }

    public void Damage(float damage)
    {
        _hp -= damage;
        if (_hp <= 0)
        {
            _uiManager.GameOver("Loser");
            Destroy(gameObject);
        }
    }

    private void Spawn()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Instantiate(prefab[0], _allySpawnPosition, Quaternion.identity);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Instantiate(prefab[1], _allySpawnPosition, Quaternion.identity);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Instantiate(prefab[2], _allySpawnPosition, Quaternion.identity);
        }
        //_manager.UpdateScore(-2);
    }

    private void AddFood()
    {
        _food += _foodRate;
        _uiManager.UpdateScore(_food);
    }
}
