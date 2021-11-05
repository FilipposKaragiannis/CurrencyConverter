using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CurrencyConverter.Application;
using CurrencyConverter.Application.Models;

namespace CurrencyConverter.Presentation
{
    public interface IScreenCallbacks
    {
        ConversionSummary Convert(string source, string target, int amount);
    }

    public interface IScreen
    {
        void Register(IScreenCallbacks callbacks);
    }

    public class Screen : IScreen
    {
        private readonly IInputOutput         _io;
        private readonly IExchangeRateLocator _rateLocator;
        private          IScreenCallbacks     _callbacks;

        public Screen(IExchangeRateLocator rateLocator, IInputOutput io)
        {
            _rateLocator = rateLocator;
            _io          = io;
        }

        public void Register(IScreenCallbacks callbacks)
        {
            _callbacks = callbacks;
        }

        public void Start()
        {
            _io.WriteInfo("Welcome!");

            var (assets, assetList) = ConstructAssets();

            if(!assets.Any())
                return;

            var source = GetSelection(assets, assetList);

            var target = GetTarget(source);

            var amount = GetAmount();

            var result = _callbacks.Convert(source, target, amount);

            DisplayResult(result);
        }

        private void DisplayResult(ConversionSummary result)
        {
            _io.WriteInfo($"{result.SourceAsset.Amount} of {result.SourceAsset.Ccy} convert to " +
                      $"{result.DestAsset.Amount} of {result.DestAsset.Ccy}.");

            if(result.Change > 0)
                _io.WriteInfo($"You also got {result.Change:F4} change in {result.SourceAsset.Ccy}.\n");

            ConsoleKey response;
            do
            {
                _io.WriteEvent("Want another conversion ?? (y/N)");
                response = Console.ReadKey(false).Key; // true is intercept key (dont show), false is show
                if(response != ConsoleKey.Enter)
                    Console.WriteLine();
            }
            while(response != ConsoleKey.Y && response != ConsoleKey.N);

            var confirmed = response == ConsoleKey.Y;

            if(confirmed)
                Start();
            else
                _io.WriteInfo("Thanks for using this converter. Bye!!!");
        }

        private int GetAmount()
        {
            _io.WriteEvent("Provide amount to convert");
            while(true)
            {
                if(int.TryParse(_io.Read(), out var amount))
                    return amount;

                _io.WriteError("Expecting an integer number. Try again...");
            }
        }

        private string GetTarget(string source)
        {
            var targets = _rateLocator.Targets(source).ToArray();

            var table = ConstructTable(targets, "Targets");

            return GetSelection(targets, table);
        }

        private static string ConstructTable(IEnumerable<string> items, string title)
        {
            var sb    = new StringBuilder($"------- {title} -------\n");
            var count = 1;

            foreach(var asset in items)
                sb.AppendLine($"{count++}. {asset.ToUpper()}");
            return sb.ToString();
        }

        private (string[], string) ConstructAssets()
        {
            var assets = _rateLocator.Assets.ToArray();

            if(!assets.Any())
                _io.WriteEvent("No Available conversions for you. Bye!!");

            var table = ConstructTable(assets, "Assets");
            return (assets, table);
        }

        private string GetSelection(IReadOnlyList<string> assets, string assetList)
        {
            string source;
            while(true)
            {
                _io.WriteEvent("Select an asset to convert: \n");
                _io.WriteEvent(assetList);

                var input = _io.Read();

                if(int.TryParse(input, out var option))
                {
                    if(option > assets.Count)
                    {
                        _io.WriteError("Invalid source. Select a new source");
                        continue;
                    }

                    source = assets[option - 1];
                }
                else if(assets.Contains(input))
                {
                    source = input;
                }
                else
                {
                    _io.WriteError("Invalid source. Select a new source");
                    continue;
                }

                break;
            }

            return source;
        }
    }
}
