using RCG.Attributes;
using UnityEngine;

namespace RCG.Maps
{
    public interface IMap : IDescribable
    {
        Vector3Int LocalToCell(Vector3 localPosition);
        Vector3 CellToLocal(Vector3Int cellPosition);

        void AddElement(IMapElement element);
        void RemoveElement(IMapElement element);
    }
}
