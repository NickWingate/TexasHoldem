using System;
using System.Collections.Generic;
using System.Linq;
using Spectre.Console;
using TexasHoldem.ConsoleUI.Extensions;
using TexasHoldem.Domain.Entities;

namespace TexasHoldem.ConsoleUI.Services
{
	public class ConsoleOutputService : IConsoleOutputService
	{
		public void OutputChips(List<Player> players)
		{
			var table = new Table()
				.AddColumn("[lightseagreen bold]Player[/]")
				.AddColumn("[darkseagreen1 bold]Chips[/]");
			foreach (var player in players)
			{
				table.AddRow(player.Name, player.ChipCount.ToString());
			}
			AnsiConsole.Render(table);
		}

		public void OutputCurrentStage(string stage)
		{
			AnsiConsole.Render(new Rule($"[yellow]{stage}[/]")
				.LeftAligned());
		}

		public void OutputPlayerHands(List<Player> players)
		{
			var table = new Table()
				.AddColumn("[lightseagreen bold]Player[/]")
				.AddColumn("[teal]Hand[/]")
				.AddColumn("[teal]Best Hand[/]")
				.BorderStyle(new Style(Color.Yellow));
			foreach (var player in players)
			{
				table.AddRow(player.Name, string.Join(", ", player.Hand), player.BestHand.ToString());
			}
			AnsiConsole.Render(table);
		}

		public void OutputRoles(Player smallBlind, Player bigBlind, Player dealer)
		{
			var table = new Table()
				.AddColumn("[lightseagreen bold]Role[/]")
				.AddColumn("[teal]Player[/]")
				.AddRow("Dealer", dealer.Name)
				.AddRow("Small Blind", smallBlind.Name)
				.AddRow("Big Blind", bigBlind.Name);
			AnsiConsole.Render(table);
		}

		public void OutputPot(Pot pot)
		{
			AnsiConsole.Render(new Table()
				.AddColumn("[lightseagreen bold]Pot Total[/]")
				.AddColumn("[darkseagreen1 bold]Last Bet[/]")
				.AddRow(pot.Chips.ToString(), pot.CurrentBet.ToString()));
		}

		public void OutputPrivateCards(List<Player> players)
		{
			foreach (var player in players)
			{
				var cards = string.Join(", ", player.Hand.Select(c => c.ToMarkupString()));
				AnsiConsole.MarkupLine($"{player.Name}'s Cards:\n{cards}\nEnter to clear");
				Console.ReadLine();
				Console.SetCursorPosition(0, Console.CursorTop - 2);
				Console.Write(new String(' ', Console.BufferWidth));
			}
		}
	}
}