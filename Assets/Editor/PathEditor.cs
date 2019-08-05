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
            Input();
            Draw();
        }

        private void Input()
        {
            var guiEvent = Event.current;
            if (guiEvent.type != EventType.MouseDown || guiEvent.button != 0 || !guiEvent.shift) return;
            var mousePosition = HandleUtility.GUIPointToWorldRay(guiEvent.mousePosition).origin;
            Undo.RecordObject(_pathCreator, "Add point");
            _path.AddPoint(mousePosition);
        }

        private void Draw()
        {
            for (var i = 0; i < _path.TotalSegments; i++)
            {
                var points = _path.GetPointsInSegment(i);
                Handles.color = Color.black;
                Handles.DrawLine(points[1], points[0]);
                Handles.DrawLine(points[2], points[3]);
                Handles.DrawBezier(
                    points[0],
                    points[3],
                    points[1],
                    points[2],
                    Color.green,
                    null,
                    2.0f
                );
            }

            Handles.color = Color.red;
            for (var i = 0; i < _path.TotalPoints; i++)
            {
                Vector2 newPosition = Handles.FreeMoveHandle(_path[i], Quaternion.identity, 0.1f, Vector2.zero,
                    Handles.CylinderHandleCap);
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