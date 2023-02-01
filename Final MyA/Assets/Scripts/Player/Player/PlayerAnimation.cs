using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {
    private Animator _anim;

    private void Awake() {
        _anim = GetComponent<Animator>();
    }
    public void SetBoolParam(string boolName, bool boolParam) {
        _anim.SetBool(boolName, boolParam);
    }
}
