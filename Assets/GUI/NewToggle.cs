using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
	[AddComponentMenu("UI/Toggle New", 31)]
    [RequireComponent(typeof(RectTransform))]
    public class NewToggle : Toggle {

        /// <summary>
        /// Предполагается, что true означает необходимость скрыть фон при установке флажка
        /// </summary>
        public bool hideBackground = false;
    }
	
}
