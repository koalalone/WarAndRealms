using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyBase : MonoBehaviour
{
    [SerializeField] private float _hp;
    public GameObject[] prefab;
    private Vector3 _allySpawnPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        _allySpawnPosition = new Vector3(-9, 1.75f, 0);
        _hp = 10;
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

        if (Input.GetKeyDown(KeyCode.A))
        {
            Instantiate(prefab[0], _allySpawnPosition, Quaternion.identity);
        }
        
    }
}
