public interface IPath
{
    IPointPath Target { get; }
    bool Running{ get; }
    bool Over { get; }

    void Initialize();

    void NextTarget();
}