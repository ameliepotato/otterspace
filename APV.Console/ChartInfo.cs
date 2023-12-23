using Microsoft.VisualBasic;
using System;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace APV.Console
{
    public struct dataChart
    {
        public List<string> labels;
    }

    public struct dataSetChart
    {
        public string label;
        public List<int> data;
    };
    public class ChartInfo
    {
        public string type { get; set; }
        public dataChart data { get; set; }

        public dataSetChart datasets { get; set; }

        //{type:%27bar%27,
        //data:{labels:[2012,2013,2014,2015,%202016],
        //datasets:[
        //{label:%27Users%27,
        //data:[120,60,50,180,120]}
        //]}}
        public string ToUrl()
        {
            return $"{{type:'{type}',data:{{labels:['{string.Join("','", data.labels)}']," +
                $"datasets:[{{label:'{datasets.label}'," +
                $"data:[{string.Join(",", datasets.data)}]}}]}}}}"; 
        }
    }
}
