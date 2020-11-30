using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAni : MonoBehaviour
{
    [SerializeField] Animator animator;
    private void Start()
    {
        if (animator == null)
            animator = this.transform.GetComponent<Animator>();
        animator.Play("StartAni");
        Time.timeScale = 1f;
    }
}
