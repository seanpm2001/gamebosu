﻿using Emux.GameBoy;
using Emux.GameBoy.Cartridge;
using Emux.GameBoy.Input;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Bindings;
using osu.Game.Rulesets.Gamebosu.Audio;

namespace osu.Game.Rulesets.Gamebosu.UI.Gameboy
{
    public class DrawableGameboy : CompositeDrawable, IKeyBindingHandler<GamebosuAction>
    {
        private readonly ICartridge cartridge;

        private readonly DrawableGameboyClock clock;

        private readonly DrawableGameboyScreen screen;

        private GameBoy gameBoy;

        public DrawableGameboy(ICartridge cart)
        {
            cartridge = cart;

            InternalChildren = new Drawable[]
            {
                clock = new DrawableGameboyClock(),
                screen = new DrawableGameboyScreen
                {
                    Size = new osuTK.Vector2(160, 144),
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre
                }
            };

            Masking = true;
            CornerRadius = 5;
        }

        public bool OnPressed(GamebosuAction action)
        {
            if (gameBoy == null) return false;

            gameBoy.KeyPad.PressedButtons |= getFromAction(action);

            return true;
        }

        public void OnReleased(GamebosuAction action)
        {
            if (gameBoy == null) return;

            gameBoy.KeyPad.PressedButtons &= ~getFromAction(action);
        }

        public void Start()
        {
            if (!gameBoy.Cpu.Running)
                gameBoy.Cpu.Run();
        }

        private GameBoyPadButton getFromAction(GamebosuAction action) => action switch
        {
            GamebosuAction.ButtonA => GameBoyPadButton.A,
            GamebosuAction.ButtonB => GameBoyPadButton.B,
            GamebosuAction.DPadUp => GameBoyPadButton.Up,
            GamebosuAction.DPadDown => GameBoyPadButton.Down,
            GamebosuAction.DPadRight => GameBoyPadButton.Right,
            GamebosuAction.DPadLeft => GameBoyPadButton.Left,
            GamebosuAction.ButtonStart => GameBoyPadButton.Start,
            GamebosuAction.ButtonSelect => GameBoyPadButton.Select,
            _ => 0
        };

        [BackgroundDependencyLoader]
        private void load()
        {
            gameBoy = new GameBoy(cartridge, clock, true);
            gameBoy.Gpu.VideoOutput = screen;

            foreach (var channel in gameBoy.Spu.Channels)
                channel.ChannelOutput = new DummyAudioChannelOutput();
        }
    }
}