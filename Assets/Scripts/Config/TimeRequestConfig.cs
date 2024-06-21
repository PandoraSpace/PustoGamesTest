using UnityEngine;

namespace Config
{
    [CreateAssetMenu]
    public class TimeRequestConfig : ScriptableObject
    {
        public string Url;
        [Min(0)] public float Interval;
    }
}