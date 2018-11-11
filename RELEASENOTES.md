# Release Notes

##[v2.0.2](https://github.com/NanotechComputers/IPaygateService/releases/tag/v2.0.2)
            
* Refactor Project Structure
* Add Unit Tests for create, query and refund transaction methods to solution

##[v2.0.1](https://github.com/NanotechComputers/IPaygateService/releases/tag/v2.0.1)
            
* Add Travis-ci build config
* Automatically push new versions to Nuget
* Add Codescene to repo to monitor code complexity
* Minor Code refactoring in PaygateService.cs for smaller code footprint

##[v2.0.0](https://github.com/NanotechComputers/IPaygateService/releases/tag/v2.0.0)

Important Notes: This version is not backwards compatible with existing code utilising previous versions
You'll need to change the previously passed string parameters to GUID's where PayRequestId was passed as this is now used for the Merchant Reference

New Features:
* Added ability to void a transaction

Fixes:
* Return a proper exception when an error is returned from PayGate

Changes:
* Service is now added as Scoped in .net core projects rather than Transient
* Updated QueryTransaction to allow passing multiple parameters - PayGateRequestId, TransactionId and Reference
* Updated SettleTransaction to allow passing multiple parameters - TransactionId and Reference
* Updated RefundTransaction to allow passing multiple parameters - PayGateRequestId, TransactionId and Reference
* Added XML Comments to Methods

##[v1.1.0](https://github.com/NanotechComputers/IPaygateService/releases/tag/v1.0.1)
* Added ability to settle a transaction
* Added ability to refund a transaction

##[v1.0.1](https://github.com/NanotechComputers/IPaygateService/releases/tag/v1.0.1) 
* Added ability to create a transaction
* Added ability to query a transaction
* Added ability to verify a transaction's checksum