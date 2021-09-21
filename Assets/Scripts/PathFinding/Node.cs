using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Battlefield;

public class Node
{
    protected Vector2 _position;
    protected Vector3 _nodeCenter;

    public Node parent { get; set; }

    public float targetDist { get; set; }
    public float startDist { get; set; }

    public float totalDist => targetDist + startDist;

    public event UnityAction<Troop, Node> OnTroopEnter;
    //public event UnityAction<Troop, Node> OnTargetSelection;

    //getters
    public float x => _position.x;
    public float y => _position.y;
    public Vector2 position2d => _position;
    public Vector3 nodeCenter => _nodeCenter;


    public bool enemyInNode { get; set; } = false;


    public float GetDistanceFrom(Node node)
    {
        float deltaX = Mathf.Abs(node.x - x);
        float deltaY = Mathf.Abs(node.y - y);

        if (deltaX > deltaY)
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
    }

    public override string ToString()
    {
        return position2d.ToString();
    }
}
