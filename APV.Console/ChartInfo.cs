using Microsoft.VisualBasic;
using System;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace APV.Console
{
    public class ChartData
    {
        public string label { get; set; }
        public int[] data { get; set; }
    }

    public class ChartInfo
    {
        public string[] labels { get; set; }
        public ChartData[] datasets { get; set; }

    }
}
