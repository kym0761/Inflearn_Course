using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Base : MonoBehaviour
{
    protected Dictionary<Type, UnityEngine.Object[]> _Objects = new Dictionary<Type, UnityEngine.Object[]>();


    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {

        string[] names = Enum.GetNames(type);

        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
        _Objects.Add(typeof(T), objects);

        for (int i = 0; i < names.Length; i++)
        {

            if (typeof(T) == typeof(GameObject))
            {
                objects[i] = Util.FindChild(gameObject, names[i], true);
            }
            else
            {
                objects[i] = Util.FindChild<T>(gameObject, names[i], true);
            }

            if (objects[i] == null)
            {
                Debug.Log($"Failed Bind() --> name : {names[i]}");
            }


        }
    }


    protected T Get<T>(int index) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objects = null;

        if (_Objects.TryGetValue(typeof(T), out objects) == false)
        {
            return null;
        }

        return objects[index] as T;

    }

    protected Text GetText(int index)
    {
        return Get<Text>(index);
    }

    protected Button GetButton(int index)
    {
        return Get<Button>(index);
    }

    protected Image GetImage(int index)
    {
        return Get<Image>(index);
    }

    public static void AddUIEvent(GameObject go , Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click)
    {
        UI_EventHandler evt = Util.GetOrAddComponent<UI_EventHandler>(go);

        switch (type)
        {
            case Define.UIEvent.Click:
                evt.OnClickHandler -= action;
                evt.OnClickHandler += action;
                break;

            case Define.UIEvent.Drag:
                evt.OnDragHandler -= action;
                evt.OnDragHandler += action;
                break;
        }
    }

}
