using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BoxMove : MonoBehaviour
{
    // Start is called before the first frame update
    [HideInInspector]
    public bool IsGravity = false;
    public bool hasGra = true;

    private bool isRunning = false;
    void Start()
    {
        if (hasGra==true)
        {
            this.tag = "Box_Gra";
        }else if (hasGra==false)
        {
            this.tag = "Box_No_Gra";
        }
        
    }

    void FixedUpdate()
    {
        if (hasGra)
        {
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 100.0f))
            {
                if (!Physics.Raycast(transform.position,Vector3.down,out RaycastHit info,1.0f,~(1<<LayerMask.NameToLayer("Winner"))))
                {
                    while (!Physics.Raycast(transform.position, Vector3.down, 1.0f,~(1<<LayerMask.NameToLayer("Winner")))&&IsGravity==true)
                    {
                        transform.position += Vector3.down;
                    }
                }
        
                IsGravity = false;
            }
            if (!Physics.Raycast(transform.position, Vector3.down, 100.0f))
            {
                Destroy(this.gameObject);
            }
        }
        if (!Physics.Raycast(transform.position, Vector3.down, 100.0f))
        {
            Destroy(this.gameObject);
        }
        
        
    }

    public void UpdateHeight()
    {
        if (Physics.Raycast(transform.position, Vector3.down, 100.0f)&&IsGravity)
        {
            while (!Physics.Raycast(transform.position, Vector3.down, 1.0f))
            {
                transform.position += Vector3.down;
            }
        }

       
    }
    public bool CanMove(Vector3 pos,out bool Up)
    {
        Up = false;
        if (Physics.Raycast(transform.position, pos,out RaycastHit hitinfo ,1.0f))
        {
            if (hitinfo.collider.CompareTag("Winner"))
            {
                Move(pos);
                return true;
            }
            if (Physics.Raycast(transform.position, Vector3.up,out RaycastHit hitInfo, 1.0f))
            {
                if (!hitInfo.collider.CompareTag(this.tag))
                {
                    
                    BoxMove boxMove = hitInfo.collider.GetComponent<BoxMove>();
                    bool canMove = boxMove.CanMove(pos);
                    if (canMove)
                    { 
                        Up = true;
                    }
                   

                }
            }

            return false;
        }
        else
        {
            Move(pos);
            return true;
        }
    }
    public bool CanMove(Vector3 pos)
    {
       
        if (Physics.Raycast(transform.position, pos,out RaycastHit hit, 1.0f))
        {
            if (hit.collider.CompareTag("Winner"))
            {
                Move(pos);
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            Move(pos);
            return true;
        }
    }
    public void Move(Vector3 pos)
    {

        RaycastHit hit;
        BoxMove boxMove = null;
        if (Physics.Raycast(transform.position, Vector3.up, out hit, 1.0f))
        {
            if (hit.collider.CompareTag(this.tag)||(hit.collider.CompareTag("Box_Gra")&&this.CompareTag("Box_No_Gra")))
            {
                boxMove = hit.collider.GetComponent<BoxMove>();
                boxMove.IsGravity = false;
                boxMove.CanMove(pos);
            }
            
        }
        transform.position += pos;
        if (!isRunning)
        {
            if (boxMove != null)
            {
                StartCoroutine(StopTimeToUpdate(boxMove));
            }
            else
            {
                StartCoroutine(StopTimeToUpdate());
            }
            
        }
    }

    IEnumerator StopTimeToUpdate(BoxMove boxMove)
    {
        isRunning = true;
        yield return new WaitForSeconds(0.1f);
        boxMove.IsGravity = true;
        yield return new WaitForSeconds(0.1f);
        boxMove.IsGravity = false;
        isRunning = false;
        yield break;
    }
    IEnumerator StopTimeToUpdate()
    {
        isRunning = true;
        yield return new WaitForSeconds(0.1f);
        IsGravity = true;
        yield return new WaitForSeconds(0.1f);
        IsGravity = false;
        isRunning = false;
        yield break;
    }
    
}

