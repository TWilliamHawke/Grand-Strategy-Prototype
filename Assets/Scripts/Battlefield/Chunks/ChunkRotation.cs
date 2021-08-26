using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battlefield.Chunks
{
	public class ChunkRotation
	{
	    FrameController _frameController;
		SelectedObjects _selectedObjects;
		InnerArrowController _innerArrowController;

        public ChunkRotation(FrameController frameController, SelectedObjects selectedObjects, InnerArrowController innerArrowController)
        {
            _frameController = frameController;
            _selectedObjects = selectedObjects;
            _innerArrowController = innerArrowController;
        }
    }
}