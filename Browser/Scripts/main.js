let track_name = document.querySelector(".track-name");
let playpause_btn = document.querySelector(".playpause-track");
let seek_slider = document.querySelector(".seek_slider");
let volume_slider = document.querySelector(".volume_slider");
let curr_time = document.querySelector(".current-time");
let total_duration = document.querySelector(".total-duration");
let speed_input = document.querySelector(".speed-input");
let isPlaying = false;
let updateTimer;

let curr_track = document.createElement('audio');

const track = {
    //name: "Put file into Files/ and change name to file.mp3: Files/file.mp3",
    path: "../Files/file.wav"
};

function loadTrack() {
    curr_track.src = track["path"];
    curr_track.load();
    
    //track_name.textContent = track["name"];

    updateTimer = setInterval(seekUpdate, 1000);
}

// PLAY/PAUSE BUTTON
function playpauseTrack() {
    if (!isPlaying) playTrack();
    else pauseTrack();
}

function playTrack() {
    curr_track.play();
    isPlaying = true;
    playpause_btn.innerHTML = '<i class="fa fa-pause-circle fa-5x"></i>';
}

function pauseTrack() {
    curr_track.pause();
    isPlaying = false;
    playpause_btn.innerHTML = '<i class="fa fa-play-circle fa-5x"></i>';
}

// SEEK BAR
function seekTo() {
    seekto = curr_track.duration * (seek_slider.value / 100);
    curr_track.currentTime = seekto;
}

// VOLUME BAR
function setVolume() {
    curr_track.volume = volume_slider.value / 100;
}

// UPDATE SEEK BAR POSITION AND TIMES
function seekUpdate() {
    let seekPosition = 0;

    // Check if the current track duration is a legible number
    if (!isNaN(curr_track.duration)) {
        seekPosition = curr_track.currentTime * (100 / curr_track.duration);
        seek_slider.value = seekPosition;

        // Calculate the time left and the total duration
        let currentMinutes = Math.floor(curr_track.currentTime / 60);
        let currentSeconds = Math.floor(curr_track.currentTime - currentMinutes * 60);
        let durationMinutes = Math.floor(curr_track.duration / 60);
        let durationSeconds = Math.floor(curr_track.duration - durationMinutes * 60);

        // Add a zero to the single digit time values
        if (currentSeconds < 10) { currentSeconds = "0" + currentSeconds; }
        if (durationSeconds < 10) { durationSeconds = "0" + durationSeconds; }
        if (currentMinutes < 10) { currentMinutes = "0" + currentMinutes; }
        if (durationMinutes < 10) { durationMinutes = "0" + durationMinutes; }

        // Display the updated duration
        curr_time.textContent = currentMinutes + ":" + currentSeconds;
        total_duration.textContent = durationMinutes + ":" + durationSeconds;
    }
}

function changeSpeed() {
    curr_track.playbackRate = speed_input.value;
}

document.addEventListener("keydown", e => {
    if(e.key === "F10") {
      // Suppress default behaviour 
      // e.g. F1 in Microsoft Edge on Windows usually opens Windows help
      e.preventDefault();
    }
})

document.addEventListener("keyup", e => {
    if(e.key === "F10") {
      // Handle the keyup event
      playTrack();
    }
})