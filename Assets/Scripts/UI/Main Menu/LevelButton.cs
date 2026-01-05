using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.GPUPrefixSum;

namespace Assets.Scripts.UI
{
    [UxmlElement]
    public partial class LevelButton : VisualElement
    {
        public readonly int ID;
        public LevelButton() { }

        public LevelButton(int id, Vector2 pos, EventCallback<PointerDownEvent> callback)
        {
            AddToClassList("map-button");
            style.left = pos.x;
            style.top = pos.y;
            ID = id;
            Add(new Label(id.ToString()));
            this[0].focusable = false;
            this[0].pickingMode = PickingMode.Ignore;
            RegisterCallback(callback);
        }
    }
}
