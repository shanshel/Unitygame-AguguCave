using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContainer : MonoBehaviour
{

    Vector3 mOffset;
    float mZcoor;
    private void OnMouseDrag()
    {


        var move = GetMouseWorldPos() + mOffset;
        move.z = PlayerController._inst.playerShapes[0].transform.position.z;

        if (move.x > 7f)
            move.x = 7f;

        if (move.x < -7f)
            move.x = -7f;

        if (move.y > 15f)
            move.y = 15f;

        if (move.y < -2f)
            move.y = -2f;
     
        PlayerController._inst.moveTarget = move;
    

    }

    private void OnMouseDown()
    {
        mZcoor = Camera.main.WorldToScreenPoint(PlayerController._inst.playerShapes[0].transform.position).z;
        mOffset = PlayerController._inst.playerShapes[0].transform.position - GetMouseWorldPos();
    }



    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePosInScreen = Input.mousePosition;
        mousePosInScreen.z = mZcoor;
        return Camera.main.ScreenToWorldPoint(mousePosInScreen);
    }

    public Vector3 Floor(Vector3 vector3)
    {
        return new Vector3(
            Mathf.Floor(vector3.x),
            Mathf.Floor(vector3.y),
            Mathf.Floor(vector3.z));
    }
}
