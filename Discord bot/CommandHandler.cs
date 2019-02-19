using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Discord.Commands;
using System.Threading.Tasks;

namespace Discord_bot
{
    class CommandHandler
    {
        DiscordSocketClient _client;
        CommandService _serverice;

        public async Task InutianlizeAsync(DiscordSocketClient client)
        {
            _client = client;
            _serverice = new CommandService();
            await _serverice.AddModulesAsync(Assembly.GetEntryAssembly());
            client.MessageReceived += HandleCommandAsync;
        }

        private async Task HandleCommandAsync(SocketMessage s)
        {
            var msg = s as SocketUserMessage;
            if (msg == null) return;
            var context = new SocketCommandContext(_client, msg);
            int argPos = 0;
            if (msg.HasStringPrefix(Config.bot.cmdPrefix, ref argPos)
                || msg.HasMentionPrefix(_client.CurrentUser, ref argPos))
            {
                var result = await _serverice.ExecuteAsync(context, argPos);
                if(!result.IsSuccess && result.Error != CommandError.UnknownCommand)
                {
                    Console.WriteLine(result.ErrorReason);
                }
            }
        }
    }
}