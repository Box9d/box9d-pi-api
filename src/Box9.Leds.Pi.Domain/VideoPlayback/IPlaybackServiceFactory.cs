﻿using System;

namespace Box9.Leds.Pi.Domain.VideoPlayback
{
    public interface IPlaybackServiceFactory
    {
        Func<IPlaybackService> Playback { get; }
    }
}