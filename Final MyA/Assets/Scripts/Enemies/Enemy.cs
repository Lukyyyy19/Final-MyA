using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Enemy : Entity {

    [SerializeField]
    protected Transform _target;
    protected float _currentSpeed;
    protected Vector2 _velocity;
    [SerializeField]
    protected float viewRange;

    protected float radiusSeparation = 5.3f;
    [SerializeField]
    protected Vector2 steering;

    protected float dist;

    [SerializeField]
    protected ParticleSystem particleDead;
    [SerializeField]
    protected ParticleSystem particleAppear;

    protected bool _isSpawning;

    string key;
    Action<string, Enemy> notify;

    protected override void Start() {
        base.Start();
        radiusSeparation = 5.3f;
        GameManager.instance.enemies.Add(this);
        _target = PlayerManager.instance.transform;
        _currentSpeed = _speed;
    }
    public virtual void Configure(string _key, Action<string, Enemy> notify) {
        key = _key;
        this.notify = notify;
    }
    protected virtual void Update() {
        UpdateHandPos();
    }


    public IEnumerator OnAppear() {
        ScreenManager.instance.AddPausable(this);
        _isSpawning = true;
        sr.enabled = false;
        _hand.gameObject.SetActive(false);
        _rb.isKinematic = true;
        // gameObject.SetActive(false);
        var ps = Instantiate(particleAppear, transform.position, Quaternion.identity);
        var pm = ps.main;
        pm.startColor = sr.color;
        yield return new WaitUntil(() => ps.isStopped);
        Destroy(ps.gameObject);
        sr.enabled = true;
        _hand.gameObject.SetActive(true);
        _rb.isKinematic = false;
        _isSpawning = false;
        // gameObject.SetActive(true);
        _health = _maxHealth;
    }

    protected void UpdateHandPos() {
        var _dir = _target.position - _hand.position;
        float angle = Mathf.Atan2(_dir.y, _dir.x) * Mathf.Rad2Deg;
        _hand.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
    }

    protected Vector2 Arrive(Vector3 actualTarget) {
        Vector2 desired = actualTarget - transform.position;
        dist = desired.magnitude;
        desired.Normalize();
        if (dist <= viewRange) {
            desired *= _currentSpeed * (dist / viewRange);
        } else {
            desired *= _currentSpeed;
        }

        steering = desired;

        return steering;
    }

    protected Vector3 Separation() {
        Vector3 desired = Vector3.zero;

        foreach (var item in GameManager.instance.enemies) {
            Vector3 dist = item.transform.position - transform.position;

            if (dist.magnitude <= radiusSeparation)
                desired += dist;
        }

        if (desired == Vector3.zero)
            return desired;

        desired = -desired;

        desired.Normalize();
        desired *= _speed;

        return desired;
    }

    protected override void Die() {
        base.Die();
        sr.enabled = false;
        _rb.isKinematic = true;
        ResetColor();
        var currParticle = Instantiate(particleDead, transform.position, Quaternion.identity);
        var pm = currParticle.main;
        pm.startColor = sr.color;
        PlayerManager.instance.EnemyKill();
        GameManager.instance.RemoveEnemyFormHash(this);
        notify.Invoke(key, this);
    }
    public void Reactivate() {
        sr.enabled = true;
        _rb.isKinematic = false;
        ScreenManager.instance.AddPausable(this);
    }

    public void SetInitalPos(Vector2 position) {
        transform.position = position;
    }

    private void OnEnable() {
        ScreenManager.instance.AddPausable(this);
    }


}
