using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    [SerializeField] private float _hp;
    [SerializeField] private float _damage;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _attackSpeed;

    [SerializeField] private Data _unitData;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("FindAllies", 0.0f, 1.0f);
        SetEnemyData();
    }

    // Update is called once per frame
    void Update()
    {
        
        MoveLeft();
    }

    private void MoveLeft()
    {
        transform.position += new Vector3(-1 * _moveSpeed * Time.deltaTime, 0, 0); 
    }
    private void SetEnemyData()
    {
        _hp = _unitData.hp;
        _damage = _unitData.damage;
        _moveSpeed = _unitData.moveSpeed;
        _attackSpeed = _unitData.attackSpeed;

    }

    private void FindAllies()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 1.0f);
        
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Ally"))
            {
               Debug.Log("dasudjadasd");
            }
           
            
        }

        
    }
}
