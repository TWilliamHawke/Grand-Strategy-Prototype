using System.Collections;
using System.Collections.Generic;
using Battlefield.Generator;
using UnityEngine;

namespace Battlefield.Chunks
{
    public class PathArrowController
    {
        ChunkArrow _pathArrow;

        public PathArrowController(ChunkArrow pathArrow)
        {
            _pathArrow = pathArrow;
        }

        public void RotatePathArrow(Directions direction)
        {
            _pathArrow.gameObject.SetActive(true);
            float angleY = (int)direction * 45;
            //_pathArrow.transform.eulerAngles = new Vector3(0, angleY, 0);
            _pathArrow.transform.rotation = Quaternion.Euler(0, angleY, 0);
        }


        public void HidePathArrow()
        {
            _pathArrow.gameObject.SetActive(false);
        }


    }
}