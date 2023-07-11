using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class playerSavePosition : EditorWindow
{
    [MenuItem("debug/player Save Position")]
    public static void ShowExample()
    {
        playerSavePosition wnd = GetWindow<playerSavePosition>();
        wnd.titleContent = new GUIContent("player Save Position");
    }

    private void OnGUI()
    {

        if (PlayerPrefs.HasKey("PlayerX"))
        {
            float PlayerX = PlayerPrefs.GetFloat("PlayerX");
            float PlayerY = PlayerPrefs.GetFloat("PlayerY");
            float PlayerZ = PlayerPrefs.GetFloat("PlayerZ");

            float tempx = EditorGUILayout.FloatField("X : ", PlayerX);
            float tempy = EditorGUILayout.FloatField("Y : ", PlayerY);
            float tempz = EditorGUILayout.FloatField("Z : ", PlayerZ);

            if (tempx != PlayerX) { PlayerPrefs.SetFloat("PlayerX", tempx); PlayerPrefs.Save(); }
            if (tempy != PlayerY) { PlayerPrefs.SetFloat("PlayerY", tempy); PlayerPrefs.Save(); }
            if (tempz != PlayerZ) { PlayerPrefs.SetFloat("PlayerZ", tempz); PlayerPrefs.Save(); }

            if (GUILayout.Button("reset"))
            {
                PlayerPrefs.DeleteKey("PlayerX");
                PlayerPrefs.DeleteKey("PlayerY");
                PlayerPrefs.DeleteKey("PlayerZ");
                PlayerPrefs.Save();
            }

        }
        else
        {
            EditorGUILayout.LabelField("no player posision data");
        }


        int PlayerStep = PlayerPrefs.GetInt("globalStep", 0);
        float tempStep = EditorGUILayout.IntField("step : ", PlayerStep);
        if (tempStep != PlayerStep) { PlayerPrefs.SetFloat("globalStep", tempStep); PlayerPrefs.Save(); }


    }

}
