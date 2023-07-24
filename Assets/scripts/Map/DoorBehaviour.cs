using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{

    public Sprite OpenedSprite, ClosedSprite;
    
    private bool _opened = false;
    private BoxCollider2D _collider2D;
    private SpriteRenderer _spriteRenderer;

    public void Start(){
        _collider2D = GetComponent<BoxCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetOpened(bool opened){
        _opened = opened;
        _collider2D.enabled = !opened;
        _spriteRenderer.sprite = opened ? OpenedSprite : ClosedSprite;
    }

    public void ChangeOpen(){
        SetOpened(!_opened);
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
        


        EditorGUILayout.EndHorizontal();
    }
    
    

}

