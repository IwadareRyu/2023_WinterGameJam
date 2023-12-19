using System.Collections;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField, Tooltip("巡回ルートを設定するためのポジションの配列")]
    private Transform[] _targetPoss;

    [SerializeField, Tooltip("巡回するかどうか")]
    private bool _isPatrollingGuard = false;

    private Transform _playerPos = null;

    [SerializeField, Tooltip("壁のレイヤー")]
    private LayerMask _wallLayer;

    [SerializeField, Tooltip("プレイヤーの初期スポーン地点")]
    private Transform _playerSpawnPos = null;

    [SerializeField, Tooltip("歩きの速さ")]
    private float _walkSpeed = 5f;

    [SerializeField, Tooltip("走りの速さ")]
    private float _splintSpeed = 5f;

    Rigidbody2D _rb;

    [SerializeField]
    private float _stopDistance = 0.5f;

    private RaycastHit2D _hit;
    private int _currentTargetIndex = 0;
    private bool _isPatrolling = false;
    void Start()
    {
        if (_isPatrollingGuard) _isPatrolling = true;
        if (TryGetComponent(out Rigidbody2D rb)) _rb = rb;
    }

    void Update()
    {
        if (_playerPos)
        {
            Debug.DrawLine(this.transform.position, _playerPos.position, Color.red);
            _hit = Physics2D.Linecast(this.transform.position, _playerPos.position, _wallLayer);
        }
    }

    private void FixedUpdate()
    {
        if (!_isPatrolling && !_hit && _playerPos)
        {
            Chase();
        }
        else if (_isPatrolling)
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        var currentTargetPos = _targetPoss[_currentTargetIndex % _targetPoss.Length].position;
        Vector2 dir = (currentTargetPos - this.transform.position).normalized;
        transform.up = dir;
        _rb.velocity = dir * _walkSpeed;
        var targetDistance = Vector2.Distance(currentTargetPos, this.transform.position);

        if (targetDistance < _stopDistance)
        {
            _currentTargetIndex++;
        }
    }

    private void Chase()
    {
        var dir = (_playerPos.position - this.transform.position).normalized;
        transform.up = dir;
        _rb.velocity = dir * _splintSpeed;
    }

    IEnumerator Return()
    {
        _rb.velocity = Vector2.zero;
        _playerPos = null;
        yield return new WaitForSeconds(1);
        _isPatrolling = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _playerPos = collision.transform;
            _isPatrolling = false;
            Debug.Log("Player Detected");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_isPatrollingGuard) StartCoroutine(Return());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.position = _playerSpawnPos.position;
            _playerPos = null;
        }
    }
}
