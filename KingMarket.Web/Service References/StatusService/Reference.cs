﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KingMarket.Web.StatusService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="StatusService.IStatusService")]
    public interface IStatusService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStatusService/GetStatuses", ReplyAction="http://tempuri.org/IStatusService/GetStatusesResponse")]
        System.Collections.Generic.List<KingMarket.Model.Models.Status> GetStatuses();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStatusService/GetStatuses", ReplyAction="http://tempuri.org/IStatusService/GetStatusesResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<KingMarket.Model.Models.Status>> GetStatusesAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStatusService/GetStatus", ReplyAction="http://tempuri.org/IStatusService/GetStatusResponse")]
        KingMarket.Model.Models.Status GetStatus(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStatusService/GetStatus", ReplyAction="http://tempuri.org/IStatusService/GetStatusResponse")]
        System.Threading.Tasks.Task<KingMarket.Model.Models.Status> GetStatusAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStatusService/CreateStatus", ReplyAction="http://tempuri.org/IStatusService/CreateStatusResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(KingMarket.Model.Models.GeneralException), Action="http://tempuri.org/IStatusService/CreateStatusGeneralExceptionFault", Name="GeneralException", Namespace="http://schemas.datacontract.org/2004/07/KingMarket.Model.Models")]
        void CreateStatus(KingMarket.Model.Models.Status myObject);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStatusService/CreateStatus", ReplyAction="http://tempuri.org/IStatusService/CreateStatusResponse")]
        System.Threading.Tasks.Task CreateStatusAsync(KingMarket.Model.Models.Status myObject);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStatusService/EditStatus", ReplyAction="http://tempuri.org/IStatusService/EditStatusResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(KingMarket.Model.Models.GeneralException), Action="http://tempuri.org/IStatusService/EditStatusGeneralExceptionFault", Name="GeneralException", Namespace="http://schemas.datacontract.org/2004/07/KingMarket.Model.Models")]
        void EditStatus(KingMarket.Model.Models.Status myObject);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStatusService/EditStatus", ReplyAction="http://tempuri.org/IStatusService/EditStatusResponse")]
        System.Threading.Tasks.Task EditStatusAsync(KingMarket.Model.Models.Status myObject);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStatusService/DeleteStatus", ReplyAction="http://tempuri.org/IStatusService/DeleteStatusResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(KingMarket.Model.Models.GeneralException), Action="http://tempuri.org/IStatusService/DeleteStatusGeneralExceptionFault", Name="GeneralException", Namespace="http://schemas.datacontract.org/2004/07/KingMarket.Model.Models")]
        void DeleteStatus(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStatusService/DeleteStatus", ReplyAction="http://tempuri.org/IStatusService/DeleteStatusResponse")]
        System.Threading.Tasks.Task DeleteStatusAsync(int id);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IStatusServiceChannel : KingMarket.Web.StatusService.IStatusService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class StatusServiceClient : System.ServiceModel.ClientBase<KingMarket.Web.StatusService.IStatusService>, KingMarket.Web.StatusService.IStatusService {
        
        public StatusServiceClient() {
        }
        
        public StatusServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public StatusServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public StatusServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public StatusServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Collections.Generic.List<KingMarket.Model.Models.Status> GetStatuses() {
            return base.Channel.GetStatuses();
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<KingMarket.Model.Models.Status>> GetStatusesAsync() {
            return base.Channel.GetStatusesAsync();
        }
        
        public KingMarket.Model.Models.Status GetStatus(int id) {
            return base.Channel.GetStatus(id);
        }
        
        public System.Threading.Tasks.Task<KingMarket.Model.Models.Status> GetStatusAsync(int id) {
            return base.Channel.GetStatusAsync(id);
        }
        
        public void CreateStatus(KingMarket.Model.Models.Status myObject) {
            base.Channel.CreateStatus(myObject);
        }
        
        public System.Threading.Tasks.Task CreateStatusAsync(KingMarket.Model.Models.Status myObject) {
            return base.Channel.CreateStatusAsync(myObject);
        }
        
        public void EditStatus(KingMarket.Model.Models.Status myObject) {
            base.Channel.EditStatus(myObject);
        }
        
        public System.Threading.Tasks.Task EditStatusAsync(KingMarket.Model.Models.Status myObject) {
            return base.Channel.EditStatusAsync(myObject);
        }
        
        public void DeleteStatus(int id) {
            base.Channel.DeleteStatus(id);
        }
        
        public System.Threading.Tasks.Task DeleteStatusAsync(int id) {
            return base.Channel.DeleteStatusAsync(id);
        }
    }
}
