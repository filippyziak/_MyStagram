namespace MyStagram.Core.Builders.Interface
{
    public interface IBuilder<T> where T : class, new()
    {
        T Build();
    }
}