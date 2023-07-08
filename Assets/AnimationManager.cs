using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEditor.Animations;

public class AnimationManager : MonoBehaviour
{

    public float SeuilMarche = 1f;
    public float SeuilCourse = 3f;

    private Rigidbody2D body;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = spriteRenderer.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.flipX = body.velocity.x < 0;
        animator.SetFloat("Speed", body.velocity.magnitude);
        animator.SetFloat("SeuilMarche", SeuilMarche);
        animator.SetFloat("SeuilCourse", SeuilCourse);
    }
}
