using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Battlefield;
using Battlefield.Chunks;
using Battlefield.Generator;

public class Node
{
    Vector2 _position;
    Vector3 _chunkCenter;
    Chunk _chunk;

    public Node parent { get; set; }

    public float targetDist { get; set; }
    public float startDist { get; set; }

    public float totalDist => targetDist + startDist;

    public event UnityAction<Troop, Node> OnTroopEnter;
    //public event UnityAction<Troop, Node> OnTargetSelection;

    //getters
    public float x => _position.x;
    public float y => _position.y;
    public Chunk chunk => _chunk;
    public Vector2 position2d => _position;
    public Vector3 chunkCenter => _chunkCenter;


    public bool enemyInNode { get; set; } = false;


    public Node(Chunk chunk, LayerMask gridLayer)
    {
        _chunk = chunk;
        float posX = chunk.transform.position.x;
        float posZ = chunk.transform.position.z;
        _chunk.StartCoroutine(RaycastToChunkCenter(gridLayer));

        _position = new Vector2(posX, posZ);
    }

    IEnumerator RaycastToChunkCenter(LayerMask gridLayer)
    {
        yield return null;
        _chunkCenter = Raycasts.VerticalDown(_chunk.transform.position, gridLayer);
    }



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
