using System;
using System.Collections.Generic;
using System.Data;
using Box9.Leds.Pi.DataAccess.Models;
using Box9.Leds.Pi.Domain.Dispatch;
using Box9.Leds.Pi.Domain.VideoFrames;
using Box9.Leds.Pi.Domain.VideoPlayback;
using FakeItEasy;
using Xunit;
using Box9.Leds.Pi.Domain.Logging;

namespace Box9.Leds.Pi.Domain.Tests
{
    public class VideoPlayerTests
    {
        [Fact]
        public void WhenILoadAVideo_FramesAreLoadedIntoMemory()
        {
            var playbackServiceFactory = A.Fake<IPlaybackServiceFactory>();
            var dispatcher = A.Fake<IDispatcher>();
            var log = A.Fake<ILog>();

            var videoPlayer = new VideoPlayer(playbackServiceFactory, dispatcher, log);
            var video = DummyVideo(A.Dummy<VideoMetadataModel>());

            videoPlayer.Load(video);

            A.CallTo(() => dispatcher.Dispatch(A<Func<IDbConnection, IEnumerable<VideoFrame>>>._))
                .MustHaveHappened(Repeated.Exactly.Once);
        }

        [Fact]
        public async void WhenILoadAVideo_ThenPlaybackWithTheCorrectPlaybackToken_VideoWillPlay()
        {
            var playbackServiceFactory = A.Fake<IPlaybackServiceFactory>();
            var dispatcher = A.Fake<IDispatcher>();
            var log = A.Fake<ILog>();

            var videoPlayer = new VideoPlayer(playbackServiceFactory, dispatcher, log);

            var videoMetadataModel = new VideoMetadataModel
            {
                FrameRate = 1000
            };
            var videoFrames = new List<VideoFrameModel>
            {
                new VideoFrameModel
                {
                    Id = 1,
                    BinaryData = new Byte[1],
                    Position = 1,
                    VideoId = 1
                }
            };

            var video = DummyVideo(videoMetadataModel, videoFrames);

            var playbackToken = videoPlayer.Load(video);
            await videoPlayer.Play(video);
        }

        [Fact]
        public void WhenILoadAVideo_ThenPlaybackWithTheIncorrectPlaybackToken_WillThrowArgumentException()
        {
            var playbackServiceFactory = A.Fake<IPlaybackServiceFactory>();
            var dispatcher = A.Fake<IDispatcher>();
            var log = A.Fake<ILog>();

            var videoPlayer = new VideoPlayer(playbackServiceFactory, dispatcher, log);
            var video = DummyVideo(new VideoMetadataModel
            {
                FrameRate = 1000
            });

            var playbackToken = videoPlayer.Load(video);
            Assert.ThrowsAsync<ArgumentException>(() => videoPlayer.Play(video));
        }

        private Video DummyVideo(VideoMetadataModel model, List<VideoFrameModel> videoFrames = null)
        {
            if (videoFrames == null)
            {
                videoFrames = new List<VideoFrameModel>();
            }

            return new Video(model, A.Fake<IDispatcher>());
        }
    }
}
