using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ProjectileStats))]
public class ProjectileStatsDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        var prefab = property.FindPropertyRelative("<Prefab>k__BackingField");
        var isSplashAttack = property.FindPropertyRelative("<IsSplashAttack>k__BackingField");
        var splashRadius = property.FindPropertyRelative("<SplashRadius>k__BackingField");
        var speed = property.FindPropertyRelative("<Speed>k__BackingField");

        EditorGUILayout.PropertyField(prefab);
        EditorGUILayout.PropertyField(isSplashAttack);

        if ((bool)isSplashAttack.boolValue == true)
        {
            EditorGUILayout.PropertyField(splashRadius);
        }

        EditorGUILayout.PropertyField(speed);


        EditorGUI.EndProperty();
    }

}
