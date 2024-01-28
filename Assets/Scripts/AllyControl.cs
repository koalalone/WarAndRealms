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
    [SerializeField] private bool _isIdle;
    [SerializeField] private float _range;
    [SerializeField] private float _cost;
    private GameObject _currentEnemy;
    private float _attackTime;

    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        _isIdle = false;
        _isAttacking = false;
        _currentEnemy = null;
        //InvokeRepeating("FindAllies", 0.0f, 1.0f);
        SetEnemyData();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isAttacking && !_isIdle)
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
        //Debug.Log(this.name + " hit " + _currentEnemy.name + " " + Time.time);
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
        _cost = _unitData.cost;
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
            //Debug.Log("D��man g�r�lmedi");
            _currentEnemy = null;
        }


    }
    private void OnTriggerEnter2D(Collider2D unit)
    {
        float distance = this.transform.position.x - unit.transform.position.x;
        if (unit.CompareTag("Ally") && distance <= 0)
        {
            //Debug.Log("anan " + unit.gameObject);
            _isIdle = true;
            anim.SetTrigger("isIdle");
        }
    }

    private void OnTriggerExit2D(Collider2D unit)
    {
        if (unit.CompareTag("Ally"))
        {
            _isIdle = false;
            anim.ResetTrigger("isIdle");
        }
    }
    public float getCost()
    {
        return _cost;
    }

    public Data getData()
    {
        return _unitData;
    }
}
