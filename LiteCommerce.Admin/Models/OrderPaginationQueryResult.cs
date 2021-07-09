using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LiteCommerce.DomainModels;

namespace LiteCommerce.Admin.Models
{
    public class OrderPaginationQueryResult:BasePaginationQueryResult
    {
        public List<Order> Data { get; set; }
    }
}