using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ItemData))]
public class ItemDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        ItemData itemData = (ItemData)target;

        // �׻� ǥ�õǴ� Info �κ�
        DrawPropertiesExcluding(serializedObject, "isStackable", "maxStackCount", "consumables", "boosts");

        EditorGUILayout.Space();

        // ItemType�� ���� Stacking Info ó��
        if (itemData.type == ItemType.Equippable)
        {
            itemData.isStackable = false; // �ڵ����� false�� ����
        }
        else
        {
            itemData.isStackable = true; // �ڵ����� true�� ����
        }

        // Stackable �ʵ� ǥ��
        SerializedProperty stackableProp = serializedObject.FindProperty("isStackable");
        EditorGUILayout.PropertyField(stackableProp);

        // Stackable�� true�� ��� maxStackCount ǥ��
        if (itemData.isStackable)
        {
            SerializedProperty maxStackProp = serializedObject.FindProperty("maxStackCount");
            EditorGUILayout.PropertyField(maxStackProp, new GUIContent("Max Stack Count"));
        }



        EditorGUILayout.Space();

        // Consumable Ÿ���� ���� Consumables �迭 ǥ��
        if (itemData.type == ItemType.Consumable)
        {
            SerializedProperty consumablesProp = serializedObject.FindProperty("consumables");
            EditorGUILayout.PropertyField(consumablesProp, true);

            // Boost Ÿ���� Consumable ���� ���
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

            // Boosts �迭 ǥ�� (Boost Ÿ�� ������ŭ ����)
            if (boostCount > 0)
            {
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Boosting Info", EditorStyles.boldLabel);

                SerializedProperty boostsProp = serializedObject.FindProperty("boosts");

                // Boosts �迭 ũ�� ����
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
