using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class Platform : MonoBehaviour
{
    private void Awake()
    {
        SetLayer();
    }

    protected abstract void SetLayer();
}
