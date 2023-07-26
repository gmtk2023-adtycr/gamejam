using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public static class ShadowCaster2DExtension
{
    static readonly FieldInfo meshField = typeof(ShadowCaster2D).GetField("m_Mesh", BindingFlags.NonPublic | BindingFlags.Instance);
    static readonly FieldInfo shapePathField = typeof(ShadowCaster2D).GetField("m_ShapePath", BindingFlags.NonPublic | BindingFlags.Instance);
    static readonly FieldInfo shapePathHashField = typeof(ShadowCaster2D).GetField("m_ShapePathHash", BindingFlags.NonPublic | BindingFlags.Instance);
    static readonly MethodInfo generateShadowMeshMethod = typeof(ShadowCaster2D)
        .Assembly
        .GetType("UnityEngine.Rendering.Universal.ShadowUtility")
        .GetMethod("GenerateShadowMesh", BindingFlags.Public | BindingFlags.Static);

    public static void SetShapePath(this ShadowCaster2D shadowCasterComponent, Vector3[] path){
        shapePathField.SetValue(shadowCasterComponent, path);
        shapePathHashField.SetValue(shadowCasterComponent, Random.Range(int.MinValue, int.MaxValue));
        meshField.SetValue(shadowCasterComponent, new Mesh());
        generateShadowMeshMethod.Invoke(shadowCasterComponent,
            new object[] { meshField.GetValue(shadowCasterComponent), shapePathField.GetValue(shadowCasterComponent) });
    }
}