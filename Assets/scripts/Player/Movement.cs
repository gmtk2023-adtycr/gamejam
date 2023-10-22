using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{

    public float Speed = 4f;
    private float originSpeed;

    private Rigidbody2D body;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private static readonly int Speed1 = Animator.StringToHash("Speed");
    private static readonly int VerticalDirection = Animator.StringToHash("VerticalDirection");


    [SerializeField] private AudioSource myAudioSource;
    [SerializeField] private AudioClip DeathSound;
    [SerializeField] private float volume = 1.0f;
    private bool deathSoundPlayed = false;
    public GameObject DeathGameObject;

    // Start is called before the first frame update
    void Start(){
        originSpeed = Speed;
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        var collider = GetComponent<BoxCollider2D>();

        foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy")){
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

        body.velocity = new Vector2(dx, dy) * (Speed * diagonal);
        animator.SetFloat(Speed1, body.velocity.magnitude);
        if(body.velocity.x != 0)
            spriteRenderer.flipX = body.velocity.x < 0;
        animator.SetInteger(VerticalDirection, Math.Abs(body.velocity.x) > 0 ? 0 : body.velocity.y > 0 ? 1 : -1);
    }

    public void Die(){
        enabled = false;
        body.velocity = Vector2.zero;
        animator.SetBool("Dead", true);
        if(!deathSoundPlayed){
        myAudioSource.PlayOneShot(DeathSound, volume);
        deathSoundPlayed=true;        }
        //Invoke(nameof(GoToGameOverScene), 1f);
        DeathGameObject.SetActive(true);
        GetComponent<BoxCollider2D>().enabled = false;

    }


    public void Slow(){
        Speed = originSpeed * .4f;
        Invoke(nameof(ResetSpeed), 3f);
    }

    private void ResetSpeed(){
        Speed = originSpeed;
    }

}
