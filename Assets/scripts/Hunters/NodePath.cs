using System.Collections.Generic;

public class NodePath : IPath
{
    public IPointPath Target => _points.Peek();

    private Stack<NodePoint> _points = new();
    public bool Over => _points.Count == 0;

    public NodePath(List<Node> nodes)
    {
        foreach(var node in nodes)
            _points.Push(new NodePoint(node));
        _points.Peek().WaitingTime = 2f;
        nodes.Reverse();
        foreach(var node in nodes)
            _points.Push(new NodePoint(node));
    }
    
    public void Initialize()
    {
        
    }

    public void NextTarget()
    {
        _points.Pop();
    }
}