using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Utility.VectorC
{
    public static class VectorUtility
    {
        public static Vector2 Vector2Half()
        {
            return new Vector2(0.5f, 0.5f);
        }

        public static Vector3 Vector3ToVector3_xy0(this Vector3 vector)
        {
            return new Vector3(vector.x, vector.y, 0);
        }

        public static Vector3 Vector2ToVector3_xy0(this Vector2 vector)
        {
            return new Vector3(vector.x, vector.y, 0);
        }

        public static Vector2 Vector3ToVector2(this Vector3 vector)
        {
            return new Vector2(vector.x, vector.y);
        }

        public static Vector2 Vector2ToFloor(this Vector2 vector)
        {
            return new Vector2(MathF.Floor(vector.x), MathF.Floor(vector.y));
        }

        public static Vector2 GetProperlyDirection(this Vector2 vector)
        {
            if (vector == Vector2.zero) return Vector2.zero;

            if (vector.y > 0) return Vector2.up;
            if (vector.y < 0) return Vector2.down;
            if (vector.x < 0) return Vector2.left;
            if (vector.x > 0) return Vector2.right;

            return Vector2.zero;
        }

        public static Vector2 GetNearestOrFarthestVector2(Vector2 startPoint, List<Vector2> vectors, bool closest)
        {
            if (vectors == null || vectors.Count == 0)
                return Vector2.zero;

            Vector2 result = vectors[0];
            float minDistance = Vector2.Distance(startPoint, vectors[0]);

            foreach (Vector2 vector in vectors)
            {
                float distance = Vector2.Distance(startPoint, vector);

                if (closest && distance < minDistance || !closest && distance > minDistance)
                {
                    result = vector;
                    minDistance = distance;
                }
            }

            return result;
        }

        public static Transform GetNearestOrFarthestTransform(Vector2 startPoint, List<Transform> transforms, bool closest)
        {
            if (transforms == null || transforms.Count == 0)
                return null;

            Transform result = transforms[0];
            float minDistance = Vector2.Distance(startPoint, transforms[0].position);

            foreach (Transform transform in transforms)
            {
                float distance = Vector2.Distance(startPoint, transform.position);

                if (closest && distance < minDistance || !closest && distance > minDistance)
                {
                    result = transform;
                    minDistance = distance;
                }
            }

            return result;
        }

        public static List<Vector3> ConvertVector2ListToVector3List(this List<Vector2> list)
        {
            if (list == null) return null;

            List<Vector3> vector3List = new List<Vector3>();

            foreach (Vector2 vector2 in list)
            {
                Vector3 vector3 = new Vector3(vector2.x, vector2.y, 0f);
                vector3List.Add(vector3);
            }

            return vector3List;
        }
    }
}