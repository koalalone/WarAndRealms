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
            FindAllies();
            MoveLeft();
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
    private void Attack()
    {
        
        anim.SetTrigger("isAttacking");
        _attackTime = Time.time + _attackSpeed;
        if (_currentEnemy.CompareTag("Ally"))
            _currentEnemy.GetComponent<AllyControl>().Damage(_damage);
        else
            _currentEnemy.GetComponent<AllyBase>().Damage(_damage);


    }

    public void Damage(float damage)
    {
        _hp -= damage;
        if (_hp <= 0)
        {
            Debug.Log("dead2");
            Destroy(gameObject);
        }
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
        _race = _unitData.race;
        _range = _unitData.range;
    }

    private void FindAllies()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, _range);

        float nearestDistance = Mathf.Infinity;
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Ally") || hitCollider.CompareTag("AllyBase"))
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
            //Debug.Log("Nearest ally: " + _currentEnemy.gameObject.name);
            _isAttacking = true;
        }
        else
        {
            //Debug.Log("Düþman görülmedi");
            _currentEnemy = null;
        }

    }
}
