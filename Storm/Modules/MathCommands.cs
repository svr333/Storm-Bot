using Discord.Commands;
using Discord;
using System.Threading.Tasks;
using System;
using Storm.Resources;

namespace Storm.Modules
{
    public class MathCommands : ModuleBase<SocketCommandContext>
    {
        [Command("addition")]
        [Alias("add")]
        [Summary("Add two numbers.")]
        public async Task AddAsync(float num1, float num2)
        {
            var eb = new EmbedBuilder();
            eb.WithColor(Color.Blue);
            eb.AddField($":package: Equation", $"{num1} + {num2}");
            eb.AddField($"<:DawnCheck:512261208875991041> Answer", $"{num1 + num2}");
            await ReplyAsync("", false, eb.Build());
        }

        [Command("subtract")]
        [Alias("sub", "take", "minus")]
        [Summary("Divide two numbers.")]
        public async Task SubtractAsync(float num1, float num2)
        {
            var eb = new EmbedBuilder();
            eb.WithColor(Color.Blue);
            eb.AddField($":package: Equation", $"{num1} - {num2}");
            eb.AddField($"<:DawnCheck:512261208875991041> Answer", $"{num1 - num2}");
            await ReplyAsync("", false, eb.Build());
        }

        [Command("multiply")]
        [Alias("times")]
        [Summary("Multiply two numbers.")]
        public async Task MultiplyAsync(float num1, float num2)
        {
            var eb = new EmbedBuilder();
            eb.WithColor(Color.Blue);
            eb.AddField($":package: Equation", $"{num1} x {num2}");
            eb.AddField($"<:DawnCheck:512261208875991041> Answer", $"{num1 * num2}");
            await ReplyAsync("", false, eb.Build());
        }

        [Command("divide")]
        [Alias("division")]
        [Summary("Divide two numbers.")]
        public async Task DivideAsync(float num1, float num2)
        {
            var eb = new EmbedBuilder();
            eb.WithColor(Color.Blue);
            eb.AddField($":package: Equation", $"{num1} ÷ {num2}");
            eb.AddField($"<:DawnCheck:512261208875991041> Answer", $"{num1 / num2}");
            await ReplyAsync("", false, eb.Build());
        }

        [Command("random")]
        [Alias("rnd")]
        [Summary("Pick a random number between the two specified numbers.")]
        public async Task PickRandomNumberAsync(int num1, int num2)
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            float result = rnd.Next(num1, (num2 + 1));
            var RandomizeColor = new Random().Next(Lists.colorsArray.Length);
            var eb = new EmbedBuilder();
            eb.AddField(":package: Parameters:", $"Random number between {num1} and {num2}.");
            eb.AddField("<:DawnCheck:512261208875991041> Result", result);
            eb.WithColor(Lists.colorsArray[RandomizeColor]);
            await ReplyAsync("", false, eb.Build());

        }

        [Command("dice")]
        [Alias("die", "roll")]
        [Summary("Rolls a standard six sided die.")]
        public async Task RollDiceAsync()
        {
            await ReplyAsync(Lists.Dice.RandomElement());
        }
    }
}
