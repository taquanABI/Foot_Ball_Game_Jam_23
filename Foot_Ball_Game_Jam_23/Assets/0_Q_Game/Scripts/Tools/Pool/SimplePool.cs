using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class SimplePool
{
    static int DEFAULT_AMOUNT = 10;
    //pool tong
    static Dictionary<GameUnit, Pool> poolObjects = new Dictionary<GameUnit, Pool>();

    //tim pool cha cua thang object
    static Dictionary<GameUnit, Pool> poolParents = new Dictionary<GameUnit, Pool>();

    public static void Preload(GameUnit prefab, int amount, Transform parent)
    {
        if (!poolObjects.ContainsKey(prefab))
        {
            poolObjects.Add(prefab, new Pool(prefab, amount, parent));
        }
    }

    public static GameUnit Spawn(GameUnit prefab, Vector3 position, Quaternion rotation)
    {
        GameUnit obj = null;

        if (!poolObjects.ContainsKey(prefab) || poolObjects[prefab] == null)
        {
            poolObjects.Add(prefab, new Pool(prefab, DEFAULT_AMOUNT, null));
            Debug.Log(" Spawn    "  );
        }

        obj = poolObjects[prefab].Spawn(position, rotation);


        if (obj == null)
        {
            Debug.Log(" obj == null    ");
        }


        return obj;
    }

    public static T Spawn<T>(GameUnit prefab, Vector3 position, Quaternion rotation) where T : GameUnit
    {
        GameUnit obj = null;

        if (!poolObjects.ContainsKey(prefab) || poolObjects[prefab] == null)
        {
            poolObjects.Add(prefab, new Pool(prefab, DEFAULT_AMOUNT, null));
        }

        obj = poolObjects[prefab].Spawn(position, rotation);

        return obj as T;
    }

    public static void Despawn(GameUnit obj)
    {
        if (poolParents.ContainsKey(obj))
        {
            poolParents[obj].Despawn(obj);
        }
        else
        {
            GameObject.Destroy(obj);
        }
    }

    public static void CollectAll()
    {
        foreach (var item in poolObjects)
        {
            item.Value.Collect();
        }
    }

    public static void ReleaseAll()
    {
        foreach (var item in poolObjects)
        {
            item.Value.Release();
        }
    }

    public class Pool
    {
        Queue<GameUnit> pools = new Queue<GameUnit>();
        List<GameUnit> activeObjs = new List<GameUnit>();
        Transform parent;
        GameUnit prefab;

        public Pool(GameUnit prefab, int amount, Transform parent)
        {
            this.prefab = prefab;
            this.parent = parent;

            for (int i = 0; i < amount; i++)
            {
                GameUnit obj = GameObject.Instantiate(prefab, parent);
                poolParents.Add(obj, this);
                pools.Enqueue(obj);
                obj.gameObject.SetActive(false);
            }
        }

        public GameUnit Spawn(Vector3 position, Quaternion rotation)
        {
            GameUnit obj = null;

            if (pools.Count == 0)
            {
                obj = GameObject.Instantiate(prefab, parent);
                poolParents.Add(obj, this);
            }
            else
            {
                obj = pools.Dequeue();
            }

            obj.transform.SetPositionAndRotation(position, rotation);
            obj.gameObject.SetActive(true);

            activeObjs.Add(obj);

            return obj;
        }

        public void Despawn(GameUnit obj)
        {
            if (obj.gameObject.activeInHierarchy)
            {
                activeObjs.Remove(obj);
                pools.Enqueue(obj);
                obj.gameObject.SetActive(false);
            }
        }

        public void Collect()
        {
            while (activeObjs.Count > 0)
            {
                Despawn(activeObjs[0]);
            }
        }

        public void Release()
        {
            Collect();

            while (pools.Count > 0)
            {
                GameUnit obj = pools.Dequeue();
                GameObject.DestroyImmediate(obj);
            }
        }

    }
}

public class GameUnit: MonoBehaviour
{
    private Transform Transforms;

    public Transform tf
    {
        get
        {
            if (this.Transforms == null)
            {
                this.Transforms = transform;
            }

            return Transforms;
        }
        
    }
    
}