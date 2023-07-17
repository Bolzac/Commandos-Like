using UnityEngine;

public class HearRunning : ViewBase<Enemy>
{
    private Collider[] _results = new Collider[4];
    private int _size;
    public HearRunning(Enemy runner) : base(runner)
    {
    }
    
    public override void View()
    {
        base.View();
    }
}