using System;
using System.Linq;
using System.Reflection;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;



[RequireComponent(typeof(CompositeCollider2D), typeof(Tilemap))]
public class ShadowCaster2DCreator : MonoBehaviour
{

    private int _casterIndex = 0;


    public void CreateShadowCaster(bool selfShadows){
        var collisionMap = GetComponent<Tilemap>();
        var bounds = collisionMap.cellBounds;
        for (int x = bounds.xMin; x < bounds.xMax; x++){
            for (int y = bounds.yMin; y < bounds.yMax; y++){
                var pos = new Vector3Int(x, y);
                if(collisionMap.HasTile(pos))
                    CreateShadowCasterAt(collisionMap.CellToWorld(pos), selfShadows);
            }
        }
    }

    private void CreateShadowCasterAt(Vector3 pos, bool selfShadows){
        var casterObject = new GameObject($"CASTER_{_casterIndex++}");
        casterObject.transform.parent = transform;
        var caster = casterObject.AddComponent<ShadowCaster2D>();
        caster.SetShapePath(new []
        {
            pos,
            pos + Vector3.right,
            pos + Vector3.right + Vector3.up,
            pos + Vector3.up
        });
        caster.selfShadows = selfShadows;
    }

}