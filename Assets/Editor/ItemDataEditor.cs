using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ItemData))]
public class ItemDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        ItemData itemData = (ItemData)target;

        // 항상 표시되는 Info 부분
        DrawPropertiesExcluding(serializedObject, "isStackable", "maxStackCount", "consumables", "boosts");

        EditorGUILayout.Space();

        // ItemType에 따른 Stacking Info 처리
        if (itemData.type == ItemType.Equippable)
        {
            itemData.isStackable = false; // 자동으로 false로 설정
        }
        else
        {
            itemData.isStackable = true; // 자동으로 true로 설정
        }

        // Stackable 필드 표시
        SerializedProperty stackableProp = serializedObject.FindProperty("isStackable");
        EditorGUILayout.PropertyField(stackableProp);

        // Stackable이 true인 경우 maxStackCount 표시
        if (itemData.isStackable)
        {
            SerializedProperty maxStackProp = serializedObject.FindProperty("maxStackCount");
            EditorGUILayout.PropertyField(maxStackProp, new GUIContent("Max Stack Count"));
        }



        EditorGUILayout.Space();

        // Consumable 타입일 때만 Consumables 배열 표시
        if (itemData.type == ItemType.Consumable)
        {
            SerializedProperty consumablesProp = serializedObject.FindProperty("consumables");
            EditorGUILayout.PropertyField(consumablesProp, true);

            // Boost 타입의 Consumable 개수 계산
            int boostCount = 0;
            for (int i = 0; i < consumablesProp.arraySize; i++)
            {
                SerializedProperty consumable = consumablesProp.GetArrayElementAtIndex(i);
                SerializedProperty typeProp = consumable.FindPropertyRelative("type");

                if ((ConsumableType)typeProp.enumValueIndex == ConsumableType.Boost)
                {
                    boostCount++;
                }
            }

            // Boosts 배열 표시 (Boost 타입 개수만큼 제한)
            if (boostCount > 0)
            {
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Boosting Info", EditorStyles.boldLabel);

                SerializedProperty boostsProp = serializedObject.FindProperty("boosts");

                // Boosts 배열 크기 제한
                if (boostsProp.arraySize != boostCount)
                {
                    boostsProp.arraySize = boostCount;
                }

                EditorGUILayout.PropertyField(boostsProp, true);
            }
        }

        serializedObject.ApplyModifiedProperties();
    }
}
