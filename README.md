[![Build Status](https://travis-ci.org/NanotechComputers/IPaygateService.svg?branch=master)](https://travis-ci.org/NanotechComputers/IPaygateService)  Linux & OSX (Travis)

# IPaygateService

.NET Standard Service and Wrapper for the [Paygate](https://www.paygate.co.za) Payment Gateway. 

### Quick Start

Install the [NuGet package](https://www.nuget.org/packages/IPaygateService/)
```powershell
Install-Package IPaygateService
```

Next, you will need to provide IPaygateService with your URL, MerchantId and MerchantSecret key in code, build the request model and create the transaction on PayGate.

### Examples

####Console Application:

In Program.cs, call:

```CSharp
const string url = "https://secure.paygate.co.za/payhost/process.trans";
const string merchantId = "10011064270";
const string merchantSecret = "test";

var paygate = new PaygateService(url, merchantId, merchantSecret);

var data = new CreateTransactionModel{
    ...
};

var response = paygate.CreateTransaction(data);

//TODO: Process the response received from PayGate

```
Please see example console application for more info/options

####.net core MVC Web Application:

In Startup.cs, call:
```CSharp
services.AddPaygate(options =>
    {
        options.MerchantId = "10011064270";
        options.MerchantSecret = "test";
        options.Url = "https://secure.paygate.co.za/payhost/process.trans";
    });
```

In your Controller's Constructor, use dependency injection to access the PaygateService
```CSharp
private IPaygateService _paygateService;

public HomeController(IPaygateService paygateService)
{
    _paygateService = paygateService;
}
```

In your controller method, call:
```CSharp
var data = new CreateTransactionModel{
    ...
};

var response = _paygateService.CreateTransaction(data);

//TODO: Process the response received from PayGate
```

If you used 3D secure then in your controller's redirect return method, call:
```CSharp
var dict = Request.Form.ToDictionary(x => x.Key, x => x.Value.ToString());
dict.TryGetValue("PAY_REQUEST_ID", out var paygateRequestId);

var queryResponse = _paygateService.QueryTransaction(paygateRequestId); //Paygate does not return userdefinedfields on 3d secure redirects.

var validChecksum = _paygateService.VerifyTransaction(dict, queryResponse.Reference); //the reference should ideally be stored locally for the verification but for this example we will use what is returned from Paygate
Debug.WriteLine(validChecksum ? "Checksum Matches" : "Checksum does not match, possible tampering");

//TODO: Process the queryResponse received from PayGate
```

Please see example .net core Web Application for more info/options

[![](https://codescene.io/projects/3655/status.svg) Get more details at **codescene.io**.](https://codescene.io/projects/3655/jobs/latest-successful/results)

