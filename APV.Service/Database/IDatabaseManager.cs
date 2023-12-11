namespace APV.Service.Database
{
    public interface IDataManager
    {
        public abstract bool IsConnected();
        public abstract T? GetData<T>(Dictionary<string, string>? filters = null);

        public abstract bool AddData<T>(T data);
    }
}
