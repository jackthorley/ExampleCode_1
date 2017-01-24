/*
 * This code would ideally be Integration tested as it appears as though it's interacting externally to the core process
 * This could be 'mocked' out in a way that wouldn't interact with the live implementation if nessaccery
 */
 
namespace App
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName = "App.ICustomerCreditService")]
    public interface ICustomerCreditService
    {

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/ICustomerCreditService/GetCreditLimit", ReplyAction = "http://tempuri.org/ICustomerCreditService/GetCreditLimitResponse")]
        int GetCreditLimit(string firstname, string surname, System.DateTime dateOfBirth);
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ICustomerCreditServiceChannel : ICustomerCreditService, System.ServiceModel.IClientChannel
    {
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CustomerCreditServiceClient : System.ServiceModel.ClientBase<ICustomerCreditService>, ICustomerCreditService
    {

        public CustomerCreditServiceClient()
        {
        }

        public CustomerCreditServiceClient(string endpointConfigurationName) :
            base(endpointConfigurationName)
        {
        }

        public CustomerCreditServiceClient(string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public CustomerCreditServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public CustomerCreditServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
        }

        public int GetCreditLimit(string firstname, string surname, System.DateTime dateOfBirth)
        {
            return base.Channel.GetCreditLimit(firstname, surname, dateOfBirth);
        }
    }
}
