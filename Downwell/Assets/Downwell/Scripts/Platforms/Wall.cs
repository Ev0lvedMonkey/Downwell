using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Downwell.Scripts.Platforms
{
    internal class Wall : Platform
    {
        protected override void SetLayer()
        {
            gameObject.layer = LayerMask.NameToLayer(StringConstants.GroundayerName);

        }
    }
}
