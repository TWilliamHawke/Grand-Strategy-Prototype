
namespace PathFinding
{
	public interface InodeHolder<T> where T : Node
	{
	    T node { get; }
	}
}