using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

public class DoorBehaviour : MonoBehaviour
{

    public Sprite OpenedSprite, ClosedSprite;

    private bool Opened => !_collider2D.enabled;
    private BoxCollider2D _collider2D;
    private SpriteRenderer _spriteRenderer;
    private ShadowCaster2D _shadowCaster;

    public void Start(){
        _collider2D = GetComponent<BoxCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _shadowCaster = GetComponent<ShadowCaster2D>();
        //SetOpened(Opened);
    }

    public void SetOpened(bool opened){
        _collider2D.enabled = !opened;
        _shadowCaster.enabled = !opened;
        _spriteRenderer.sprite = opened ? OpenedSprite : ClosedSprite;
    }

    public void ChangeOpen(){
        SetOpened(!Opened);
    }


}


[CustomEditor(typeof(DoorBehaviour)), CanEditMultipleObjects]
public class DoorBehaviourEditor : Editor
{

    private GameObject gameObject;
    private DoorBehaviour _target;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        _target = (DoorBehaviour)target;
        gameObject = _target.gameObject;
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Open/close")){
            _target.Start();
            _target.ChangeOpen();
        }

        if (GUILayout.Button("Flip X")){
            _target.Start();
            var sprite = _target.GetComponent<SpriteRenderer>();
            sprite.flipX = !sprite.flipX;
            var collider = _target.GetComponent<BoxCollider2D>();
            collider.offset = new Vector2(-collider.offset.x,collider.offset.y);
        }



        EditorGUILayout.EndHorizontal();
    }



}
