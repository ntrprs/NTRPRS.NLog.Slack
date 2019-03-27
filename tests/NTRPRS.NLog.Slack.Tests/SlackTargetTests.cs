using System;
using FluentAssertions;
using NLog;
using Xunit;

namespace NTRPRS.NLog.Slack.Tests
{
    public class SlackTargetTests
    {
        [Fact]
        public void DefaultSettings_ShouldBeCorrect()
        {
            var slackTarget = new TestableSlackTarget();
            slackTarget.WebHookUrl.Should().Be(null);
            slackTarget.ExcludeLevel.Should().BeFalse();
        }

        [Fact]
        public void CustomSettings_ShouldBeCorrect()
        {
            const string webHookUrl = "http://slack.is.awesome.com";
            const bool excludeLevel = true;

            var slackTarget = new TestableSlackTarget
            {
                WebHookUrl = webHookUrl,
                ExcludeLevel = excludeLevel
            };

            var logEvent = new LogEventInfo { Level = LogLevel.Info, Message = "This is a ${level} message" };

            slackTarget.WebHookUrl.Should().Be(webHookUrl);
            slackTarget.ExcludeLevel.Should().BeTrue();
        }

        [Fact]
        public void InitializeTarget_EmptyWebHookUrl_ShouldThrowException()
        {
            var slackTarget = new TestableSlackTarget();

            Assert.Throws<ArgumentOutOfRangeException>(() => slackTarget.Initialize());
        }

        [Fact]
        public void InitializeTarget_IncorrectWebHookUrl_ShouldThrowException()
        {
            var slackTarget = new TestableSlackTarget
            {
                WebHookUrl = "IM A BIG FAT PHONY"
            };

            Assert.Throws<ArgumentOutOfRangeException>(() => slackTarget.Initialize());
        }

        [Fact]
        public void InitializeTarget_Correct_TargetShouldInitialize()
        {
            var slackTarget = new TestableSlackTarget
            {
                WebHookUrl = "http://slack.is.awesome.com"
            };

            slackTarget.Initialize();
        }
    }
}