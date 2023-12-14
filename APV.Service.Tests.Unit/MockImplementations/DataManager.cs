using APV.Service.Services;

namespace APV.Service.Tests.Unit.MockImplementations
{
    public class DataManager<T> : IDataManager<T>
    {
        private List<T> _data = new List<T>();

        public DataManager() 
        { 
        }

        public bool AddData(T data)
        {
            _data.Add(data);
            return true;
        }

        public List<T>? GetManyData(Dictionary<string, string>? filters = null)
        {
            return _data;
        }

        public T? GetData(Dictionary<string, string>? filters = null)
        {
            return _data.FirstOrDefault();
        }

        public bool IsConnected()
        {
            return true;
        }
    }
}
