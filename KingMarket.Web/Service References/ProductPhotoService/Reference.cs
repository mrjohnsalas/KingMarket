﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KingMarket.Web.ProductPhotoService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ProductPhotoService.IProductPhotoService")]
    public interface IProductPhotoService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductPhotoService/GetProductPhotosByProductId", ReplyAction="http://tempuri.org/IProductPhotoService/GetProductPhotosByProductIdResponse")]
        System.Collections.Generic.List<KingMarket.Model.Models.ProductPhoto> GetProductPhotosByProductId(int productId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductPhotoService/GetProductPhotosByProductId", ReplyAction="http://tempuri.org/IProductPhotoService/GetProductPhotosByProductIdResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<KingMarket.Model.Models.ProductPhoto>> GetProductPhotosByProductIdAsync(int productId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductPhotoService/GetProductPhotosByProductIdNoContent", ReplyAction="http://tempuri.org/IProductPhotoService/GetProductPhotosByProductIdNoContentRespo" +
            "nse")]
        System.Collections.Generic.List<KingMarket.Model.Models.ProductPhoto> GetProductPhotosByProductIdNoContent(int productId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductPhotoService/GetProductPhotosByProductIdNoContent", ReplyAction="http://tempuri.org/IProductPhotoService/GetProductPhotosByProductIdNoContentRespo" +
            "nse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<KingMarket.Model.Models.ProductPhoto>> GetProductPhotosByProductIdNoContentAsync(int productId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductPhotoService/GetProductPhotosOnlyDefault", ReplyAction="http://tempuri.org/IProductPhotoService/GetProductPhotosOnlyDefaultResponse")]
        System.Collections.Generic.List<KingMarket.Model.Models.ProductPhoto> GetProductPhotosOnlyDefault();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductPhotoService/GetProductPhotosOnlyDefault", ReplyAction="http://tempuri.org/IProductPhotoService/GetProductPhotosOnlyDefaultResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<KingMarket.Model.Models.ProductPhoto>> GetProductPhotosOnlyDefaultAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductPhotoService/GetProductPhoto", ReplyAction="http://tempuri.org/IProductPhotoService/GetProductPhotoResponse")]
        KingMarket.Model.Models.ProductPhoto GetProductPhoto(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductPhotoService/GetProductPhoto", ReplyAction="http://tempuri.org/IProductPhotoService/GetProductPhotoResponse")]
        System.Threading.Tasks.Task<KingMarket.Model.Models.ProductPhoto> GetProductPhotoAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductPhotoService/GetProductPhotoDefaultByProductId", ReplyAction="http://tempuri.org/IProductPhotoService/GetProductPhotoDefaultByProductIdResponse" +
            "")]
        KingMarket.Model.Models.ProductPhoto GetProductPhotoDefaultByProductId(int productId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductPhotoService/GetProductPhotoDefaultByProductId", ReplyAction="http://tempuri.org/IProductPhotoService/GetProductPhotoDefaultByProductIdResponse" +
            "")]
        System.Threading.Tasks.Task<KingMarket.Model.Models.ProductPhoto> GetProductPhotoDefaultByProductIdAsync(int productId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductPhotoService/DeleteProductPhotosByProductId", ReplyAction="http://tempuri.org/IProductPhotoService/DeleteProductPhotosByProductIdResponse")]
        void DeleteProductPhotosByProductId(int productId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductPhotoService/DeleteProductPhotosByProductId", ReplyAction="http://tempuri.org/IProductPhotoService/DeleteProductPhotosByProductIdResponse")]
        System.Threading.Tasks.Task DeleteProductPhotosByProductIdAsync(int productId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductPhotoService/DeleteProductPhoto", ReplyAction="http://tempuri.org/IProductPhotoService/DeleteProductPhotoResponse")]
        void DeleteProductPhoto(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductPhotoService/DeleteProductPhoto", ReplyAction="http://tempuri.org/IProductPhotoService/DeleteProductPhotoResponse")]
        System.Threading.Tasks.Task DeleteProductPhotoAsync(int id);
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
        
        public System.Collections.Generic.List<KingMarket.Model.Models.ProductPhoto> GetProductPhotosByProductId(int productId) {
            return base.Channel.GetProductPhotosByProductId(productId);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<KingMarket.Model.Models.ProductPhoto>> GetProductPhotosByProductIdAsync(int productId) {
            return base.Channel.GetProductPhotosByProductIdAsync(productId);
        }
        
        public System.Collections.Generic.List<KingMarket.Model.Models.ProductPhoto> GetProductPhotosByProductIdNoContent(int productId) {
            return base.Channel.GetProductPhotosByProductIdNoContent(productId);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<KingMarket.Model.Models.ProductPhoto>> GetProductPhotosByProductIdNoContentAsync(int productId) {
            return base.Channel.GetProductPhotosByProductIdNoContentAsync(productId);
        }
        
        public System.Collections.Generic.List<KingMarket.Model.Models.ProductPhoto> GetProductPhotosOnlyDefault() {
            return base.Channel.GetProductPhotosOnlyDefault();
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<KingMarket.Model.Models.ProductPhoto>> GetProductPhotosOnlyDefaultAsync() {
            return base.Channel.GetProductPhotosOnlyDefaultAsync();
        }
        
        public KingMarket.Model.Models.ProductPhoto GetProductPhoto(int id) {
            return base.Channel.GetProductPhoto(id);
        }
        
        public System.Threading.Tasks.Task<KingMarket.Model.Models.ProductPhoto> GetProductPhotoAsync(int id) {
            return base.Channel.GetProductPhotoAsync(id);
        }
        
        public KingMarket.Model.Models.ProductPhoto GetProductPhotoDefaultByProductId(int productId) {
            return base.Channel.GetProductPhotoDefaultByProductId(productId);
        }
        
        public System.Threading.Tasks.Task<KingMarket.Model.Models.ProductPhoto> GetProductPhotoDefaultByProductIdAsync(int productId) {
            return base.Channel.GetProductPhotoDefaultByProductIdAsync(productId);
        }
        
        public void DeleteProductPhotosByProductId(int productId) {
            base.Channel.DeleteProductPhotosByProductId(productId);
        }
        
        public System.Threading.Tasks.Task DeleteProductPhotosByProductIdAsync(int productId) {
            return base.Channel.DeleteProductPhotosByProductIdAsync(productId);
        }
        
        public void DeleteProductPhoto(int id) {
            base.Channel.DeleteProductPhoto(id);
        }
        
        public System.Threading.Tasks.Task DeleteProductPhotoAsync(int id) {
            return base.Channel.DeleteProductPhotoAsync(id);
        }
    }
}
