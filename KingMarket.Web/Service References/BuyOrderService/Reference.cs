﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KingMarket.Web.BuyOrderService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="BuyOrderService.IBuyOrderService")]
    public interface IBuyOrderService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBuyOrderService/GetBuyOrdersByUserId", ReplyAction="http://tempuri.org/IBuyOrderService/GetBuyOrdersByUserIdResponse")]
        System.Collections.Generic.List<KingMarket.Model.Models.BuyOrder> GetBuyOrdersByUserId(string userId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBuyOrderService/GetBuyOrdersByUserId", ReplyAction="http://tempuri.org/IBuyOrderService/GetBuyOrdersByUserIdResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<KingMarket.Model.Models.BuyOrder>> GetBuyOrdersByUserIdAsync(string userId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBuyOrderService/GetBuyOrder", ReplyAction="http://tempuri.org/IBuyOrderService/GetBuyOrderResponse")]
        KingMarket.Model.Models.BuyOrder GetBuyOrder(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBuyOrderService/GetBuyOrder", ReplyAction="http://tempuri.org/IBuyOrderService/GetBuyOrderResponse")]
        System.Threading.Tasks.Task<KingMarket.Model.Models.BuyOrder> GetBuyOrderAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBuyOrderService/CreateBuyOrder", ReplyAction="http://tempuri.org/IBuyOrderService/CreateBuyOrderResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(KingMarket.Model.Models.GeneralException), Action="http://tempuri.org/IBuyOrderService/CreateBuyOrderGeneralExceptionFault", Name="GeneralException", Namespace="http://schemas.datacontract.org/2004/07/KingMarket.Model.Models")]
        void CreateBuyOrder(KingMarket.Model.Models.BuyOrder myObject);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBuyOrderService/CreateBuyOrder", ReplyAction="http://tempuri.org/IBuyOrderService/CreateBuyOrderResponse")]
        System.Threading.Tasks.Task CreateBuyOrderAsync(KingMarket.Model.Models.BuyOrder myObject);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IBuyOrderServiceChannel : KingMarket.Web.BuyOrderService.IBuyOrderService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class BuyOrderServiceClient : System.ServiceModel.ClientBase<KingMarket.Web.BuyOrderService.IBuyOrderService>, KingMarket.Web.BuyOrderService.IBuyOrderService {
        
        public BuyOrderServiceClient() {
        }
        
        public BuyOrderServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public BuyOrderServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public BuyOrderServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public BuyOrderServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Collections.Generic.List<KingMarket.Model.Models.BuyOrder> GetBuyOrdersByUserId(string userId) {
            return base.Channel.GetBuyOrdersByUserId(userId);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<KingMarket.Model.Models.BuyOrder>> GetBuyOrdersByUserIdAsync(string userId) {
            return base.Channel.GetBuyOrdersByUserIdAsync(userId);
        }
        
        public KingMarket.Model.Models.BuyOrder GetBuyOrder(int id) {
            return base.Channel.GetBuyOrder(id);
        }
        
        public System.Threading.Tasks.Task<KingMarket.Model.Models.BuyOrder> GetBuyOrderAsync(int id) {
            return base.Channel.GetBuyOrderAsync(id);
        }
        
        public void CreateBuyOrder(KingMarket.Model.Models.BuyOrder myObject) {
            base.Channel.CreateBuyOrder(myObject);
        }
        
        public System.Threading.Tasks.Task CreateBuyOrderAsync(KingMarket.Model.Models.BuyOrder myObject) {
            return base.Channel.CreateBuyOrderAsync(myObject);
        }
    }
}
