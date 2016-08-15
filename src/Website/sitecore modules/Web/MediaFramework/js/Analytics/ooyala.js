;
var ooyalaListener = new PlayerEventsListener();

ooyalaListener.getMediaId = function (player) {
    return player.embedCode;
};

ooyalaListener.getPosition = function (player) {
    return player.getPlayheadTime();
};

ooyalaListener.getDuration = function (player) {
    return Math.round(player.getDuration() || player.item.time);
};

ooyalaListener.getContainer = function (player) {
    return this.jQuerySMF('#' + player.elementId).closest('.mf-player-container');
},

ooyalaListener.getAdditionalParameters = function (player) {
    return {
        mediaId: player.getCurrentItemEmbedCode(),
        mediaName: player.getTitle(),
        mediaLength: this.getDuration(player)
    };
};

ooyalaListener.onPlay = function (player) {
    //workaround for Flash PLAY event
    if (!player.mf_isPlaying) {
        this.onMediaEvent(player, this.eventTypes.PlaybackStarted);
        player.mf_isPlaying = true;
    }
};

ooyalaListener.onPlayed = function (player) {
    this.onMediaEvent(player, this.eventTypes.PlaybackCompleted);
    player.mf_isPlaying = false;
};

ooyalaListener.onPlayheadTimeChanged = function (player) {
    this.onMediaEvent(player, this.eventTypes.PlaybackChanged);
};

ooyalaListener.subscribeEvents = function (player) {
    var ooyala = window[player.elementId];
    if (ooyala) {
        player.subscribe(ooyala.EVENTS.PLAYING, 'Playback started', function () { ooyalaListener.onPlay(player); });
        player.subscribe(ooyala.EVENTS.PLAYED, 'Playback completed', function () { ooyalaListener.onPlayed(player); });
        player.subscribe(ooyala.EVENTS.PLAYHEAD_TIME_CHANGED, 'Playback changed', function () { ooyalaListener.onPlayheadTimeChanged(player); });

        player.subscribe(ooyala.EVENTS.EMBED_CODE_CHANGED, 'Playback media is changed', function () { ooyalaListener.onMediaChanged(player); });
    }
};