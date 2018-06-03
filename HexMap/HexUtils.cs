using System;
using Shared;
using UnityEngine;

namespace GridMap
{
    public static class HexUtils
    {
        private const float RADIUS_RATIO = 0.866025404f;

        public static float GetInnerRadius(float outerRadius)
        {
            return outerRadius * RADIUS_RATIO;
        }

        public static Vector3 GetHorizontalPosition(HexCoordinates hexCoordinates, float outerRadius)
        {
            return GetHorizontalPosition(hexCoordinates.MapCoordinates, outerRadius);
        }

        public static Vector3 GetHorizontalPosition(IntVector2 coordinates, float outerRadius)
        {
            return GetHorizontalPosition(coordinates.X, coordinates.Y, outerRadius);
        }

        public static Vector3 GetHorizontalPosition(int x, int z, float outerRadius)
        {
            var innerRadius = GetInnerRadius(outerRadius);
            Vector3 position;
            position.x = (x + z * 0.5f - z / 2) * (innerRadius * 2f);
            position.y = 0f;
            position.z = z * (outerRadius * 1.5f);
            return position;
        }

        public static Vector3 GetVerticalPosition(HexCoordinates hexCoordinates, float outerRadius)
        {
            return GetVerticalPosition(hexCoordinates.MapCoordinates, outerRadius);
        }

        public static Vector3 GetVerticalPosition(IntVector2 coordinates, float outerRadius)
        {
            return GetVerticalPosition(coordinates.X, coordinates.Y, outerRadius);
        }

        public static Vector3 GetVerticalPosition(int x, int y, float outerRadius)
        {
            var innerRadius = GetInnerRadius(outerRadius);
            Vector3 position;
            position.x = (x + y * 0.5f - y / 2) * (innerRadius * 2f);
            position.y = y * (outerRadius * 1.5f);
            position.z = 0f;
            return position;
        }

        public static Vector3[] GetCorners(float outerRadius)
        {
            float innerRadius = GetInnerRadius(outerRadius);
            return new Vector3[] {
                new Vector3(0f, 0f, outerRadius),
                new Vector3(innerRadius, 0f, 0.5f * outerRadius),
                new Vector3(innerRadius, 0f, -0.5f * outerRadius),
                new Vector3(0f, 0f, -outerRadius),
                new Vector3(-innerRadius, 0f, -0.5f * outerRadius),
                new Vector3(-innerRadius, 0f, 0.5f * outerRadius),
                new Vector3(0f, 0f, outerRadius)
            };
        }
    }
}
