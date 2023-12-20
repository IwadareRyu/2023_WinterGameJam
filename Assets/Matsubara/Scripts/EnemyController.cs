using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField, Tooltip("巡回ルートを設定するためのポジションの配列")]
    private Transform[] _targetPoss;

    [SerializeField, Tooltip("巡回するかどうか")]
    private bool _isPatrollingGuard = false;

    [SerializeField, Tooltip("壁のレイヤー")]
    private LayerMask _wallLayer;

    [SerializeField, Tooltip("プレイヤーの初期スポーン地点")]
    private Transform _playerSpawnPos = null;

    [SerializeField, Tooltip("歩行の速さ")]
    private float _walkSpeed = 5f;

    [SerializeField, Tooltip("走りの速さ")]
    private float _sprintSpeed = 7f;

    [SerializeField]
    private StunStateScripts _stunState;

    [SerializeField, Tooltip("停止距離")]
    private float _stopDistance = 0.5f;

    [SerializeField]
    private float _returnTime = 2f;

    private RaycastHit2D _hit;
    private int _currentTargetIndex = 0;
    private bool _isStun = false;
    Rigidbody2D _rb;
    private Vector2 _defaultPos = default;
    private Transform _playerPos = null;
    private Coroutine _nowCoroutine;
    private bool _isTimerEnd = false;


    void Start()
    {
        DreamStateScripts.DreamWorld += OnDreamWorld;
        DreamStateScripts.DreamWorldEnd += OnDreamWorldExit;

        if (TryGetComponent(out Rigidbody2D rb)) _rb = rb;

        _defaultPos = transform.position;

        if (_isPatrollingGuard) _nowCoroutine = StartCoroutine(Patrol());
        else _nowCoroutine = StartCoroutine(Search());

        StartCoroutine(DrawRay());
    }

    IEnumerator DrawRay()
    {
        while (true)
        {
            if (_playerPos)
            {
                // プレイヤーとの線を描画し、壁に衝突するかどうかを確認
                Debug.DrawLine(this.transform.position, _playerPos.position, Color.red);
                _hit = Physics2D.Linecast(this.transform.position, _playerPos.position, _wallLayer);
            }
            if (_stunState.StunState == StunState.Stun)
            {
                _rb.velocity = Vector3.zero;
            }

            yield return null;
        }
    }
    /// <summary>巡回関数</summary>
    IEnumerator Patrol()
    {
        if (_targetPoss.Length <= 0) yield break;

        while (!_isStun)
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

            yield return new WaitForFixedUpdate();
        }
    }

    /// <summary>追跡関数</summary>
    IEnumerator Chase()
    {
        while (!_isStun && _playerPos)
        {
            // プレイヤーを追跡する方向を計算して移動
            var dir = (_playerPos.position - this.transform.position).normalized;
            transform.up = dir;
            _rb.velocity = dir * _sprintSpeed;
            yield return new WaitForFixedUpdate();
        }

    }

    /// <summary>プレイヤーを見失ったときの処理を行う関数</summary>
    IEnumerator Return()
    {
        _rb.velocity = Vector2.zero;
        _playerPos = null;
        yield return new WaitForSeconds(1);
        // 一定時間後に巡回モードに戻る
        if (_isPatrollingGuard)
        {
            StopCoroutine(_nowCoroutine);
            _nowCoroutine = StartCoroutine(Patrol());
            yield break;
        }
        else
        {
            StartCoroutine(Timer());

            while (!_isTimerEnd)
            {
                var dir = _defaultPos;
                transform.up = dir;
                _rb.velocity = dir * _walkSpeed;
                var dis = Vector2.Distance(_defaultPos, this.transform.position);

                if (dis < _stopDistance)
                {
                    _rb.velocity = Vector2.zero;
                }
                yield return new WaitForFixedUpdate();
            }

            this.transform.position = _defaultPos;
            yield break;
        }
    }

    IEnumerator Search()
    {
        var z = 0;
        while (true)
        {
            this.transform.localEulerAngles = new Vector3(0, 0, z);

            yield return null;
        }
    }

    IEnumerator Timer()
    {
        var timer = 0f;
        while (_returnTime > timer)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        _isTimerEnd = true;
    }

    private void OnDreamWorld()
    {
        GetComponentInChildren<Collider2D>().enabled = false;
    }

    private void OnDreamWorldExit()
    {
        GetComponentInChildren<Collider2D>().enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // プレイヤーが検知されたら追跡モードに切り替える
        if (collision.CompareTag("Player"))
        {
            _playerPos = collision.transform;
            if (_nowCoroutine != null) StopCoroutine(_nowCoroutine);
            _nowCoroutine = StartCoroutine(Chase());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // 巡回モードの場合、プレイヤーが離れたら一定時間後に巡回モードに戻る
        if (collision.gameObject.CompareTag("Player"))
        {
            StopCoroutine(_nowCoroutine);
            _nowCoroutine = StartCoroutine(Return());
        }
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
