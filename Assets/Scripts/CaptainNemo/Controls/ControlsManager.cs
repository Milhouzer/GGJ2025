using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace CaptainNemo.Controls
{
    public class ControlsManager : Singleton<ControlsManager>
    {
        public static IControlHandler ControlHandler { get; set; }

        private List<IControlHandler> _handlers = new List<IControlHandler>();
        
        public void Register(IControlHandler controlHandler)
        {
            if (_handlers.Contains(controlHandler))
            {
                Debug.LogWarning($"ControlHandler {controlHandler.ToString()} already registered");
                return;
            }
            
            _handlers.Add(controlHandler);
        }
        
        public void UnRegister(IControlHandler controlHandler)
        {
            _handlers.Remove(controlHandler);
        }
    }
}