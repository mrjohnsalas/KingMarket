﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KingMarket.Web.ProductPhotoService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ProductPhotoService.IProductPhotoService")]
    public interface IProductPhotoService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductPhotoService/GetProductPhoto", ReplyAction="http://tempuri.org/IProductPhotoService/GetProductPhotoResponse")]
        KingMarket.Model.Models.ProductPhoto GetProductPhoto(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductPhotoService/GetProductPhoto", ReplyAction="http://tempuri.org/IProductPhotoService/GetProductPhotoResponse")]
        System.Threading.Tasks.Task<KingMarket.Model.Models.ProductPhoto> GetProductPhotoAsync(int id);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IProductPhotoServiceChannel : KingMarket.Web.ProductPhotoService.IProductPhotoService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ProductPhotoServiceClient : System.ServiceModel.ClientBase<KingMarket.Web.ProductPhotoService.IProductPhotoService>, KingMarket.Web.ProductPhotoService.IProductPhotoService {
        
        public ProductPhotoServiceClient() {
        }
        
        public ProductPhotoServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ProductPhotoServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ProductPhotoServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ProductPhotoServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public KingMarket.Model.Models.ProductPhoto GetProductPhoto(int id) {
            return base.Channel.GetProductPhoto(id);
        }
        
        public System.Threading.Tasks.Task<KingMarket.Model.Models.ProductPhoto> GetProductPhotoAsync(int id) {
            return base.Channel.GetProductPhotoAsync(id);
        }
    }
}
