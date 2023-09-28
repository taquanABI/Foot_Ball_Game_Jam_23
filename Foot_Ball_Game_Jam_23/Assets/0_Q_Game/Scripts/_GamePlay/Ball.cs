using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Ball : MonoBehaviour
{
    [HideInInspector] public bool isCan_Move;
    [HideInInspector] public Transform tf;
    public int index_Point_Moved;
    [Tooltip("Speed của cầu thủ sút")]
    public float force_Kick;

    public List<Player> list_Player_Target;

    Sequence sequence_Move_Ball;

    private void Awake()
    {
        tf = transform;
        // Create a sequence of Tweens
        Sequence sequence_Move_Ball = DOTween.Sequence();

        

        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void On_Init()
    {

    }

    public void Set_Move()
    {
        for (int i = 0; i < list_Player_Target.Count; i++)
        {
            float distance = Vector3.Distance(tf.position, list_Player_Target[i].tf_Ball_In.position);

            force_Kick = list_Player_Target[i].force_Kick;

            float time_Move_Each_Point = distance / force_Kick;

            sequence_Move_Ball.Append
                (
                tf.DOMove(list_Player_Target[i].tf_Ball_In.position, time_Move_Each_Point)
                .SetEase(Ease.Linear)
                .OnComplete(()=> { Debug.Log(" Kick " + i.ToString()); })
                );

        }

        sequence_Move_Ball.Play();

    }

    public void Set_List_Target_Move(List<Player> _list_Player_Target)
    {
        //list_Player_Target = _list_Player_Target; // reference

        //clone
        for (int i = 0; i < _list_Player_Target.Count; i++)
        {
            list_Player_Target.Add(_list_Player_Target[i]);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.tag.enemy))
        {
            isCan_Move = false;
            //TODO: stop bóng lăn

        }
    }
}
