using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RCG.Maps;

namespace RCG.Demo.FloppyKnights
{
    public class RadiusMarker : MonoBehaviour
    {
        [SerializeField]
        int radius;
        public int Radius
        {
            get
            {
                return Mathf.Max(0, radius);
            }
            set
            {
                if (radius != value && value >= 0)
                {
                    radius = value;
                    Draw();
                }        
            }
        }

        [SerializeField]
        Color color = Color.white;
        Color MarkerColor
        {
            get
            {
                return color;
            }
            set
            {
                if (color != value)
                {
                    color = value;
                    Recolor();
                }
            }
        }

        [SerializeField]
        int orderInLayer = 1;

        [SerializeField]
        SpriteRenderer cellPrefab;

        IMap map;

        void Awake()
        {
            Init();
        }

        void Init()
        {
            SetMap();
            Draw();
        }

        void SetMap()
        {
            map = GetComponentInParent<IMap>();
        }

        void Draw()
        {
            Clear();
            Vector3Int mapSize = map.Size;

            Vector3Int center = map.LocalToCell(transform.localPosition);

            Vector3Int offsetCenter = center;
            offsetCenter.x += mapSize.x / 2;
            offsetCenter.y += mapSize.y / 2;

            HashSet<Vector3Int> cells = new HashSet<Vector3Int>();

            int d = (5 - Radius * 4) / 4;
            int x = 0;
            int y = Radius;

            do
            {
                if (offsetCenter.x + x >= 0 && offsetCenter.x + x <= mapSize.x - 1 && offsetCenter.y + y >= 0 && offsetCenter.y + y <= mapSize.y - 1) cells.Add(new Vector3Int(center.x + x, center.y + y, 0));
                if (offsetCenter.x + x >= 0 && offsetCenter.x + x <= mapSize.x - 1 && offsetCenter.y - y >= 0 && offsetCenter.y - y <= mapSize.y - 1) cells.Add(new Vector3Int(center.x + x, center.y - y, 0));
                if (offsetCenter.x - x >= 0 && offsetCenter.x - x <= mapSize.x - 1 && offsetCenter.y + y >= 0 && offsetCenter.y + y <= mapSize.y - 1) cells.Add(new Vector3Int(center.x - x, center.y + y, 0));
                if (offsetCenter.x - x >= 0 && offsetCenter.x - x <= mapSize.x - 1 && offsetCenter.y - y >= 0 && offsetCenter.y - y <= mapSize.y - 1) cells.Add(new Vector3Int(center.x - x, center.y - y, 0));
                if (offsetCenter.x + y >= 0 && offsetCenter.x + y <= mapSize.x - 1 && offsetCenter.y + x >= 0 && offsetCenter.y + x <= mapSize.y - 1) cells.Add(new Vector3Int(center.x + y, center.y + x, 0));
                if (offsetCenter.x + y >= 0 && offsetCenter.x + y <= mapSize.x - 1 && offsetCenter.y - x >= 0 && offsetCenter.y - x <= mapSize.y - 1) cells.Add(new Vector3Int(center.x + y, center.y - x, 0));
                if (offsetCenter.x - y >= 0 && offsetCenter.x - y <= mapSize.x - 1 && offsetCenter.y + x >= 0 && offsetCenter.y + x <= mapSize.y - 1) cells.Add(new Vector3Int(center.x - y, center.y + x, 0));
                if (offsetCenter.x - y >= 0 && offsetCenter.x - y <= mapSize.x - 1 && offsetCenter.y - x >= 0 && offsetCenter.y - x <= mapSize.y - 1) cells.Add(new Vector3Int(center.x - y, center.y - x, 0));
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

            foreach(Vector3Int cell in cells)
            {      
                Vector3 cellPosition = map.CellToLocal(cell);
                cellPosition.x -= transform.localPosition.x;
                cellPosition.y -= transform.localPosition.y;

                SpriteRenderer cellSpriteRenderer = Instantiate(cellPrefab, transform);
                cellSpriteRenderer.transform.localPosition = cellPosition;
                cellSpriteRenderer.color = MarkerColor;
                cellSpriteRenderer.sortingOrder = orderInLayer;
            }
        }

        void Clear()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }

        void Recolor()
        {
            foreach (Transform child in transform)
            {
                SpriteRenderer spriteRenderer = child.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    spriteRenderer.color = MarkerColor;
                }
            }
        }

        void Animate(int startRadius, int endRadius, float intervalSeconds = 0.25f)
        {
            StopAllCoroutines();
            StartCoroutine(AnimateCoroutine(startRadius, endRadius, intervalSeconds));
        }

        IEnumerator AnimateCoroutine(int startRadius, int endRadius, float intervalSeconds = 0.25f, bool isResettingOnComplete = true)
        {
            int initialRadius = Radius;

            startRadius = Mathf.Max(0, startRadius);
            endRadius = Mathf.Max(0, endRadius);
            intervalSeconds = Mathf.Max(0, intervalSeconds);

            int increment = (startRadius < endRadius) ? 1 : -1;

            do
            {
                Radius = startRadius;
                startRadius += increment;
                yield return new WaitForSeconds(intervalSeconds);
            } while (startRadius <= endRadius);

            if (isResettingOnComplete)
            {
                Radius = initialRadius;
            }
        }

        [ContextMenu("Decrease Radius")]
        void DecreaseRadius()
        {
            Radius -= 1;
        }

        [ContextMenu("Increase Radius")]
        void IncreaseRadius()
        {
            Radius += 1;
        }

        [ContextMenu("Animation Test")]
        void AnimationTest()
        {
            Animate(0, map.Size.x);
        }
    }
}