using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[ExecuteAlways]
public class RoomPass : MonoBehaviour
{

    public List<RoomDoor> rooms = new List<RoomDoor> ();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        foreach (RoomDoor roomDoor in rooms) 
        {
            Debug.DrawLine(transform.position, roomDoor.door.transform.position,Color.blue);
        }

    }

    public Vector2 findDirectionTo(Vector2 pos)
    {
        Collider2D[] colids =  Physics2D.OverlapBoxAll(pos, Vector3.zero,0f);

        RoomPass roomFind = null;
        int i = 0;
        while (i < colids.Length && roomFind == null) 
        {
            roomFind = colids[i].GetComponent<RoomPass>();
            i++;
        }

        if(roomFind == this)
            return pos;


        if (roomFind != null)
        {
            List<RoomPass> pass = new List<RoomPass>();

            List<RoomDoorDirection> dir = new List<RoomDoorDirection>();
            RoomDoorDirection final = null;

            List<RoomDoorDirection> toPass = new List<RoomDoorDirection>();

            foreach (RoomDoor door in rooms)
                toPass.Add(new RoomDoorDirection(door, null));

            while (toPass.Count > 0 && final == null)
            {
                RoomPass next = toPass[0].cur.room;
                if (next == roomFind)
                {
                    final = toPass[0];
                }
                else if (!pass.Contains(next))
                {
                    foreach (RoomDoor door in next.rooms)
                        toPass.Add(new RoomDoorDirection(door, toPass[0]));
                }
                toPass.Remove(toPass[0]);

            }

            if (final != null)
            {
                RoomDoorDirection temp = final;

                while (temp.past != null)
                {
                    temp = temp.past;
                }

                return temp.cur.door.transform.position;

            }
        }
        return Vector2.zero;
    }

}

[System.Serializable]
public class RoomDoor
{
    public GameObject door;
    public RoomPass room;
}
[System.Serializable]
public class RoomDoorDirection
{
    public RoomDoorDirection(RoomDoor cur, RoomDoorDirection past)
    {
        this.past = past;
        this.cur = cur;
    }

    public RoomDoor cur;
    public RoomDoorDirection past;
}
