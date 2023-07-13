using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class Movement : MonoBehaviour
{
    
    public float Speed = 4f;

    private Rigidbody2D body;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        var collider = GetComponent<BoxCollider2D>();

        foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy")){
            Debug.Log(enemy.name);
            var enemyBody = enemy.GetComponent<BoxCollider2D>();
            if(enemyBody != null)
                Physics2D.IgnoreCollision(collider, enemyBody);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float dx = Input.GetAxis("Horizontal");
        float dy = Input.GetAxis("Vertical");

        float diagonal = 1f;
        if (dx != 0 && dy != 0)
            diagonal = 0.707f; // 1 / sqrt(2)

        body.velocity = new Vector3(dx, dy) * Speed * diagonal;
        animator.SetFloat("Speed", body.velocity.magnitude);
        if(body.velocity.x != 0)
            spriteRenderer.flipX = body.velocity.x < 0;
    }
}
