using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs {

    private Vector2 _mousePos;
    private float _movHor;
    private float _movVer;
    private bool _fire;
    private bool _reload;
    private bool _dash;


    public float MovHor { get => _movHor; }
    public float MovVer { get => _movVer; }
    public Vector2 MousePos { get => _mousePos; }
    public bool Fire { get => _fire; }
    public bool Reload { get => _reload; }
    public bool Dash { get => _dash; set => _dash = value; }

    public void ArtificialUpdate() {
        _mousePos = Input.mousePosition;
        _movHor = Input.GetAxis("Horizontal");
        _movVer = Input.GetAxis("Vertical");
        _fire = Input.GetButtonDown("Fire1");
        _dash = Input.GetButtonDown("Jump");
        _reload = Input.GetButtonDown("Reload");
    }
}
