using Shared;
using UnityEngine;

namespace GridMap
{
    public abstract class AbstractMapEntity : MonoBehaviour, IMapEntity
    {
        public IntVector2 Coordinates { get; protected set; }
        public IGridMap GridMap { get; set; }

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

        public void SetLocation(IntVector2 coordinates)
        {
            Coordinates = coordinates;
            transform.position = GridMap.GetPosition(coordinates);
        }

        public abstract void MoveTo(IntVector2 coordinates);
    }
}

