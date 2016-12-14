using System.ServiceModel;

namespace TimeService
{
    [ServiceContract]
    public interface ITimeService
    {
        [OperationContract]
        string GetCurrentTime();
    }
}