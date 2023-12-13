namespace APV.Service.Database
{
    public interface IDataManager<T>
    {
        public abstract bool IsConnected();
        public abstract T? GetData(Dictionary<string, string>? filters = null);

        public abstract bool AddData(T data);
    }
}
