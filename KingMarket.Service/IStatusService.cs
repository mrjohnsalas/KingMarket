using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using KingMarket.Model.Models;

namespace KingMarket.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IStatusService" in both code and config file together.
    [ServiceContract]
    public interface IStatusService
    {
        [OperationContract]
        IEnumerable<Status> GetStatuses();

        [OperationContract]
        Status GetStatus(int id);

        [OperationContract]
        void CreateStatus(Status myObject);

        [OperationContract]
        void EditStatus(Status myObject);

        [OperationContract]
        void DeleteStatus(int id);
    }
}
