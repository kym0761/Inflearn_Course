using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{

    public T Load<T>(string Path) where T : Object
    {

        return Resources.Load<T>(Path);
    }


    public GameObject InstantiateFromPath(string Path, Transform Parent = null)
    {
        GameObject prefab = Load<GameObject>($"Prefabs/{Path}");

        if (prefab == null)
        {
            Debug.Log($"failed to load Prefab : {Path}");


            return null;
        }

        GameObject result = Object.Instantiate(prefab, Parent);
        return result;
    }

    public void Destroy(GameObject gameObj)
    {
        if (gameObj == null)
        {
            return;
        }

        Object.Destroy(gameObj);
    }


}
