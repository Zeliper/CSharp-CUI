string token = "Mzc3MjY0NTE0NDU2MDkyNjgy.WgEI9Q.JDaQ65_sPauaKgxqLUf92g8I78E";
DiscordSocketClient _client = new DiscordSocketClient();
_client.Log += Log;
await _client.LoginAsync(TokenType.Bot, token);
await _client.StartAsync();
_client.Ready+= mainFunction;

Task? mainFunction()
{
  var _channel = _client.GetChannel(933233704556441634) as IMessageChannel;
  var eb = new EmbedBuilder()
  {
    Title = "ㅎ",
    Color = 0x00FFFF
  };
  var builder = new ComponentBuilder();
  var selMenu = new SelectMenuBuilder();
  List<SelectMenuOptionBuilder> options = new List<SelectMenuOptionBuilder>();
  options.Add(new SelectMenuOptionBuilder()
  {
    Label = "만들",
    Description = "유이이잉",
    Value = "??",
    Emote = Emoji.Parse(":white_check_mark:"),
    IsDefault = false,
  });
  selMenu.WithOptions(options);
  builder.WithSelectMenu("row_0_select_0",options,"PH");
  _client.SelectMenuExecuted += (e) =>
  {
    _channel.SendMessageAsync(e.Data.CustomId.ToString());
    _channel.SendMessageAsync(e.Data.Type.ToString());
    e.DeferAsync(false);
    return Task.CompletedTask;
  };
  _channel.SendMessageAsync(null, false, eb.Build(),null,null,null,builder.Build());

  //  const lib = require('lib')({ token: process.env.STDLIB_SECRET_TOKEN});

  //await lib.discord.channels['@0.2.0'].messages.create({
  //  "channel_id": `${ context.params.event.channel_id}`,
  //  "content": "",
  //  "tts": false,
  //  "components": [
  //    {
  //    "type": 1,
  //      "components": [
  //        {
  //      "custom_id": `row_0_select_0`,
  //          "options": [
  //            {
  //        "label": `만들어줘!`,
  //              "value": `ㅁㄴㅇㄻㄴㄹㅇ`,
  //              "description": `ㅁㄴㅇㄻㄴㅇㄹ22`,
  //              "default": true
  //            }
  //          ],
  //          "min_values": 1,
  //          "max_values": 1,
  //          "type": 3
  //        }
  //      ]
  //    }
  //  ],


  Console.WriteLine();
  return null;
}


await Task.Delay(-1);
Task? Log(LogMessage arg)
{
  Console.WriteLine(arg.Message);
  return null;
}
