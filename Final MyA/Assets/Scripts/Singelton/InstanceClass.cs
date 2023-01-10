using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InstanceClass<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T instance;
    protected virtual void Awake() => InstanceClass<T>.instance = (this as T);   


}
