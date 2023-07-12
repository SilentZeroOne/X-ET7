﻿using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace ET
{
    [CustomEditor(typeof (EntityBridge))]
    public class EntityBridgeEditor: Editor
    {
        public override void OnInspectorGUI()
        {
            EntityBridge componentView = (EntityBridge) target;
            Entity component = componentView.Entity;
            ComponentViewHelper.Draw(component);
        }

        public static class ComponentViewHelper
        {
            private static readonly List<ITypeDrawer> typeDrawers = new List<ITypeDrawer>();

            static ComponentViewHelper()
            {
                Assembly assembly = typeof (ComponentViewHelper).Assembly;
                foreach (Type type in assembly.GetTypes())
                {
                    if (!type.IsDefined(typeof (TypeDrawerAttribute)))
                    {
                        continue;
                    }

                    ITypeDrawer iTypeDrawer = (ITypeDrawer) Activator.CreateInstance(type);
                    typeDrawers.Add(iTypeDrawer);
                }
            }

            public static void Draw(Entity entity)
            {
                try
                {
                    FieldInfo[] fields = entity.GetType()
                            .GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);

                    EditorGUILayout.BeginVertical();

                    EditorGUILayout.LongField("InstanceId: ", entity.InstanceId);

                    EditorGUILayout.LongField("Id: ", entity.Id);
                    if (entity is GameSweet x)
                    {
                        EditorGUILayout.FloatField("GridX", x.PosInGrid.x);
                        EditorGUILayout.FloatField("GridY", x.PosInGrid.y);
                    }
                    foreach (FieldInfo fieldInfo in fields)
                    {
                        Type type = fieldInfo.FieldType;
                        if (type.IsDefined(typeof (HideInInspector), false))
                        {
                            continue;
                        }

                        if (fieldInfo.IsDefined(typeof (HideInInspector), false))
                        {
                            continue;
                        }

                        object value = fieldInfo.GetValue(entity);

                        foreach (ITypeDrawer typeDrawer in typeDrawers)
                        {
                            if (!typeDrawer.HandlesType(type))
                            {
                                continue;
                            }

                            string fieldName = fieldInfo.Name;
                            if (fieldName.Length > 17 && fieldName.Contains("k__BackingField"))
                            {
                                fieldName = fieldName.Substring(1, fieldName.Length - 17);
                            }

                            value = typeDrawer.DrawAndGetNewValue(type, fieldName, value, null);
                            fieldInfo.SetValue(entity, value);
                            break;
                        }
                    }

                    EditorGUILayout.EndVertical();
                }
                catch (Exception e)
                {
                    UnityEngine.Debug.Log($"component view error: {entity.GetType().FullName} {e}");
                }
            }
        }
    }
}