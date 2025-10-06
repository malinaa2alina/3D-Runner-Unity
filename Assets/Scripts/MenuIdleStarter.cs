using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class MenuIdleStarter : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.enabled = true;
            animator.Play("Idle"); // название твоей анимации
        }
    }
}
