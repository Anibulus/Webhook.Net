# Webhook.Net

Webhook sample in .Net C#. This webhook saves the information in memory. It's just a sample server-client project.

## How to run
To watch it work use this command to run the server: 
```bash
dotnet run --project Webhook.Net
```

Then run the client
```bash
dotnet run --project Webhook.Client.Net/
```

You can use curl to trigger the action
```bash
curl -X POST http://localhost:5278/publish      -H "C
ontent-Type: application/json"      -d '{
           "Topic": "item.new",
           "Message": {
               "Name": "Some Item",
               "Price": "2.55"
           }
         }'
```

## To Do 
Add webhook service to a database model