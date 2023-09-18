# ServiceContracts
Contracts between services

Install packages:

[![NuGet](https://img.shields.io/badge/EasyMicroservices-ServiceContracts-orange.svg)](https://www.nuget.org/packages/EasyMicroservices.ServiceContracts/)

ServiceContract lets you manage your service responses more quickly.

![image](https://github.com/EasyMicroservices/ServiceContracts/assets/5262218/929847a3-cd9e-4ff0-b103-76d1094c6214)

Easy return:

```csharp
public class MyController : ControllerBase
{
    //...
    
        [HttpPost]
        public async Task<MessageContract<long>> Add(CreateFaqRequestContract request)
        {
            //Do stuff
            return 1452;
        }
}
```

Easy cast:

```csharp
public class MyController : ControllerBase
{
    //...
    
        [HttpPost]
        public async Task<MessageContract<long>> Add(CreateFaqRequestContract request)
        {
            MessageContract<string> serviceResult = //service stuff;
            if (!serviceResult)
                return serviceResult.ToContract<long>();
            //Do stuff
            return 1452;
        }
}
```

Easy convert:

```csharp
public class MyController : ControllerBase
{
    //...
    
        [HttpPost]
        public Task<MessageContract<long>> Add(CreateFaqRequestContract request)
        {
            Task<MessageContract<string>> serviceResult = //service stuff;
            return serviceResult.ToContract(x => long.Parse(x));
        }
}
```
