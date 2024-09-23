using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            print("Split it");
            animator.SetBool("Split", true);
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Simple Splitting") && 
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            animator.SetBool("Split", false);
        }
    }

}
