![Logo](https://raw.githubusercontent.com/ntrprs/NTRPRS.NLog.Slack/master/nuget.png)
NTRPRS.NLog.Slack  
============
[![Build status](https://ci.appveyor.com/api/projects/status/bmdsm0gqsd5luk79?svg=true)](https://ci.appveyor.com/project/leobuskin/ntrprs-nlog-slack) [![NuGet package](https://img.shields.io/badge/nuget-v5.0-blue.svg)](https://www.nuget.org/packages/NTRPRS.NLog.Slack)  
An NLog target for Slack - your logs in one place and instantly searchable, everywhere.  
Based on [Paul Price](https://github.com/eth0izzle/NLog.Slack) & [Cyril Gandon](https://github.com/cyrilgandon/NLogToSlack) projects  

Use default Features
![NTRPRS.NLog.Slack](res/example.png)

Or create your own attachment

![NTRPRS.NLog.Slack](res/exampleWithAuthor.png)
![NTRPRS.NLog.Slack](res/exampleWithImage.png)

Installation
============
Via [NuGet](https://www.nuget.org/packages/NTRPRS.NLog.Slack/): ```Install-Package NTRPRS.NLog.Slack```

... or just build it yourself!

Usage
=====
1. Create a [new Incoming Webhook integration](https://slack.com/apps/A0F7XDUAZ-incoming-webhooks).
2. Configure NLog to use `NTRPRS.NLog.Slack`:

### NLog.config

```xml
<?xml version="1.0" encoding="utf-8" ?>
<nlog xmlns="http://www.nlog-project.org/schemas/NLog.xsd"
      xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">

  <extensions>
    <add assembly="NTRPRS.NLog.Slack" />
  </extensions>

  <targets async="true">
    <target xsi:type="Slack"
            name="slackTarget"
            layout="${message}"
            webHookUrl="https://hooks.slack.com/services/%your%/%tokens%/%here%"
            excludeLevel="false"
            embed="true" />
  </targets>

  <rules>
    <logger name="*" minlevel="Debug" writeTo="slackTarget" />
  </rules>
</nlog>
```

Note: it's recommended to set ```async="true"``` on `targets` so if the HTTP call to Slack fails or times out it doesn't slow down your application.

### Programmatically 

```
var config = new LoggingConfiguration();
var slackTarget = new SlackTarget
{
      Layout = "${message}",
      WebHookUrl = "https://hooks.slack.com/services/%your%/%tokens%/%here%"
};

config.AddTarget("slack", slackTarget);

var slackTargetRules = new LoggingRule("*", LogLevel.Debug, slackTarget);
config.LoggingRules.Add(slackTargetRules);

LogManager.Configuration = config;
```

And you're good to go!

### Configuration Options

Key         | Description
-----------:| -----------
WebHookUrl  | Grab your Webhook URL (__with the token__) from your Incoming Webhooks integration in Slack
ExcludeLevel| Set to true to just send the NLog layout text (no colors, etc)
Embed       | Set to true to embed NLog layout text into attachment
