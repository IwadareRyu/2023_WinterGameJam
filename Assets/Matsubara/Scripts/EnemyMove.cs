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

    [SerializeField, Tooltip("歩行の速さ")]
    private float _walkSpeed = 5f;

    [SerializeField, Tooltip("走りの速さ")]
    private float _sprintSpeed = 5f;

    Rigidbody2D _rb;

    [SerializeField, Tooltip("停止距離")]
    private float _stopDistance = 0.5f;

    private RaycastHit2D _hit;
    private int _currentTargetIndex = 0;
    private bool _isPatrolling = false;

    [SerializeField]
    private StunStateScripts _stunState;

    private bool _isStun = false;

    void Start()
    {
        // 巡回する場合、巡回フラグを有効にする
        if (_isPatrollingGuard) _isPatrolling = true;

        if (TryGetComponent(out Rigidbody2D rb)) _rb = rb;
    }

    void Update()
    {
        if (_playerPos)
        {
            // プレイヤーとの線を描画し、壁に衝突するかどうかを確認
            Debug.DrawLine(this.transform.position, _playerPos.position, Color.red);
            _hit = Physics2D.Linecast(this.transform.position, _playerPos.position, _wallLayer);
        }
        if (_stunState.StunState == StunState.Stun)
        {
            _isStun = true;
        }
        else
        {
            _isStun = false;
        }
    }

    private void FixedUpdate()
    {
        if (_isStun)
        {
            _rb.velocity = Vector3.zero;
            return;
        }
        // プレイヤーを追跡するか、巡回するかを判断
        if (!_isPatrolling && !_hit && _playerPos)
        {
            Chase(); // プレイヤーを追跡
        }
        else if (_isPatrolling)
        {
            Patrol(); // 巡回する
        }
    }

    private void Patrol()
    {
        // 次の目標地点を取得し、方向を計算して移動
        var currentTargetPos = _targetPoss[_currentTargetIndex % _targetPoss.Length].position;
        Vector2 dir = (currentTargetPos - this.transform.position).normalized;
        transform.up = dir;
        _rb.velocity = dir * _walkSpeed;
        var targetDistance = Vector2.Distance(currentTargetPos, this.transform.position);

        // 目標地点に到達したら次の目標地点へ
        if (targetDistance < _stopDistance)
        {
            _currentTargetIndex++;
        }
    }

    private void Chase()
    {
        // プレイヤーを追跡する方向を計算して移動
        var dir = (_playerPos.position - this.transform.position).normalized;
        transform.up = dir;
        _rb.velocity = dir * _sprintSpeed;
    }

    IEnumerator Return()
    {
        // 一定時間後に巡回モードに戻る
        _rb.velocity = Vector2.zero;
        _playerPos = null;
        yield return new WaitForSeconds(1);
        _isPatrolling = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // プレイヤーが検知されたら追跡モードに切り替える
        if (collision.CompareTag("Player"))
        {
            _playerPos = collision.transform;
            _isPatrolling = false;
            Debug.Log("Player Detected");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // 巡回モードの場合、プレイヤーが離れたら一定時間後に巡回モードに戻る
        if (_isPatrollingGuard) StartCoroutine(Return());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // プレイヤーと衝突した場合、プレイヤーを初期スポーン地点に戻す
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.position = _playerSpawnPos.position;
            _playerPos = null;
        }
    }
}
