﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KingMarket.Web.CartItemService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="CartItemService.ICartItemService")]
    public interface ICartItemService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICartItemService/GetCartItemsByCustomerId", ReplyAction="http://tempuri.org/ICartItemService/GetCartItemsByCustomerIdResponse")]
        System.Collections.Generic.List<KingMarket.Model.Models.CartItem> GetCartItemsByCustomerId(int customerId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICartItemService/GetCartItemsByCustomerId", ReplyAction="http://tempuri.org/ICartItemService/GetCartItemsByCustomerIdResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<KingMarket.Model.Models.CartItem>> GetCartItemsByCustomerIdAsync(int customerId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICartItemService/GetCartItem", ReplyAction="http://tempuri.org/ICartItemService/GetCartItemResponse")]
        KingMarket.Model.Models.CartItem GetCartItem(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICartItemService/GetCartItem", ReplyAction="http://tempuri.org/ICartItemService/GetCartItemResponse")]
        System.Threading.Tasks.Task<KingMarket.Model.Models.CartItem> GetCartItemAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICartItemService/GetCartItemByProductIdAndCustomerId", ReplyAction="http://tempuri.org/ICartItemService/GetCartItemByProductIdAndCustomerIdResponse")]
        KingMarket.Model.Models.CartItem GetCartItemByProductIdAndCustomerId(int productId, int customerId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICartItemService/GetCartItemByProductIdAndCustomerId", ReplyAction="http://tempuri.org/ICartItemService/GetCartItemByProductIdAndCustomerIdResponse")]
        System.Threading.Tasks.Task<KingMarket.Model.Models.CartItem> GetCartItemByProductIdAndCustomerIdAsync(int productId, int customerId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICartItemService/CreateCartItem", ReplyAction="http://tempuri.org/ICartItemService/CreateCartItemResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(KingMarket.Model.Models.GeneralException), Action="http://tempuri.org/ICartItemService/CreateCartItemGeneralExceptionFault", Name="GeneralException", Namespace="http://schemas.datacontract.org/2004/07/KingMarket.Model.Models")]
        void CreateCartItem(KingMarket.Model.Models.CartItem myObject);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICartItemService/CreateCartItem", ReplyAction="http://tempuri.org/ICartItemService/CreateCartItemResponse")]
        System.Threading.Tasks.Task CreateCartItemAsync(KingMarket.Model.Models.CartItem myObject);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICartItemService/EditCartItem", ReplyAction="http://tempuri.org/ICartItemService/EditCartItemResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(KingMarket.Model.Models.GeneralException), Action="http://tempuri.org/ICartItemService/EditCartItemGeneralExceptionFault", Name="GeneralException", Namespace="http://schemas.datacontract.org/2004/07/KingMarket.Model.Models")]
        void EditCartItem(KingMarket.Model.Models.CartItem myObject);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICartItemService/EditCartItem", ReplyAction="http://tempuri.org/ICartItemService/EditCartItemResponse")]
        System.Threading.Tasks.Task EditCartItemAsync(KingMarket.Model.Models.CartItem myObject);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICartItemService/DeleteCartItem", ReplyAction="http://tempuri.org/ICartItemService/DeleteCartItemResponse")]
        void DeleteCartItem(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICartItemService/DeleteCartItem", ReplyAction="http://tempuri.org/ICartItemService/DeleteCartItemResponse")]
        System.Threading.Tasks.Task DeleteCartItemAsync(int id);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ICartItemServiceChannel : KingMarket.Web.CartItemService.ICartItemService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CartItemServiceClient : System.ServiceModel.ClientBase<KingMarket.Web.CartItemService.ICartItemService>, KingMarket.Web.CartItemService.ICartItemService {
        
        public CartItemServiceClient() {
        }
        
        public CartItemServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public CartItemServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CartItemServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CartItemServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Collections.Generic.List<KingMarket.Model.Models.CartItem> GetCartItemsByCustomerId(int customerId) {
            return base.Channel.GetCartItemsByCustomerId(customerId);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<KingMarket.Model.Models.CartItem>> GetCartItemsByCustomerIdAsync(int customerId) {
            return base.Channel.GetCartItemsByCustomerIdAsync(customerId);
        }
        
        public KingMarket.Model.Models.CartItem GetCartItem(int id) {
            return base.Channel.GetCartItem(id);
        }
        
        public System.Threading.Tasks.Task<KingMarket.Model.Models.CartItem> GetCartItemAsync(int id) {
            return base.Channel.GetCartItemAsync(id);
        }
        
        public KingMarket.Model.Models.CartItem GetCartItemByProductIdAndCustomerId(int productId, int customerId) {
            return base.Channel.GetCartItemByProductIdAndCustomerId(productId, customerId);
        }
        
        public System.Threading.Tasks.Task<KingMarket.Model.Models.CartItem> GetCartItemByProductIdAndCustomerIdAsync(int productId, int customerId) {
            return base.Channel.GetCartItemByProductIdAndCustomerIdAsync(productId, customerId);
        }
        
        public void CreateCartItem(KingMarket.Model.Models.CartItem myObject) {
            base.Channel.CreateCartItem(myObject);
        }
        
        public System.Threading.Tasks.Task CreateCartItemAsync(KingMarket.Model.Models.CartItem myObject) {
            return base.Channel.CreateCartItemAsync(myObject);
        }
        
        public void EditCartItem(KingMarket.Model.Models.CartItem myObject) {
            base.Channel.EditCartItem(myObject);
        }
        
        public System.Threading.Tasks.Task EditCartItemAsync(KingMarket.Model.Models.CartItem myObject) {
            return base.Channel.EditCartItemAsync(myObject);
        }
        
        public void DeleteCartItem(int id) {
            base.Channel.DeleteCartItem(id);
        }
        
        public System.Threading.Tasks.Task DeleteCartItemAsync(int id) {
            return base.Channel.DeleteCartItemAsync(id);
        }
    }
}
