public class ViewBase<T>
{
    protected T Runner;
    public ViewBase(T runner)
    {
        Runner = runner;
    }
    public virtual void View(){}
}