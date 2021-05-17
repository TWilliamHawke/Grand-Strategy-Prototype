using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Battlefield;

public class Node
{
    public int x { get; private set; }
    public int y { get; private set; }

    public Node parent { get; set; }

    public Square square { get; private set; }

    public int targetDist { get; set; }
    public int startDist { get; set; }

    public int totalDist => targetDist + startDist;

    public event UnityAction<Troop, Node> OnTroopEnter;
    //public event UnityAction<Troop, Node> OnTargetSelection;
    


    public Node(Square square)
    {
        this.square = square;
        var position = square.GetPosition();
        x = (int)position.x;
        y = (int)position.y;
    }

    public int GetDistanceFrom(Node node)
    {
        int deltaX = Mathf.Abs(node.x - x);
        int deltaY = Mathf.Abs(node.y - y);

        if(deltaX > deltaY)
        {
            return deltaY * 14 + (deltaX - deltaY) * 10;
        }
        else
        {
            return deltaX * 14 + (deltaY - deltaX) * 10;
        }
    }

    public void EnterTroop(Troop troop)
    {
        OnTroopEnter?.Invoke(troop, this);
                    Debug.Log("enter");

    }
}
