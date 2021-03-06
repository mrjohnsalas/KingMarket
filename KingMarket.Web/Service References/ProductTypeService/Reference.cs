﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KingMarket.Web.ProductTypeService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ProductTypeService.IProductTypeService")]
    public interface IProductTypeService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductTypeService/GetProductTypes", ReplyAction="http://tempuri.org/IProductTypeService/GetProductTypesResponse")]
        System.Collections.Generic.List<KingMarket.Model.Models.ProductType> GetProductTypes();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductTypeService/GetProductTypes", ReplyAction="http://tempuri.org/IProductTypeService/GetProductTypesResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<KingMarket.Model.Models.ProductType>> GetProductTypesAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductTypeService/GetProductType", ReplyAction="http://tempuri.org/IProductTypeService/GetProductTypeResponse")]
        KingMarket.Model.Models.ProductType GetProductType(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductTypeService/GetProductType", ReplyAction="http://tempuri.org/IProductTypeService/GetProductTypeResponse")]
        System.Threading.Tasks.Task<KingMarket.Model.Models.ProductType> GetProductTypeAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductTypeService/CreateProductType", ReplyAction="http://tempuri.org/IProductTypeService/CreateProductTypeResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(KingMarket.Model.Models.GeneralException), Action="http://tempuri.org/IProductTypeService/CreateProductTypeGeneralExceptionFault", Name="GeneralException", Namespace="http://schemas.datacontract.org/2004/07/KingMarket.Model.Models")]
        void CreateProductType(KingMarket.Model.Models.ProductType myObject);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductTypeService/CreateProductType", ReplyAction="http://tempuri.org/IProductTypeService/CreateProductTypeResponse")]
        System.Threading.Tasks.Task CreateProductTypeAsync(KingMarket.Model.Models.ProductType myObject);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductTypeService/EditProductType", ReplyAction="http://tempuri.org/IProductTypeService/EditProductTypeResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(KingMarket.Model.Models.GeneralException), Action="http://tempuri.org/IProductTypeService/EditProductTypeGeneralExceptionFault", Name="GeneralException", Namespace="http://schemas.datacontract.org/2004/07/KingMarket.Model.Models")]
        void EditProductType(KingMarket.Model.Models.ProductType myObject);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductTypeService/EditProductType", ReplyAction="http://tempuri.org/IProductTypeService/EditProductTypeResponse")]
        System.Threading.Tasks.Task EditProductTypeAsync(KingMarket.Model.Models.ProductType myObject);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductTypeService/DeleteProductType", ReplyAction="http://tempuri.org/IProductTypeService/DeleteProductTypeResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(KingMarket.Model.Models.GeneralException), Action="http://tempuri.org/IProductTypeService/DeleteProductTypeGeneralExceptionFault", Name="GeneralException", Namespace="http://schemas.datacontract.org/2004/07/KingMarket.Model.Models")]
        void DeleteProductType(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductTypeService/DeleteProductType", ReplyAction="http://tempuri.org/IProductTypeService/DeleteProductTypeResponse")]
        System.Threading.Tasks.Task DeleteProductTypeAsync(int id);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IProductTypeServiceChannel : KingMarket.Web.ProductTypeService.IProductTypeService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ProductTypeServiceClient : System.ServiceModel.ClientBase<KingMarket.Web.ProductTypeService.IProductTypeService>, KingMarket.Web.ProductTypeService.IProductTypeService {
        
        public ProductTypeServiceClient() {
        }
        
        public ProductTypeServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ProductTypeServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ProductTypeServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ProductTypeServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Collections.Generic.List<KingMarket.Model.Models.ProductType> GetProductTypes() {
            return base.Channel.GetProductTypes();
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<KingMarket.Model.Models.ProductType>> GetProductTypesAsync() {
            return base.Channel.GetProductTypesAsync();
        }
        
        public KingMarket.Model.Models.ProductType GetProductType(int id) {
            return base.Channel.GetProductType(id);
        }
        
        public System.Threading.Tasks.Task<KingMarket.Model.Models.ProductType> GetProductTypeAsync(int id) {
            return base.Channel.GetProductTypeAsync(id);
        }
        
        public void CreateProductType(KingMarket.Model.Models.ProductType myObject) {
            base.Channel.CreateProductType(myObject);
        }
        
        public System.Threading.Tasks.Task CreateProductTypeAsync(KingMarket.Model.Models.ProductType myObject) {
            return base.Channel.CreateProductTypeAsync(myObject);
        }
        
        public void EditProductType(KingMarket.Model.Models.ProductType myObject) {
            base.Channel.EditProductType(myObject);
        }
        
        public System.Threading.Tasks.Task EditProductTypeAsync(KingMarket.Model.Models.ProductType myObject) {
            return base.Channel.EditProductTypeAsync(myObject);
        }
        
        public void DeleteProductType(int id) {
            base.Channel.DeleteProductType(id);
        }
        
        public System.Threading.Tasks.Task DeleteProductTypeAsync(int id) {
            return base.Channel.DeleteProductTypeAsync(id);
        }
    }
}
