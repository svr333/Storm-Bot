using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Globalization;
using Storm.Resources;
using Storm.Core;

namespace Storm.Helpers
{
    public static class EmbedHelper
    {
        public static Embed CreateEmbed(CommandInfo commandInfo)
        {
            var nl = Environment.NewLine;
            var embed = new EmbedBuilder()
                .WithTitle($"{Config.bot.crossEmote}   Incorrect Usage")
                .WithDescription($"**Name:** {CultureInfo.CurrentCulture.TextInfo.ToTitleCase(commandInfo.Name.ToLower())}{nl}" +
                $"**Summary:** {commandInfo.Summary}{nl}" +
                $"**Usage:** {Config.bot.cmdPrefix}{commandInfo.Name} {string.Join(" ", commandInfo.Parameters)}{nl}" +
                $"**Aliases:** `{string.Join(", ", commandInfo.Aliases)}`")
                .WithColor(Global.GetRandomColor())
                .WithCurrentTimestamp();
            return embed.Build();
        }

        public static IEnumerable<string> GetCommandParameters(CommandInfo command)
        {
            var parameters = command.Parameters;
            var optionalTemplate = "<{0}>";
            var mandatoryTemplate = "[{0}]";
            List<string> parametersFormatted = new List<string>();

            foreach (var parameter in parameters)
            {
                if (parameter.IsOptional)
                    parametersFormatted.Add(string.Format(optionalTemplate, parameter.Name));
                else
                    parametersFormatted.Add(string.Format(mandatoryTemplate, parameter.Name));
            }

            return parametersFormatted;
        }
    }
}
