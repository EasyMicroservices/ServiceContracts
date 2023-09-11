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
            PorjectName = Assembly.GetEntryAssembly()?.GetName()?.Name;
        }

        /// <summary>
        /// 
        /// </summary>
        public string ServieRouteAddress { get; set; }
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
        public string PorjectName { get; set; }

        /// <summary>
        /// Get fast result for debugger
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"ServieRouteAddress: {ServieRouteAddress}\r\nMethodName: {MethodName}\r\n$Details: {PorjectName}\r\nPorjectName: {PorjectName}";
        }
    }
}
