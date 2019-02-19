using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using NReco.ImageGenerator;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Discord_bot.Modules
{
    public class Miss : ModuleBase<SocketCommandContext>
    {
            [Command("neko"), Alias("네코"), Summary("네코 사진")]
        public async Task neko()
        {

            using (WebClient client = new WebClient())
            {
                var neko = client.DownloadString("https://nekos.life/api/v2/img/neko");
                 JObject jsonobj = JObject.Parse(neko);
                var nekourl = jsonobj["url"].ToString();


                var embed = new EmbedBuilder();
                embed.WithImageUrl(nekourl);
                embed.WithColor(Color.Gold);
                await Context.Channel.SendMessageAsync("", embed: embed);
            }
        }
        [Command("hello"), Alias("안녕"), Summary("사진")]
        public async Task Hello(string color = "red")
        {
            string css = "<style\n    h1\n       color" + color + ";\n     }\n<style>\n";
            string html = String.Format("<h1>Hello {0}!</h1>", Context.User.Username);
            var converter = new HtmlToImageConverter
            {
                Width = 250,
                Height = 70
            };
            var jpeBytes = converter.GenerateImage(css + html, NReco.ImageGenerator.ImageFormat.Jpeg);
            await Context.Channel.SendFileAsync(new MemoryStream(jpeBytes), "hello.jpe");
        }
        [Command("say"), Alias("따라해"), Summary("이건 봇이 따라 하는 거다")]
        public async Task Echo([Remainder]string message)
        {
            var embed = new EmbedBuilder();
            embed.WithTitle("명령어 쓴 사람: " + Context.User.Username);
            embed.WithDescription(message);
            embed.WithColor(new Color(0, 255, 0));
            await Context.Channel.SendMessageAsync("", false, embed);
           // await Context.User.SendMessageAsync(message);
        }
        [Command("pick"), Alias("뽑기"), Summary("이건 봇이 마음대로 뽑는 거임")]
        public async Task pick([Remainder]string message)
        {
            string[] options = message.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            Random r = new Random();
            string seletion = options[r.Next(0, options.Length)];

            var embed = new EmbedBuilder();
            embed.WithTitle("명령어 쓴 사람: " + Context.User.Username);
            embed.WithDescription(seletion);
            embed.WithColor(new Color(0, 255, 0));
            embed.WithFooter("c#pick 하고 싶은말, 하고 싶은 말하면 됩니다!");
            await Context.Channel.SendMessageAsync("", false, embed);
            // await Context.User.SendMessageAsync(message);
        }
    }
}
