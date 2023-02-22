using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bullet : MonoBehaviour, IPausable {
    public float currentSpeed;
    public float damage = 3;
    string key;
    float timer;
    public float destroyTime = 2f;
    private Vector3 direction;
    Action<string, Bullet> notify;

    public void Configure(string _key, Action<string, Bullet> notify) {
        key = _key;
        ScreenManager.instance.AddPausable(this);
        this.notify = notify;
    }
    public void Move(Vector3 pos, Vector3 dir) {
        transform.position = pos;
        direction = dir.normalized;
    }
    bool move;
    public virtual void Prender() {
        move = true;
    }
    public virtual void Apagar() {
        move = false;
    }

    protected virtual void Update() {
        if (!move) return;
        transform.position = transform.position + direction * currentSpeed * Time.deltaTime;
        timer = timer + 1 * Time.deltaTime;
        if (timer >= destroyTime) {
            TimeCompleted();
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other) {
        if (other.GetComponentInParent<IDamageable>() != null) {
            other.GetComponentInParent<IDamageable>().TakeDamage((int)damage);
        }
        notify.Invoke(key, this);
    }

    protected virtual void TimeCompleted() {
        timer = 0;
        notify.Invoke(key, this);
    }

    public void Pause() {
        move = false;
    }

    public void Resume() {
        move = true;
    }
}
