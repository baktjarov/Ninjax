using System.Collections.Generic;
using UI.Views;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(fileName = nameof(ListOfAllViews),
                    menuName = "Scriptables/" + nameof(ListOfAllViews))]
    public class ListOfAllViews : ScriptableObject
    {
        [SerializeField] private List<ViewBase> _views = new();

        public T GetView<T>() where T : ViewBase
        {
            T result = null;

            foreach (ViewBase view in _views)
            {
                if (view is T)
                {
                    result = (T)view;
                    continue;
                }
            }

            return result;
        }
    }
}