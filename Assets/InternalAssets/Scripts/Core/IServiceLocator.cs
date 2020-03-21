namespace DP.TowerDefense
{
    public interface IServiceLocator
    {
        T GetService<T>();
        void Update();
    }
}