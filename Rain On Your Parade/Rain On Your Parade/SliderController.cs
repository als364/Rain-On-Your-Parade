using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rain_On_Your_Parade
{
    class SliderController : Controller
    {
        Slider slider;

        public SliderController (Slider slider) : base(slider)
        {
            this.slider = slider;
        }
    }
}
