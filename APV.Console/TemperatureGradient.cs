using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing.Constraints;
using System.Drawing;

namespace APV.Console
{
    public class TemperatureGradient
    {
        float _min;
        float _max;
        
        Color _colorStart;
        Color _colorEnd;
        
        public TemperatureGradient(float min, float max, Color start, Color end) {
            _max = max;
            _min = min;
            _colorStart = start;
            _colorEnd = end;
        }
        public string ColorCodeByTemperature(int value)
        {
            //var alpha = 122;

            if (value > _max)
            {
                value = (int)_max;
            }
            else if (value < _min)
            {
                value = (int)_min;
            }

            float coef = Math.Abs(value - _min) / Math.Abs(_max - _min);

            var red = (int)Lerp(_colorStart.R, _colorEnd.R, coef);
            var green = (int)Lerp(_colorStart.G, _colorEnd.G, coef);
            var blue = (int)Lerp(_colorStart.B, _colorEnd.B, coef);

            Color tempColor = Color.FromArgb(red, green, blue);

            return ColorTranslator.ToHtml(tempColor);
        }

        protected static float Lerp(float firstFloat, float secondFloat, float by)
        {
            return firstFloat * (1 - by) + secondFloat * by;
        }
    }
}
