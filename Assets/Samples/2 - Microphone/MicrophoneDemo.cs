using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Whisper.Utils;
using Button = UnityEngine.UI.Button;

namespace Whisper.Samples
{
    /// <summary>
    /// Record audio clip from microphone and make a transcription.
    /// </summary>
    public class MicrophoneDemo : MonoBehaviour
    {
        public WhisperManager whisper;
        public MicrophoneRecord microphoneRecord;
        public bool streamSegments = true;
        public bool printLanguage = true;

        [Header("UI")]
        [SerializeField] private Image _indicator;
        [SerializeField] private Sprite _indicatorOff;
        [SerializeField] private Sprite _indicatorOn;

        public event Action<string> OnResultReceived;

        private string _buffer;

        private void Awake()
        {
            whisper.OnNewSegment += OnNewSegment;
            whisper.OnProgress += OnProgressHandler;
            
            microphoneRecord.OnRecordStop += OnRecordStop;
            
        }

        public void ChangeRecordState()
        {
            if (!microphoneRecord.IsRecording)
            {
                microphoneRecord.StartRecord();
                _indicator.sprite = _indicatorOn;
            }
            else
            {
                microphoneRecord.StopRecord();
                _indicator.sprite = _indicatorOff;
            }
        }
        
        private async void OnRecordStop(AudioChunk recordedAudio)
        {
            _buffer = "";

            var sw = new Stopwatch();
            sw.Start();
            
            var res = await whisper.GetTextAsync(recordedAudio.Data, recordedAudio.Frequency, recordedAudio.Channels);
            if (res == null) 
                return;

            var time = sw.ElapsedMilliseconds;
            var rate = recordedAudio.Length / (time * 0.001f);         

            var text = res.Result;
            if (printLanguage)
                text += $"\n\nLanguage: {res.Language}";

            OnResultReceived?.Invoke(res.Result);
        }

        private void OnProgressHandler(int progress)
        {
        }
        
        private void OnNewSegment(WhisperSegment segment)
        {
            if (!streamSegments)
                return;

            _buffer += segment.Text;
        }
    }
}