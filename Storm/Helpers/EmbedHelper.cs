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
            var RandomizeColor = new Random().Next(Lists.colorsArray.Length);
            var nl = Environment.NewLine;
            var embed = new EmbedBuilder()
                .WithTitle($"{Config.bot.crossEmote}   Incorrect Usage")
                .WithDescription($"**Name:** {CultureInfo.CurrentCulture.TextInfo.ToTitleCase(commandInfo.Name.ToLower())}{nl}" +
                $"**Summary:** {commandInfo.Summary}{nl}" +
                $"**Usage:** {Config.bot.cmdPrefix}{commandInfo.Name} {String.Join(" ", commandInfo.Parameters)}{nl}" +
                $"**Aliases:** `{String.Join(", ", commandInfo.Aliases)}`")
                .WithColor(Lists.colorsArray[RandomizeColor])
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
                    parametersFormatted.Add(String.Format(optionalTemplate, parameter.Name));
                else
                    parametersFormatted.Add(String.Format(mandatoryTemplate, parameter.Name));
            }

            return parametersFormatted;
        }
    }
}
