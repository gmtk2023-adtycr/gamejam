using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{

    public TypeChasseur TypeChasseur;

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
        animator.SetInteger("TypeChasseur", (int)TypeChasseur);
        spriteRenderer.flipX = body.velocity.x < 0;
        animator.SetFloat("Speed", body.velocity.magnitude);
       
    }
}

public enum TypeChasseur
{
    Un = 1,
    Deux = 2,
    Trois = 3,
    Quatre = 4
}