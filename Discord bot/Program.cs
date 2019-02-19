using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Discord.WebSocket;
using Discord;

namespace Discord_bot
{
    class Program
    {
        DiscordSocketClient _client;
        CommandHandler _handler;

        static void Main(string[] args)
            => new Program().StartAsync().GetAwaiter().GetResult();

        public async Task StartAsync()
        {

            if (Config.bot.token == "" || Config.bot.token == null) return;
            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Verbose
            });
            _client.Log += Log;
            _handler = new CommandHandler();
            await _client.LoginAsync(TokenType.Bot, Config.bot.token);
            await _client.StartAsync();
            await _client.SetGameAsync("ㅎㅇ");
            _handler = new CommandHandler();
            await _handler.InutianlizeAsync(_client);
            await Task.Delay(-1);
        }

        private async Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.Message);
         
        }
    }
}
