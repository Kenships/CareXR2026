using _Project.Scripts.Core.AudioPooling;
using _Project.Scripts.Util.Scene;
using Sisus.Init;
using UnityEngine;
using AudioType = _Project.Scripts.Core.AudioPooling.Interface.AudioType;

namespace _Project.Scripts
{
    public class MusicPlayer : MonoBehaviour<AudioPooler>
    {
        [SerializeField] private SceneReference sceneRef;
        [SerializeField] private AudioClip bgMusic;
        private AudioPooler _audioPooler;
    

        private void Start()
        {
            _audioPooler.New2DAudio(bgMusic)
                .OnChannel(AudioType.Music)
                .LoopAudio()
                .AddToScene(sceneRef.BuildIndex)
                .Play(); 
        }

        protected override void Init(AudioPooler argument)
        {
            _audioPooler = argument; 
        }
    }
}
