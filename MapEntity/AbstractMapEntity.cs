using System;
using System.Collections.Generic;
using Shared;
using UnityEngine;

namespace GridMap
{
    public abstract class AbstractMapEntity : MonoBehaviour, IMapEntity
    {
        public event Action<IntVector2> CoordinatesSet;
        public event Action<List<IntVector2>> MovementPathSet;

        private float scale;
        public float Scale
        {
            get
            {
                return scale;
            }
            set
            {
                scale = value;
                transform.localScale = new Vector3(scale, scale, scale);
            }
        }

        public IGridMap GridMap { get; set; }

        private IntVector2 coordinates;
        public IntVector2 Coordinates
        {
            get
            {
                return coordinates;
            }
            set
            {
                coordinates = value;
                CoordinatesSet.Dispatch(coordinates);
            }
        }

        private List<IntVector2> movementPath;
        public List<IntVector2> MovementPath
        {
            get
            {
                return movementPath;
            }
            set
            {
                movementPath = value;
                MovementPathSet.Dispatch(movementPath);
            }
        }

        public T Get<T>()
        {
            return GetComponent<T>();
        }
    }
}
