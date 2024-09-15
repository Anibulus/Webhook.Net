namespace Services;

public record PublishRequest(string Topic, object Message);

/// <summary>
/// This is the model that contains the necessary information to inform a single client subscribed to a <strong>topic</strong>. It must contain the topic and the callback URI.
/// </summary>
/// <param name="Topic">This is the topic of the subscription. It usually represents a domain event, for example, a new item being created.</param>
/// <param name="Callback">The callback URI is what the server sends the payload to. It is the responsibility of the client to implement an API endpoint on this URI and ensure that it accepts the payload the server sends.</param>
public record Subscription(string Topic, string Callback);

public class WebhookService 
{
    private readonly List<Subscription> _subscriptions = new List<Subscription>();
    private readonly HttpClient _httpClient = new();
    public WebhookService()
    {

    }

    /// <summary>
    /// Add suscription
    /// </summary>
    /// <param name="subscription"></param>
    public void Subscribe(Subscription subscription)
    {
        Console.WriteLine($"Add '{subscription.Topic}' from '{subscription.Callback}' to a suscription");
        _subscriptions.Add(subscription);
    }

    /// <summary>
    /// Trigger
    /// This is what triggers the webhook. Domain-wise, it is an event on a certain topic. Technically-wise, It can be anything from a simple in-process method call to an event handler that consumed a message from another part of the system.
    /// </summary>
    /// <param name="topic"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public async Task PublishMessage(string topic, object message)
    {
        Console.WriteLine($"Action triggered, sending '{topic}'");
        var suscribedWebhooks = _subscriptions.Where(x=>x.Topic == topic);

        foreach(var webhook in suscribedWebhooks)
        {
            Console.WriteLine($"'{webhook.Callback}'");
            await _httpClient.PostAsJsonAsync(webhook.Callback, message);
        }
    }
}