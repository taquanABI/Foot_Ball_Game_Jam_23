﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameManager : SingletonMonoBehaviour<IngameManager>
{
    public List<Player> list_Player_Inlevel;
    // Start is called before the first frame update
    void Start()
    {
        LoadLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Set_Level_Win()
    {
        Timer.Schedule(this, 1, () =>
        {
            //TODO: Hiện encard win sau khi Win ở chỗ này
            Debug.Log(" Show win ");
        });
    }
    
    public void Set_Level_Fail()
    {
        Timer.Schedule(this, 1, () =>
        {
            //TODO: Hiện encard fail sau khi fail ở chỗ này
            Debug.Log(" Show fail ");
        });
    }

    private void LoadLevel()
    {
        Timer.Schedule(this, 1, () =>
        {

        });

        // ảnh 100 * 100: 
    }    
}
/*
 
 Ball
                list<Transform_Target>

                Set_target(list_Transform_Target); 

                Fly(); 
                    Kiểm tra list_Player có rỗng ko
                        di chuyển qua các pos từng Player trong list
                            Remove Player ra khỏi list
                
                
                Va chạm vs enemy
                    Dừng lại
                
                
                
                
                
                
 
            InGamePlay
                Get_Pos_
                list_Player_Will_Draw
                
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 */