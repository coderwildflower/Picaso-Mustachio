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

    [SerializeField]
    float turnSpeed;
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

    private int counter = 0;

    //FOV variables
    private float viewDistance;
    private float viewAngle;
    static GameObject PlayerObj;

    private void Awake()
    {
        if (PlayerObj == null)
        {
            PlayerObj = GameObject.FindGameObjectWithTag("Player");
        }
        else
        {
            return;
        }
    }
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
            Gizmos.color = Color.yellow;

            Gizmos.DrawLine(secondPos, item.position);
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
            transform.position = Vector2.MoveTowards(transform.position, moveTargetPos[counter].transform.position, moveSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, moveTargetPos[counter].transform.position) < 0.1f)
            {
                counter++;
                if (counter > pointsToMove.Length - 1)
                {
                    counter = 0;
                }
                yield return new WaitForSeconds(holdDuration);
                yield return StartCoroutine(lookAtTarget(moveTargetPos[counter]));
            }

            yield return null;
        }

    }

    IEnumerator lookAtTarget(Transform target)
    {
        Vector3 dir = target.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;

        Quaternion rot = Quaternion.AngleAxis(angle, Vector3.forward);
        while (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.z,angle)) > 0.1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, turnSpeed * Time.deltaTime);
            yield return null;

        }
    }

    bool isTargetFound()
    {
        if (Vector2.Distance(transform.position,PlayerObj.transform.position) < viewDistance)
        {
            
        }
        return false;
    }
}
