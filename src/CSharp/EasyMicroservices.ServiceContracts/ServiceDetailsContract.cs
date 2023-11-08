using System.Reflection;

namespace EasyMicroservices.ServiceContracts
{
    /// <summary>
    /// 
    /// </summary>
    public class ServiceDetailsContract
    {
        /// <summary>
        /// 
        /// </summary>
        public ServiceDetailsContract()
        {
            ProjectName = Assembly.GetEntryAssembly()?.GetName()?.Name;
        }

        /// <summary>
        /// 
        /// </summary>
        public string ServiceRouteAddress { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string MethodName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// Get fast result for debugger
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"ServiceRouteAddress: {ServiceRouteAddress}\r\nMethodName: {MethodName}\r\n$Details: {ProjectName}\r\nProjectName: {ProjectName}";
        }
    }
}
