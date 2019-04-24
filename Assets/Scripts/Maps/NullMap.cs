using RCG.Attributes;
using UnityEngine;

namespace RCG.Maps
{
    public class NullMap : IMap
    {
        Vector3Int IMap.LocalToCell(Vector3 localPosition) { return Vector3Int.zero; }
        Vector3 IMap.CellToLocal(Vector3Int cellPosition) { return Vector3.zero; }

        void IMap.AddElement(IMapElement element) { }
        void IMap.RemoveElement(IMapElement element) { }

        string IDescribable.DisplayName { get; }
        string IDescribable.Description { get; }

        public static IMap Create()
        {
            return new NullMap();
        }
    }
}

