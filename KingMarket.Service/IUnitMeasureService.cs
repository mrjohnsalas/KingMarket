using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using KingMarket.Model.Models;

namespace KingMarket.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IUnitMeasureService" in both code and config file together.
    [ServiceContract]
    public interface IUnitMeasureService
    {
        [OperationContract]
        IEnumerable<UnitMeasure> GetUnitMeasures();

        [OperationContract]
        UnitMeasure GetUnitMeasure(int id);

        [FaultContract(typeof(GeneralException))]
        [OperationContract]
        void CreateUnitMeasure(UnitMeasure myObject);

        [FaultContract(typeof(GeneralException))]
        [OperationContract]
        void EditUnitMeasure(UnitMeasure myObject);

        [FaultContract(typeof(GeneralException))]
        [OperationContract]
        void DeleteUnitMeasure(int id);
    }
}
