using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LoadLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
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