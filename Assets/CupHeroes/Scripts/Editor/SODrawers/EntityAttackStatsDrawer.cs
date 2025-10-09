using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(EntityAttackStats))]
public class EntityAttackStatsDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Обновляем объект сериализации
        property.serializedObject.Update();
        EditorGUI.BeginProperty(position, label, property);

        // Найти подконтрольные поля (у тебя могут отличаться имена backing fields)
        var baseAmountTargets = property.FindPropertyRelative("<BaseAmountTargetsForAttack>k__BackingField");
        var canAttackMultipleTargets = property.FindPropertyRelative("<CanAttackMultipleTargets>k__BackingField");
        var baseDamage = property.FindPropertyRelative("<BaseDamage>k__BackingField");
        var attackSpeed = property.FindPropertyRelative("<BaseAttackSpeedProcent>k__BackingField");
        var baseMeleeAttackRange = property.FindPropertyRelative("<BaseMeleeAttackRange>k__BackingField");
        var baseRangeAttackRange = property.FindPropertyRelative("<BaseRangeAttackRange>k__BackingField");
        var baseAttackType = property.FindPropertyRelative("<BaseAttackType>k__BackingField");
        var availableAttackTypes = property.FindPropertyRelative("<AvailableAttackTypes>k__BackingField");
        var targetLayer = property.FindPropertyRelative("<TargetLayer>k__BackingField");
        var attackClip = property.FindPropertyRelative("<AttackClip>k__BackingField");
        var projectileType = property.FindPropertyRelative("<AvailableProjectileType>k__BackingField");

        // Рисуем простые поля
        EditorGUILayout.PropertyField(canAttackMultipleTargets);

        if ((bool)canAttackMultipleTargets.boolValue == true)
        {
            EditorGUILayout.PropertyField(baseAmountTargets);
        }

        EditorGUILayout.PropertyField(baseDamage);
        EditorGUILayout.PropertyField(attackSpeed);
        EditorGUILayout.PropertyField(baseAttackType);

        // Рисуем поле флагового enum-а и применяем изменения сразу, чтобы intValue был актуален
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(availableAttackTypes);

        if (EditorGUI.EndChangeCheck())
        {
            property.serializedObject.ApplyModifiedProperties(); // немедленное применение
            property.serializedObject.Update(); // и обновить сериализованный объект
        }

        // Теперь читаем актуальный mask
        int mask = availableAttackTypes.intValue;
        bool isEverything = (mask & (int)EntityAttackType.Everything) == (int)EntityAttackType.Everything;
        bool isRange = (mask & (int)EntityAttackType.Range) != 0;
        bool isMelee = (mask & (int)EntityAttackType.Melee) != 0;

        // Приоритет: Everything -> иначе рисуем то, что включено
        if (isEverything)
        {
            EditorGUILayout.PropertyField(baseMeleeAttackRange);
            EditorGUILayout.PropertyField(baseRangeAttackRange);
        }
        else
        {
            if (isRange)
                EditorGUILayout.PropertyField(baseRangeAttackRange);

            if (isMelee)
                EditorGUILayout.PropertyField(baseMeleeAttackRange);
        }

        // Рисуем остальные поля
        EditorGUILayout.PropertyField(targetLayer);
        EditorGUILayout.PropertyField(attackClip);

        // ProjectileStats показываем если Range или Everything
        if (isRange || isEverything)
        {
            EditorGUILayout.PropertyField(projectileType);
        }

        // Применяем все изменения и завершаем
        property.serializedObject.ApplyModifiedProperties();
        EditorGUI.EndProperty();
    }
}
