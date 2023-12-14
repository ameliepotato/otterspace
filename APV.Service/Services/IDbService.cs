namespace APV.Service.Services
{
    public interface IDbService
    {
        public abstract bool IsConnected();
        public abstract bool CreateIndex();
    }
}
