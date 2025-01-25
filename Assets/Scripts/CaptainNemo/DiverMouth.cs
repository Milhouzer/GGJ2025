using CaptainNemo.Controls;
using UnityEngine;

namespace CaptainNemo
{
    public class DiverMouth : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            IControlHandler handler = other.GetComponent<IControlHandler>();
            if (handler == null || handler.GetGlobalControlParam() != GlobalControlParam.Oxygen) return;
            
            handler.Handle();
            Debug.Log($"Handle {handler}");
        }
    }
}
