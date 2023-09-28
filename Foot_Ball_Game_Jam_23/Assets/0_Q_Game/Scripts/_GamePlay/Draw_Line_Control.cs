using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw_Line_Control : MonoBehaviour
{
    public bool isCanDraw;
    public bool is_Drawing;

    [Tooltip("đã sút bóng chưa")]
    public bool is_Kicked;
    public int count_Point_Player;
    public int count_Point_Player_Max;

    [HideInInspector] public Line line_Drag;
    //public Line line_conected;

    public Vector3 vec_Poit_Start;
    public Transform tf_Start_Drawn;
    public Transform tf_Plan;
    public Transform tf_Plan_White_Line;

    public Player player_Init;
    public List<Player> list_Player_Will_Draw;
    public List<Line> list_LineWhite_Draw;
    public Ball ball;

    [Tooltip("số lần vẽ")]
    public int times_Drag;
    [Tooltip("số lần được vẽ")]
    public int times_Can_Drag_Max;
    // Start is called before the first frame update
    void Start()
    {
        On_Init();
    }
    public void On_Init()
    {
        vec_Poit_Start = tf_Start_Drawn.position;
        Set_Point_Start(tf_Start_Drawn);

        

        int _level = DataManager.ins.playerData.level;

        count_Point_Player_Max = GameConfig.ins.Get_max_Count_Point_Connect(_level);

        list_Player_Will_Draw.Add(player_Init);

        times_Drag = 0;

    }
    // Update is called once per frame
    void Update()
    {
        if (!isCanDraw)
        {
            return;
        }




        if (Input.GetMouseButtonDown(0))
        {
            if (!isCanDraw && !is_Kicked)
            {
                return;
            }

            

            if (times_Drag < times_Can_Drag_Max)
            {
                line_Drag = (Line)(PoolController.Ins.miniPool_Line_Blue.Spawn(Vector3.zero, Quaternion.identity));


                Vector3 v_start = new Vector3(player_Init.tf.position.x, tf_Plan_White_Line.position.y, player_Init.tf.position.z);


                line_Drag.UpdateTrajectory(v_start, v_start);
            }
        }
        

        if (Input.GetMouseButton(0))
        {
            if (times_Drag < times_Can_Drag_Max)
            {
                this.GetMouseDrag();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (is_Drawing)
            {
                times_Drag++;
                if (!is_Kicked)
                {
                    Set_Done_Draw();
                }
            }
        }
    }
    private void GetMouseDrag()
    {
        is_Drawing = true;
        var output = Check_Ray_Cast.Ins.Get_Pos_Raycast_Hit_Plan();
        if (output.Item1)
        {
            line_Drag.UpdateTrajectory(output.Item2, vec_Poit_Start);
        }
        else
        {
            // ko raycast vào plan
        }

        var colider_Player = Check_Ray_Cast.Ins.Get_Raycast_Colider_Player();
        if (colider_Player != null)
        {
            if (list_Player_Will_Draw.Count != 0)
            {
                if (colider_Player.player != list_Player_Will_Draw[list_Player_Will_Draw.Count - 1])
                {
                    //nếu tay di vào player khác player vừa nối
                    if (count_Point_Player < count_Point_Player_Max)
                    {
                        // thêm Player vào list các target .. tạo mới Line trắng qua 2 cầu thủ vừa rồi
                        list_Player_Will_Draw.Add(colider_Player.player);

                        Line _line_White = (Line)(PoolController.Ins.miniPool_Line_White.Spawn(Vector3.zero, Quaternion.identity));

                        list_LineWhite_Draw.Add(_line_White);




                        int total = list_Player_Will_Draw.Count;

                        Vector3 v_start = new Vector3(list_Player_Will_Draw[total - 2].tf.position.x,tf_Plan_White_Line.position.y , list_Player_Will_Draw[total - 2].tf.position.z);

                        
                        Vector3 v_end = new Vector3(list_Player_Will_Draw[total - 1].tf.position.x,tf_Plan_White_Line.position.y , list_Player_Will_Draw[total - 1].tf.position.z);


                        _line_White.UpdateTrajectory(v_start,v_end);

                        //đổi vị trí điểm đầu tiên của đường xanh lúc merge xong bắt đầu từ cầu thủ tiếp theo
                        line_Drag.UpdateTrajectory(v_end, v_end);

                        count_Point_Player++;
                    }
                    else
                    {
                        //TODO: Kết thúc vẽ Line
                        Set_Done_Draw();
                    }
                }
            }
        }

    }

    public void Set_Done_Draw()
    {
        //TODO: Camera move góc chéo
        is_Kicked = true;
        Timer.Schedule(this, 0, () =>
        {
            Set_Kick_Ball();
        });

    }
    
    public void Set_Kick_Ball()
    {
        ball.Set_List_Target_Move(list_Player_Will_Draw);
        ball.Set_Move();
    }

    public void Set_Point_Start(Transform tf_start)
    {
        vec_Poit_Start = new Vector3(tf_start.position.x, tf_Plan.position.y, tf_start.position.z);
    }
}
