public interface IPath
{
    IPointPath Target { get; }
    bool Over { get; }

    void Initialize();

    void NextTarget();
}