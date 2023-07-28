using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{

    public TypeChasseur TypeChasseur;

    private Rigidbody2D _body;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    
    private static readonly int ChasseurProperty = Animator.StringToHash("TypeChasseur");
    private static readonly int SpeedProperty = Animator.StringToHash("Speed");

    private void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = _spriteRenderer.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _animator.SetInteger(ChasseurProperty, (int)TypeChasseur);
        _spriteRenderer.flipX = _body.velocity.x < 0;
        _animator.SetFloat(SpeedProperty, _body.velocity.magnitude);
       
    }
}

public enum TypeChasseur
{
    Un = 1,
    Deux = 2,
    Trois = 3,
    Quatre = 4
}