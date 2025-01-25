using CaptainNemo.Controls;
using UnityEngine;

namespace CaptainNemo
{
    public class Diver: MonoBehaviour
    {
        public void ParameterCallback(IControlHandler handler)
        {
            Debug.Log($"Parameter {handler.GetGlobalControlParam()} called with value {handler.GetControlValue()} on {this}");
        }
    }
}