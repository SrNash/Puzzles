using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(InteractionPC))]
public class PC_AiInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Label("Script de Di�logos de la AI asistente");
    }
}
