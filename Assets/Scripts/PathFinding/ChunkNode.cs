using System.Collections;
using System.Collections.Generic;
using Battlefield.Chunks;
using UnityEngine;

namespace PathFinding
{
    public class ChunkNode : Node
    {
        Chunk _chunk;
        public Chunk chunk => _chunk;


        public ChunkNode(Chunk chunk, LayerMask gridLayer)
        {
            _chunk = chunk;
            float posX = chunk.transform.position.x;
            float posZ = chunk.transform.position.z;
            //HACK coroutine IN CONSTRUCTOR!!!!
            _chunk.StartCoroutine(RaycastToChunkCenter(gridLayer));

            _position = new Vector2(posX, posZ);
        }

        IEnumerator RaycastToChunkCenter(LayerMask gridLayer)
        {
            yield return null;
            _nodeCenter = Raycasts.VerticalDown(_chunk.transform.position, gridLayer);
        }




    }
}