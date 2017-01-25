dotnet restore .\tests\NTRPRS.NLog.Slack.Tests\ 
dotnet build .\tests\NTRPRS.NLog.Slack.Tests\ --configuration release 

dotnet test .\tests\NTRPRS.NLog.Slack.Tests\  --configuration release