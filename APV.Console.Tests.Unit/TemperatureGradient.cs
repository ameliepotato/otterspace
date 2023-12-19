using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APV.Console.Tests.Unit
{
    public class TemperatureGradient
    {
       [Test]
        public void ColorCodeByTemperatureWorks()
        {
            Console.TemperatureGradient temperatureGradient = new Console.TemperatureGradient(
                0,
                100,
                Color.Blue,
                Color.Red
                );

            Assert.That(temperatureGradient.ColorCodeByTemperature(0), Is.EqualTo("#0000FF"));
            Assert.That(temperatureGradient.ColorCodeByTemperature(100), Is.EqualTo("#FF0000"));
            Assert.That(temperatureGradient.ColorCodeByTemperature(24), Is.EqualTo("#3D00C1"));
            Assert.That(temperatureGradient.ColorCodeByTemperature(76), Is.EqualTo("#C1003D"));
        }
    }
}
