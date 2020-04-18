﻿using osu.Game.Configuration;
using osu.Game.Rulesets.Configuration;

namespace osu.Game.Rulesets.Gamebosu.Configuration
{
    public class GamebosuConfigManager : RulesetConfigManager<GamebosuSetting>
    {
        public GamebosuConfigManager(SettingsStore settings, RulesetInfo ruleset) 
            : base(settings, ruleset, 0)
        {
        }

        protected override void InitialiseDefaults()
        {
            Set(GamebosuSetting.ClockRate, 1, 0.1, 5, 0.1);
            Set(GamebosuSetting.PreferGBCMode, true);
            base.InitialiseDefaults();
        }
    }

    public enum GamebosuSetting
    {
        ClockRate,
        PreferGBCMode
    }
}