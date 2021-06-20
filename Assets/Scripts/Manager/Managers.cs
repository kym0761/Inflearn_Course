using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_Instance;
    static Managers Instance { get { Init(); return s_Instance; } }

    InputManager _Input = new InputManager();
    public static InputManager Input { get { return Instance._Input; } }

    ResourceManager _Resource = new ResourceManager();
    public static ResourceManager Resource { get { return Instance._Resource; } }

    // Start is called before the first frame update
    void Start()
    {
        Init();

    }

    // Update is called once per frame
    void Update()
    {
        _Input.OnUpdate();
    }

    static void Init()
    {
        if (s_Instance == null)
        {
            GameObject obj = GameObject.Find("@Manager");
            if (obj == null)
            {
                obj = new GameObject { name = "@Manager" };
                obj.AddComponent<Managers>();
            }

            DontDestroyOnLoad(obj);

            s_Instance = obj.GetComponent<Managers>();
        }
    
    }

}
