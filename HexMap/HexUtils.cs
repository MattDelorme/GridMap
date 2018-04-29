using System;
using System.Collections.Generic;
using Shared;
using Shared.Unity.Math;
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

        public static List<HexCoordinates> GetLine(HexCoordinates start, HexCoordinates end)
        {
            var lineList = new List<HexCoordinates>();
            lineList.Add(start);

            Vector3 startPosition = GetVerticalPosition(start, 1);
            Vector3 endPosition = GetVerticalPosition(end, 1);
            if (Mathf.Approximately(startPosition.x, endPosition.x))
            {
                // Avoiding NaN slope
                endPosition.x += 0.0001f;
            }
            Line line = new Line(startPosition, endPosition);

            List<HexCoordinates> neighbours;
            var visited = new HashSet<HexCoordinates>();
            float smallestDelta;
            float currentDistance;

            HexCoordinates current = start;
            HexCoordinates closestNeighbour = default(HexCoordinates);
            int test = 0;
            while (current != end && test < 50)
            {
                test++;
                currentDistance = Vector3.Distance(GetVerticalPosition(current, 1), endPosition);
                neighbours = current.GetNeighbours();
                smallestDelta = float.MaxValue;
                for (int i = 0; i < neighbours.Count; i++)
                {
                    if (!visited.Contains(neighbours[i]))
                    {
                        Vector3 position = GetVerticalPosition(neighbours[i], 1);

                        float delta = LineUtils.GetDistanceFromLine(position, line);
                        float distance = Vector3.Distance(position, endPosition);
                        if (distance < currentDistance && delta < smallestDelta)
                        {
                            smallestDelta = delta;
                            closestNeighbour = neighbours[i];
                        }
                        visited.Add(neighbours[i]);
                    }
                }

                current = closestNeighbour;
                lineList.Add(closestNeighbour);
            }

            return lineList;
        }
    }
}
