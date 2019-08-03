using System;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(PathCreator))]
    public class PathEditor : UnityEditor.Editor
    {
        private PathCreator _pathCreator;

        private Path _path;

        private void OnSceneGUI()
        {
            Draw();
        }

        private void Draw()
        {
            Handles.color = Color.red;
            for (var i = 0; i < _path.TotalPoints; i++)
            {
                Vector2 newPosition = Handles.FreeMoveHandle(_path[i], Quaternion.identity, 0.1f, Vector2.zero, Handles.CylinderHandleCap);
                if (_path[i] == newPosition) continue;
                Undo.RecordObject(_pathCreator, "Move point");
                _path.MovePoint(i, newPosition);
            }
        }

        private void OnEnable()
        {
            _pathCreator = (PathCreator) target;
            if (_pathCreator.path == null)
            {
                _pathCreator.CreatePath();
            }

            _path = _pathCreator.path;
        }
    }
}