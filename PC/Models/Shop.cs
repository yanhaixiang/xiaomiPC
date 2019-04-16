﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC.Models
{
    public class Shop
    {
        public int ShopId { get; set; }
        public string ShopType { get; set; }
        public string ShopName { get; set; }
        public string ShopTitle { get; set; }
        public string ShopPicture { get; set; }
        public string ShopDatails { get; set; }
        public float ShopPrice { get; set; }
        public int ShopSum { get; set; }
        public int ShopState { get; set; }
    }
}
