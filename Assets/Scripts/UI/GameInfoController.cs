using System;
using System.Collections.Generic;
using CharacterModel;
using Zomby;

namespace UI
{
    public sealed class GameInfoController : IDisposable
    {
        private readonly List<UITextView> _textViews = new();
        private readonly Character _character;
        private readonly ZombieSpawnSystem _zombieSpawn;

        public GameInfoController(List<UITextView> textViews, Character character, ZombieSpawnSystem zombieSpawn)
        {
            _textViews.AddRange(textViews);
            _character = character;
            _zombieSpawn = zombieSpawn;
            _character.Health.Subscribe(HitPointsChanged);
            HitPointsChanged(_character.Health.Value);
            _character.AmoAmount.Subscribe(BulletAmountChanged);
            BulletAmountChanged(_character.AmoAmount.Value);
            _zombieSpawn.KillsCount.Subscribe(KillsChanged);
            KillsChanged(_zombieSpawn.KillsCount.Value);
        }

        public void Dispose()
        {
            _character.Health.UnSubscribe(HitPointsChanged);
            _character.AmoAmount.UnSubscribe(BulletAmountChanged);
            _zombieSpawn.KillsCount.UnSubscribe(KillsChanged);
        }

        private void HitPointsChanged(int value)
        {
            GetView(TextTypes.HitPoints).SetText($"HIP POINTS: {value}");
        }

        private void BulletAmountChanged(int value)
        {
            GetView(TextTypes.Bullet).SetText($"BULLETS: {value}/{_character.MaxAmoAmount.Value}");
        }

        private void KillsChanged(int value)
        {
            GetView(TextTypes.Kills).SetText($"KILLS: {value}");
        }

        private UITextView GetView(TextTypes type)
        {
            foreach (var view in _textViews)
            {
                if (view.type == type)
                {
                    return view;
                }
            }

            throw new Exception($"No view with type: {type}!!");
        }
    }
}
