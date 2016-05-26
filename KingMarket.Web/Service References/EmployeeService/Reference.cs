﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KingMarket.Web.EmployeeService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="EmployeeService.IEmployeeService")]
    public interface IEmployeeService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEmployeeService/GetEmployees", ReplyAction="http://tempuri.org/IEmployeeService/GetEmployeesResponse")]
        System.Collections.Generic.List<KingMarket.Model.Models.Employee> GetEmployees();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEmployeeService/GetEmployees", ReplyAction="http://tempuri.org/IEmployeeService/GetEmployeesResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<KingMarket.Model.Models.Employee>> GetEmployeesAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEmployeeService/GetEmployee", ReplyAction="http://tempuri.org/IEmployeeService/GetEmployeeResponse")]
        KingMarket.Model.Models.Employee GetEmployee(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEmployeeService/GetEmployee", ReplyAction="http://tempuri.org/IEmployeeService/GetEmployeeResponse")]
        System.Threading.Tasks.Task<KingMarket.Model.Models.Employee> GetEmployeeAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEmployeeService/CreateEmployee", ReplyAction="http://tempuri.org/IEmployeeService/CreateEmployeeResponse")]
        void CreateEmployee(KingMarket.Model.Models.Employee myObject);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEmployeeService/CreateEmployee", ReplyAction="http://tempuri.org/IEmployeeService/CreateEmployeeResponse")]
        System.Threading.Tasks.Task CreateEmployeeAsync(KingMarket.Model.Models.Employee myObject);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEmployeeService/EditEmployee", ReplyAction="http://tempuri.org/IEmployeeService/EditEmployeeResponse")]
        void EditEmployee(KingMarket.Model.Models.Employee myObject);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEmployeeService/EditEmployee", ReplyAction="http://tempuri.org/IEmployeeService/EditEmployeeResponse")]
        System.Threading.Tasks.Task EditEmployeeAsync(KingMarket.Model.Models.Employee myObject);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEmployeeService/DeleteEmployee", ReplyAction="http://tempuri.org/IEmployeeService/DeleteEmployeeResponse")]
        void DeleteEmployee(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEmployeeService/DeleteEmployee", ReplyAction="http://tempuri.org/IEmployeeService/DeleteEmployeeResponse")]
        System.Threading.Tasks.Task DeleteEmployeeAsync(int id);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IEmployeeServiceChannel : KingMarket.Web.EmployeeService.IEmployeeService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class EmployeeServiceClient : System.ServiceModel.ClientBase<KingMarket.Web.EmployeeService.IEmployeeService>, KingMarket.Web.EmployeeService.IEmployeeService {
        
        public EmployeeServiceClient() {
        }
        
        public EmployeeServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public EmployeeServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public EmployeeServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public EmployeeServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Collections.Generic.List<KingMarket.Model.Models.Employee> GetEmployees() {
            return base.Channel.GetEmployees();
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<KingMarket.Model.Models.Employee>> GetEmployeesAsync() {
            return base.Channel.GetEmployeesAsync();
        }
        
        public KingMarket.Model.Models.Employee GetEmployee(int id) {
            return base.Channel.GetEmployee(id);
        }
        
        public System.Threading.Tasks.Task<KingMarket.Model.Models.Employee> GetEmployeeAsync(int id) {
            return base.Channel.GetEmployeeAsync(id);
        }
        
        public void CreateEmployee(KingMarket.Model.Models.Employee myObject) {
            base.Channel.CreateEmployee(myObject);
        }
        
        public System.Threading.Tasks.Task CreateEmployeeAsync(KingMarket.Model.Models.Employee myObject) {
            return base.Channel.CreateEmployeeAsync(myObject);
        }
        
        public void EditEmployee(KingMarket.Model.Models.Employee myObject) {
            base.Channel.EditEmployee(myObject);
        }
        
        public System.Threading.Tasks.Task EditEmployeeAsync(KingMarket.Model.Models.Employee myObject) {
            return base.Channel.EditEmployeeAsync(myObject);
        }
        
        public void DeleteEmployee(int id) {
            base.Channel.DeleteEmployee(id);
        }
        
        public System.Threading.Tasks.Task DeleteEmployeeAsync(int id) {
            return base.Channel.DeleteEmployeeAsync(id);
        }
    }
}