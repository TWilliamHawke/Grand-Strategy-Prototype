using System.Collections.Generic;

namespace PathFinding
{
    public interface INodeList<T> where T : Node
    {
        IEnumerable<T> FindNeightborNodes(T node);
    }
}