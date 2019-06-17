﻿using System.Collections.Generic;
using UnityEngine;

namespace RCG.Utils
{
    public static class GridUtil
    {
        public static HashSet<Vector2Int> GetAllCells(Vector2Int gridSize)
        {
            Vector2Int centerOffset = Vector2Int.zero;
            centerOffset.x -= gridSize.x / 2;
            centerOffset.y -= gridSize.y / 2;

            HashSet<Vector2Int> cells = new HashSet<Vector2Int>();
            for(int x = 0; x < gridSize.x; x++)
            {
                for(int y = 0; y < gridSize.y; y++)
                {
                    cells.Add(CreateCell(x + centerOffset.x, y + centerOffset.y));
                }
            }
            return cells;
        }

        public static HashSet<Vector2Int> GetCellsOutsideRadius(Vector2Int gridSize, Vector2Int centerCell, int radius, bool isFilled)
        {
            HashSet<Vector2Int> gridCells = GetAllCells(gridSize);
            HashSet<Vector2Int> circleCells = GetCellsInsideRadius(gridSize, centerCell, radius, isFilled);
            gridCells.ExceptWith(circleCells);
            return gridCells;
        }

        public static HashSet<Vector2Int> GetCellsInsideRadius(Vector2Int gridSize, Vector2Int centerCell, int radius, bool isFilled)
        {       
            Vector2Int offsetCenter = centerCell;

            offsetCenter.x += gridSize.x / 2;
            offsetCenter.y += gridSize.y / 2;

            HashSet<Vector2Int> cells = new HashSet<Vector2Int>();

            int d = (5 - radius * 4) / 4;
            int x = 0;
            int y = radius;

            do
            {
                if (isFilled)
                {
                    for (int ty = y; ty > 0; ty--)
                    {
                        AddCircleCellsToHashSet(cells, gridSize, centerCell, offsetCenter, x, ty);
                    }
                }
                else
                {
                    AddCircleCellsToHashSet(cells, gridSize, centerCell, offsetCenter, x, y);
                }

                if (d < 0)
                {
                    d += 2 * x + 1;
                }
                else
                {
                    d += 2 * (x - y) + 1;
                    y--;
                }
                x++;
            } while (x <= y);

            if (isFilled)
            {
                if (offsetCenter.x >= 0 && offsetCenter.x <= gridSize.x - 1 && offsetCenter.y >= 0 && offsetCenter.y <= gridSize.y - 1) cells.Add(CreateCell(centerCell.x, centerCell.y));
            }

            return cells;
        }

        static void AddCircleCellsToHashSet(HashSet<Vector2Int> cells, Vector2Int mapSize, Vector2Int center, Vector2Int offsetCenter, int x, int y)
        {
            if (offsetCenter.x + x >= 0 && offsetCenter.x + x <= mapSize.x - 1 && offsetCenter.y + y >= 0 && offsetCenter.y + y <= mapSize.y - 1) cells.Add(CreateCell(center.x + x, center.y + y));
            if (offsetCenter.x + x >= 0 && offsetCenter.x + x <= mapSize.x - 1 && offsetCenter.y - y >= 0 && offsetCenter.y - y <= mapSize.y - 1) cells.Add(CreateCell(center.x + x, center.y - y));
            if (offsetCenter.x - x >= 0 && offsetCenter.x - x <= mapSize.x - 1 && offsetCenter.y + y >= 0 && offsetCenter.y + y <= mapSize.y - 1) cells.Add(CreateCell(center.x - x, center.y + y));
            if (offsetCenter.x - x >= 0 && offsetCenter.x - x <= mapSize.x - 1 && offsetCenter.y - y >= 0 && offsetCenter.y - y <= mapSize.y - 1) cells.Add(CreateCell(center.x - x, center.y - y));
            if (offsetCenter.x + y >= 0 && offsetCenter.x + y <= mapSize.x - 1 && offsetCenter.y + x >= 0 && offsetCenter.y + x <= mapSize.y - 1) cells.Add(CreateCell(center.x + y, center.y + x));
            if (offsetCenter.x + y >= 0 && offsetCenter.x + y <= mapSize.x - 1 && offsetCenter.y - x >= 0 && offsetCenter.y - x <= mapSize.y - 1) cells.Add(CreateCell(center.x + y, center.y - x));
            if (offsetCenter.x - y >= 0 && offsetCenter.x - y <= mapSize.x - 1 && offsetCenter.y + x >= 0 && offsetCenter.y + x <= mapSize.y - 1) cells.Add(CreateCell(center.x - y, center.y + x));
            if (offsetCenter.x - y >= 0 && offsetCenter.x - y <= mapSize.x - 1 && offsetCenter.y - x >= 0 && offsetCenter.y - x <= mapSize.y - 1) cells.Add(CreateCell(center.x - y, center.y - x));
        }

        static Vector2Int CreateCell(int x, int y)
        {
            return new Vector2Int(x, y);
        }
    }
}
