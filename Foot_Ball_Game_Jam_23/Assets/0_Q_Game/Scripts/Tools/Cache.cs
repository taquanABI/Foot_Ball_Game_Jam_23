using UnityEngine;
using System.Collections.Generic;
using System;

public class Cache
{

    private static Dictionary<float, WaitForSeconds> m_WFS = new Dictionary<float, WaitForSeconds>();

    public static WaitForSeconds GetWFS(float key)
    {
        if(!m_WFS.ContainsKey(key))
        {
            m_WFS[key] = new WaitForSeconds(key);
        }

        return m_WFS[key];
    }

    //------------------------------------------------------------------------------------------------------------


    private static Dictionary<Collider, Plane_Drawn_Arrow> m_Plane_Drawn_Arrow = new Dictionary<Collider, Plane_Drawn_Arrow>();

    public static Plane_Drawn_Arrow Get_Plane(Collider key)
    {
        if (!m_Plane_Drawn_Arrow.ContainsKey(key))
        {
            m_Plane_Drawn_Arrow.Add(key, key.GetComponent<Plane_Drawn_Arrow>());
        }

        return m_Plane_Drawn_Arrow[key];
    }

    //private static Dictionary<Collider, Character> m_Character = new Dictionary<Collider, Character>();

    //public static Character GetCharacter(Collider key)
    //{
    //    if (!m_Character.ContainsKey(key))
    //    {
    //        m_Character.Add(key, key.GetComponent<Character>());
    //    }

    //    return m_Character[key];
    //}


}
