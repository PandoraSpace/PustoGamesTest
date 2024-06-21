using System;
using System.Collections;
using Config;
using Infrastructure;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Networking;

namespace Service
{
    public class CheckTime
    {
        private readonly Coroutines _coroutines;
        private readonly string _url;
        private readonly WaitForSeconds _wait;

        public event Action<DateTimeOffset> OnRequestDate;
        
        public CheckTime(Coroutines coroutines, TimeRequestConfig config)
        {
            _coroutines = coroutines;
            _url = config.Url;
            
            _wait = new WaitForSeconds(config.Interval);
        }

        public void Start()
        {
            _coroutines.StartCoroutine(GetTime());
            _coroutines.StartCoroutine(RequestTimer());
        }

        private IEnumerator GetTime()
        {
            var request = new UnityWebRequest(_url, "GET");
            request.downloadHandler = new DownloadHandlerBuffer();
            
            yield return request.SendWebRequest();

            DateTimeOffset dateTime;
            if (request.result == UnityWebRequest.Result.Success)
            {
                var parseResult = JObject.Parse(request.downloadHandler.text);
                var unixTime = parseResult["time"].Value<long>();
                dateTime = DateTimeOffset.FromUnixTimeMilliseconds(unixTime);
            }
            else
            {
                dateTime = DateTimeOffset.Now;
            }

            OnRequestDate?.Invoke(dateTime);
        }

        private IEnumerator RequestTimer()
        {
            yield return _wait;
            _coroutines.StartCoroutine(GetTime());
        }
    }
}