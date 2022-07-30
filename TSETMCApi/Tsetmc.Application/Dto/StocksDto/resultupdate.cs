using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsetmc.Application.Dto.StocksDto
{
    public class resultupdate
    {
        public string recDate { get; set; }
        public string insCode { get; set; }
        public float buy_I_Volume { get; set; }
        public float buy_N_Volume { get; set; }
        public float buy_N_Value { get; set; }
        public float buy_I_Value { get; set; }
        public float buy_N_Count { get; set; }
        public float buy_I_Count { get; set; }
        public float sell_I_Volume { get; set; }
        public float sell_N_Volume { get; set; }
        public float sell_I_Value { get; set; }
        public float sell_N_Value { get; set; }
        public float sell_I_Count { get; set; }
        public float sell_N_Count { get; set; }
    }
}
