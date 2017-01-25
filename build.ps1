# restore and builds all projects as release.
# creates NuGet package at \artifacts
dotnet --version

dotnet restore .\src\NTRPRS.NLog.Slack\ 
dotnet pack .\src\NTRPRS.NLog.Slack\  --configuration release   -o artifacts