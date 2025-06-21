namespace WodItEasy.Common.Application.Settings;

public class MessageQueueSettings(
    string host,
    string userName,
    string password)
{
    public string Host { get; } = host;

    public string UserName { get; } = userName;

    public string Password { get; } = password;
}