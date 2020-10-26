using System;
using System.IO;
using UnityEngine;

namespace LilaTest
{
    internal class ApplicationConfig : ScriptableObject
    {
        [Header("Grid Controls")]
        [SerializeField] private GridCoordinate _gridDimensions = new GridCoordinate(default, default);

        [Header("Randomization")]
        [SerializeField] private bool _generateRandomSeed = true;
        [SerializeField] private int _deterministicSeed;
        

        [Header("Interaction Controls")] 
        [SerializeField] private int _areaOfInterest = 1;

        [UnityEditor.MenuItem("Assets/Create/Application Config")]
        public static void CreateConfig()
        {
            var config = CreateInstance<ApplicationConfig>();
            UnityEditor.AssetDatabase.CreateAsset(config, Path.Combine(ApplicationConstants.DataPaths.ResourcesPath, ApplicationConstants.DataPaths.ConfigPath, $"{nameof(ApplicationConfig)}.asset"));
            UnityEditor.AssetDatabase.SaveAssets();
            UnityEditor.AssetDatabase.Refresh();
        }
        
        //Grid
        public GridCoordinate GridDimensions => _gridDimensions;
        
        //Randomization
        public int Seed => _generateRandomSeed ? GetRandomSeed() : _deterministicSeed;
        
        //Interaction
        public int AreaOfInterest => _areaOfInterest;
        
        private int GetRandomSeed() => (int) (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
    }
}