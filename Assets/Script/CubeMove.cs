using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CubeMove : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource source;
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame

    private void LateUpdate()
    {
        float FandB = Input.GetAxis("Vertical");
        float LandR = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.W)||Input.GetKeyDown(KeyCode.S))
        {
            if (FandB>0)
            {
                RayMove(Vector3.forward);
            }else if (FandB<0)
            {
                RayMove(Vector3.back);
            }
            
        }
        if (Input.GetKeyDown(KeyCode.A)||Input.GetKeyDown(KeyCode.D))
        {
            if (LandR>0)
            {
                RayMove(Vector3.right);
            }else if (LandR<0)
            {
                RayMove(Vector3.left);
            }
            
        }
        UpdateHeiht();

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void RayMove(Vector3 Direction)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position,Direction,out hit,1.0f))
        {

            if (hit.collider.CompareTag("Winner"))
            {
                transform.position+=Direction;
            }
            else if (hit.collider.CompareTag("Box_Gra"))
            {
                BoxMove boxMove = hit.collider.GetComponent<BoxMove>();
                bool canMove = boxMove.CanMove(Direction,out bool Up);
                boxMove.IsGravity = true;
                if (canMove)
                {
                    transform.position+=Direction;
                }
                else if (Up)
                {
                    transform.position+=Direction;
                    transform.position+=Vector3.up;
                }
                else
                {
                    MoveUp(Direction);
                }
            }else if (hit.collider.CompareTag("Box_No_Gra"))
            {
                BoxMove boxMove = hit.collider.GetComponent<BoxMove>();
                bool canMove = boxMove.CanMove(Direction,out bool Up);
                if (canMove)
                {
                    transform.position+=Direction;
                }
                else if (Up)
                {
                    transform.position+=Direction;
                    transform.position+=Vector3.up;

                }
                else
                {
                    MoveUp(Direction);
                }
            }
            else
            {
                MoveUp(Direction);
            }
        }
        else
        {
            if (Physics.Raycast(transform.position+Direction,Vector3.down,out hit,100f))
            {
                transform.position+=Direction;
            }
        }
        source.Play();
    }

    private void UpdateHeiht()
    {
        if (Physics.Raycast(transform.position,Vector3.down,100f))
        {
            while (!Physics.Raycast(transform.position,Vector3.down,out RaycastHit hit,1.0f)||hit.collider.CompareTag("Winner"))
            {
                transform.position+=Vector3.down;
            }
        }
    }
    private void MoveUp(Vector3 Direction)
    {
        if (!Physics.Raycast(transform.position+Direction,Vector3.up,1f)
            &&!Physics.Raycast(transform.position,Vector3.up,1f))
        {
            transform.position+=Direction;
            transform.position+=Vector3.up;
        }else if (Physics.Raycast(transform.position + Direction, Vector3.up, out RaycastHit hit, 1f)
                  &&!Physics.Raycast(transform.position,Vector3.up,1f))

        {
            RaycastHit[] allUp = Physics.RaycastAll(transform.position + Direction, Vector3.up, 2.0f,
                ~(1 << LayerMask.NameToLayer("Winner")));
            if (allUp.Length<2)
            {
                BoxMove boxMove = hit.collider.GetComponent<BoxMove>();
                bool canMove;
                bool IsUp = false;
                if (boxMove!=null)
                {
                    canMove = boxMove.CanMove(Direction, out bool Up);
                    IsUp = Up;
                }
                else
                {
                    canMove = false;
                }
                if (canMove)
                {
                    transform.position += Direction;
                    transform.position += Vector3.up;
                }
                else if (IsUp)
                {
                    transform.position += Direction;
                    transform.position += Vector3.up;

                }else if (Physics.Raycast(transform.position+Direction,Vector3.up,out RaycastHit hitinfo,1f)&&!Physics.Raycast(transform.position,Vector3.up,1f))
                {
                    if (hitinfo.collider.CompareTag("Winner"))
                    {
                        transform.position += Direction;
                        transform.position += Vector3.up;
                    }
                }
            }

            if (Physics.Raycast(transform.position+Direction,Vector3.up,out RaycastHit hitInfo,1f)
                &&Physics.Raycast(transform.position+Direction+Vector3.up,Vector3.up,out RaycastHit hitinfo2,1f))
            {
                if (!Physics.Raycast(transform.position+Vector3.up+Direction,Direction,1f)
                    &&!Physics.Raycast(transform.position+Vector3.up+Vector3.up+Direction,Direction,1f))
                {
                    if (hitInfo.collider.CompareTag("Box_No_Gra")&& hitinfo2.collider.CompareTag("Box_Gra"))
                    {
                        BoxMove boxMove = hit.collider.GetComponent<BoxMove>();
                        bool canMove;
                        bool IsUp = false;
                        if (boxMove!=null)
                        {
                            canMove = boxMove.CanMove(Direction, out bool Up);
                            IsUp = Up;
                        }
                        else
                        {
                            canMove = false;
                        }
                        if (canMove)
                        {
                            transform.position += Direction;
                            transform.position += Vector3.up;
                        }
                        else if (IsUp)
                        {
                            transform.position += Direction;
                            transform.position += Vector3.up;

                        }else if (Physics.Raycast(transform.position+Direction,Vector3.up,out RaycastHit hitinfo,1f)&&!Physics.Raycast(transform.position,Vector3.up,1f))
                        {
                            if (hitinfo.collider.CompareTag("Winner"))
                            {
                                transform.position += Direction;
                                transform.position += Vector3.up;
                            }
                        }
                    }
                }
                
            }
            
        }
    }
}
