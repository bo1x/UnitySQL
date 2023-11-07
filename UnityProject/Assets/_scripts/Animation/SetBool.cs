using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBool : MonoBehaviour
{
    [SerializeField] Animator _Animator;

    public void SetHighlight(bool value)
    {
        _Animator.SetBool("Highlighted", value);
    }
}
