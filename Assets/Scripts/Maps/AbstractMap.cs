using System.Collections.Generic;
using UnityEngine;

namespace RCG.Maps
{
    public abstract class AbstractMap : MonoBehaviour, IMap
    {
        public virtual Vector3Int LocalToCell(Vector3 localPosition) { return Vector3Int.zero; }
        public virtual Vector3 CellToLocal(Vector3Int cellPosition) { return Vector3.zero; }

        public virtual void AddElement(IMapElement element) { }
        public virtual void RemoveElement(IMapElement element) { }
        public virtual List<IMapElement> GetMapElementsAtCell(Vector3Int cell) { return new List<IMapElement>(); }

        public virtual string DisplayName { get; }
        public virtual string Description { get; }
    }
}
