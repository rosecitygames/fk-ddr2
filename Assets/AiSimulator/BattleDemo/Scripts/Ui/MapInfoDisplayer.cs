using System;
using System.Collections.Generic;
using UnityEngine;
using IndieDevTools.Maps;
using UnityEngine.UI;
using TMPro;

namespace IndieDevTools.Demo.BattleSimulator
{
    public class MapInfoDisplayer : MonoBehaviour
    {
        [SerializeField]
        Image icon = null;

        [SerializeField]
        TextMeshProUGUI displayName = null;

        [SerializeField]
        TextMeshProUGUI description = null;

        [NonSerialized]
        IMapElement currentMapElement = null;

        [NonSerialized]
        Sprite currentIconSprite = null;

        [NonSerialized]
        Color currentIconColor = Color.white;

        void Start()
        {
            Draw();
        }

        void Update()
        {
            DetectMouseClick();
        }

        void DetectMouseClick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                HandleMouseClick();
            }
        }

        void HandleMouseClick()
        {
            RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            IMapElement mapElement = null;

            if (hitInfo.transform != null)
            {
                GameObject hitGameObject = hitInfo.transform.gameObject;

                mapElement = hitGameObject.GetComponent<IMapElement>();
                if (mapElement != null)
                {
                    SpriteRenderer spriteRenderer = hitGameObject.GetComponentInChildren<SpriteRenderer>();
                    if (spriteRenderer != null)
                    {
                        currentIconSprite = spriteRenderer.sprite;
                        currentIconColor = spriteRenderer.color;
                    }
                }
            }

            SetMapElement(mapElement);
        }

        void SetMapElement(IMapElement mapElement)
        {
            currentMapElement = mapElement;
            Draw();
        }

        void Draw()
        {
            Clear();

            if (currentMapElement == null) return;

            displayName.text = currentMapElement.DisplayName;
            description.text = currentMapElement.Description;

            icon.sprite = currentIconSprite;
            icon.color = currentIconColor;
            icon.enabled = true;
        }

        void Clear()
        {
            icon.sprite = null;
            icon.enabled = false;

            displayName.text = "";
            description.text = "";
        }
    }
}
