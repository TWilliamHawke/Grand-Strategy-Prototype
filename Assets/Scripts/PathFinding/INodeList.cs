using System.Collections.Generic;

namespace PathFinding
{
    public interface INodeList<T> where T : Node
    {
        List<T> FindNeightborNodes(T node);
    }
}