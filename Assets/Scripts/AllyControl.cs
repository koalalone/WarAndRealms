using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyControl : MonoBehaviour
{
    [SerializeField] private float _hp;
    [SerializeField] private float _damage;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private string _race;
    [SerializeField] private Data _unitData;
    [SerializeField] private bool _isAttacking;
    [SerializeField] private float _range;
    private GameObject _currentEnemy;
    private float _attackTime;

    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        _isAttacking = false;
        _currentEnemy = null;
        //InvokeRepeating("FindAllies", 0.0f, 1.0f);
        SetEnemyData();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isAttacking)
        {
            FindEnemies();
            MoveRight();
        }

        else
        {
            if (!_currentEnemy) 
            {
                _isAttacking = false;
                anim.ResetTrigger("isAttacking");
            }
            else if (Time.time > _attackTime) 
            {
                Attack();
            }
            else
            {

            }
            
        }
    }

    private void MoveRight()
    {
        transform.position += new Vector3(_moveSpeed * Time.deltaTime, 0, 0);
    }

    private void Attack()
    {
        anim.SetTrigger("isAttacking");
        _attackTime = Time.time + _attackSpeed;
        if (_currentEnemy.CompareTag("Enemy"))
            _currentEnemy.GetComponent<EnemyControl>().Damage(_damage);
        else
            _currentEnemy.GetComponent<EnemyBase>().Damage(_damage);
        Debug.Log("Axeman Cyan hit " + _currentEnemy.name + " " + Time.time);
    }

    public void Damage(float damage)
    {
        _hp -= damage;
        if (_hp <= 0)
        {   
            Debug.Log("dead1");
            Destroy(gameObject);
        }
    }
    private void SetEnemyData()
    {
        _hp = _unitData.hp;
        _damage = _unitData.damage;
        _moveSpeed = _unitData.moveSpeed;
        _attackSpeed = _unitData.attackSpeed;
        _race = _unitData.race;
        _range = _unitData.range;
    }

    private void FindEnemies()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, _range);

        float nearestDistance = Mathf.Infinity;
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy") || hitCollider.CompareTag("EnemyBase"))
            {
                float distanceX = Mathf.Abs(transform.position.x - hitCollider.transform.position.x);
                if (distanceX < nearestDistance)
                {
                    nearestDistance = distanceX;
                    _currentEnemy = hitCollider.transform.gameObject;
                }
            }




        }
        if (_currentEnemy != null)
        {
            //Debug.Log("Nearest enemy: " + _currentEnemy.gameObject.name);
            _isAttacking = true;
        }
        else
        {
            //Debug.Log("Düþman görülmedi");
            _currentEnemy = null;
        }

    }
}
