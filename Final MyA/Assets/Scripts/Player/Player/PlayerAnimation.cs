using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {
    private Animator _anim;
    private AnimatorStateInfo _currAnim;
    private void Awake() {
        _anim = GetComponent<Animator>();
    }
    public void SetBoolParam(string boolName, bool boolParam) {
        _anim.SetBool(boolName, boolParam);
    }
    public void StopAnimation() {
        _anim.speed = 0;
    }
    public void ResumeAnimation() {
        _anim.speed = 1;
    }
}
