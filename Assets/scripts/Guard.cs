using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
    [SerializeField]
    Transform[] pointsToMove;

    [SerializeField]
    float moveSpeed;

    [SerializeField]
    float holdDuration;
    public float MoveSpeed
    {
        get
        {
            return moveSpeed;
        }
        set
        {
            moveSpeed = value;
        }
    }
    public float HoldDuration
    {
        get
        {
            return holdDuration;
        }
        set
        {
            holdDuration = value;
        }
    }
  
    public int counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Patrol(pointsToMove));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Vector2 startPos = pointsToMove[0].position, secondPos;
        secondPos = startPos;

        foreach (var item in pointsToMove)
        {
            
            Gizmos.DrawWireSphere(item.position, 0.3f);
            Gizmos.color = Color.green;

            Gizmos.DrawLine(secondPos,item.position);
            secondPos = item.position;
        }
        Gizmos.DrawLine(secondPos, startPos);
    }

    IEnumerator Patrol(Transform[] moveTargetPos)
    {
        counter = 0;
        transform.position = moveTargetPos[0].transform.position;
        while (true)
        {
            transform.position = Vector2.MoveTowards(transform.position, moveTargetPos[counter].transform.position,moveSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, moveTargetPos[counter].transform.position) < 0.1f)
            {
                counter++;
                if (counter > pointsToMove.Length - 1)
                {
                    counter = 0;
                }
                yield return new WaitForSeconds(holdDuration);
            }

            yield return null;
        }
       
    }
    //void Patrol()
    //{
      
    //    for (int i = 0; i < pointsToMove.Length; i++)
    //    {
    //        transform.position = Vector3.MoveTowards(transform.position, pointsToMove[counter].position, moveSpeed * Time.deltaTime);
    //        if (Vector2.Distance(transform.position,pointsToMove[counter].position) < 1)
    //        {
    //            counter++;

    //            if (counter > pointsToMove.Length - 1)
    //            {
    //                counter = 0;
    //                transform.position = Vector3.MoveTowards(transform.position, pointsToMove[counter].position, moveSpeed * Time.deltaTime);
    //            }
    //        }

         
    //    }
    //}

}
