using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private Rigidbody2D _rb;



    [SerializeField]
    private Vector2 _currentMousePos;

    private Vector2 _keyDirection;

    private Vector2 _dir;

    [SerializeField]
    private float _speed;

    void Start() {
        _rb = GetComponent<Rigidbody2D>();

    }


    void Update() {


    }



    private void FixedUpdate() {

    }
}
