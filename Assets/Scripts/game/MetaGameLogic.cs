using System;
using System.Collections.Generic;
using game.config;
using Random = UnityEngine.Random;

namespace game
{
    public class MetaGameLogic
    {
        private readonly Dictionary<string, PuzzleConfig> _puzzles = new();

        public MetaGameLogic()
        {
            Random.InitState(1234);
        }
        
        public PuzzleConfig GetPuzzleConfig(string imageId)
        {
            if (!_puzzles.TryGetValue(imageId, out var config))
            {
                config = new PuzzleConfig();

                config.DetailLevels = new List<DetailLevelConfig>();

                var detailLevels = Random.Range(4, 7);

                for (var i = 0; i < detailLevels; i++)
                {
                    config.DetailLevels.Add(new DetailLevelConfig
                    {
                        PiecesCount = (int)Math.Pow(2, i + 3),
                        Price = GetRandomPrice(i)
                    });
                }

                _puzzles.Add(imageId, config);
            }

            return _puzzles[imageId];
        }

        private PriceData GetRandomPrice(int detailLevel)
        {
            if (detailLevel == 0) return new PriceData(PriceType.Free);

            var type = (PriceType)Random.Range(0, 4);
            var amount = 0;
            switch (type)
            {
                case PriceType.Free:
                    break;
                case PriceType.SoftCurrency:
                    amount = detailLevel * 50;
                    break;
                case PriceType.HardCurrency:
                    amount = detailLevel;
                    break;
                case PriceType.WatchAd:
                    amount = 1;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return new PriceData(type, amount);
        }

        public bool TryStartPuzzle(string imageId, int selectedPiecesCountIndex)
        {
            var price = GetPuzzleConfig(imageId).DetailLevels[selectedPiecesCountIndex].Price;
            return ProcessPrice(price);
        }

        public bool ProcessPrice(PriceData price)
        {
            switch (price.Type)
            {
                case PriceType.Free:
                    return true;
                case PriceType.SoftCurrency:
                    //todo: check if have enough money,
                    break;
                case PriceType.HardCurrency:
                    //todo: check if have enough money,
                    break;
                case PriceType.WatchAd:
                    //todo: start wathcing viedeo ad (need to implement async price processing)
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return false;
        }
    }
}