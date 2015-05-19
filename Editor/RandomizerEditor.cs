﻿// Copyright (c) 2015 Bartłomiej Wołk (bartlomiejwolk@gmail.com)
//  
// This file is part of the Randomizer extension for Unity.
// Licensed under the MIT license. See LICENSE file in the project root folder.

using UnityEngine;
using System.Collections;
using UnityEditor;
using OneDayGame;

namespace Randomizer {

    [CustomEditor(typeof (Randomizer))]
    public class RandomizerEditor : Editor {

        #region SERIALIZED PROPERTIES

        private SerializedProperty initDelay;
        private SerializedProperty interval;
        private SerializedProperty intervalType;
        private SerializedProperty minInterval;
        private SerializedProperty maxInterval;

        #endregion

        #region UNITY MESSAGES

        private void OnEnable() {
            initDelay = serializedObject.FindProperty("initDelay");
            interval = serializedObject.FindProperty("interval");
            intervalType = serializedObject.FindProperty("intervalType");
            minInterval = serializedObject.FindProperty("minInterval");
            maxInterval = serializedObject.FindProperty("maxInterval");
        }

        public override void OnInspectorGUI() {
            serializedObject.Update();

            EditorGUILayout.BeginHorizontal();

            EditorGUIUtility.labelWidth = 70;

            DrawInitDelayField();

            EditorGUIUtility.labelWidth = 40;

            DrawIntervalTypeDropdown();

            EditorGUILayout.EndHorizontal();

            HandleIntervalTypeOption();

            serializedObject.ApplyModifiedProperties();
        }

        #endregion

        #region INSPECTOR METHODS

        private void HandleIntervalTypeOption() {
            switch (intervalType.enumValueIndex) {
                case (int) IntervalTypes.Fixed:
                    DrawFixedIntervalTypeFields();
                    break;
                case (int) IntervalTypes.Random:
                    DrawRandomIntervalTypeFields();
                    break;
            }
        }

        private void DrawFixedIntervalTypeFields() {
            EditorGUIUtility.labelWidth = 0;
            EditorGUILayout.PropertyField(
                interval,
                new GUIContent(
                    "Interval",
                    "How often to change state (in seconds)."));
        }

        private void DrawRandomIntervalTypeFields() {
            EditorGUILayout.BeginHorizontal();

            EditorGUIUtility.labelWidth = 80;

            EditorGUILayout.PropertyField(
                minInterval,
                new GUIContent(
                    "Min. Interval",
                    "Minimum interval before changing state."));

            EditorGUILayout.PropertyField(
                maxInterval,
                new GUIContent(
                    "Max. Interval",
                    "Maximum interval before changing state."));

            EditorGUIUtility.labelWidth = 0;

            EditorGUILayout.EndHorizontal();
        }

        private void DrawIntervalTypeDropdown() {
            EditorGUILayout.PropertyField(
                intervalType,
                new GUIContent("Type", "Type of the interval."));
        }

        private void DrawInitDelayField() {
            EditorGUILayout.PropertyField(
                initDelay,
                new GUIContent(
                    "Init. Delay",
                    "Initial delay applied after entering into play mode."),
                GUILayout.MaxWidth(120));
        }

        #endregion

        #region METHODS

        [MenuItem("Component/Randomizer")]
        private static void AddRandomizerComponent() {
            if (Selection.activeGameObject != null) {
                Selection.activeGameObject.AddComponent(
                    typeof(Randomizer));
            }
        }

        #endregion

    }

}
