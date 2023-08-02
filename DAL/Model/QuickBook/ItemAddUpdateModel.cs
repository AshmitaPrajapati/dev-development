using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model.QuickBook
{
    public class ItemAddUpdateModel
    {
        public ItemAddUpdateModel() 
        {
            AssetAccountRef = new AssetAccountRef()
            {
                name = "Inventory Asset",
                value = "81"
            };
            ExpenseAccountRef = new ExpenseAccountRef() 
            { 
                name = "Cost of Goods Sold",
                value = "80"
            };
            IncomeAccountRef = new IncomeAccountRef()
            { 
                name = "Sales of Product Income",
                value = "79"
            };
        }

        public string? Id { get; set; }
        public bool TrackQtyOnHand { get; set; }
        public string Name { get; set; }
        public int QtyOnHand { get; set; }
        public IncomeAccountRef? IncomeAccountRef { get; set; }
        public AssetAccountRef? AssetAccountRef { get; set; }
        public string InvStartDate { get; set; }
        public string Type { get; set; }
        public ExpenseAccountRef? ExpenseAccountRef { get; set; }
        public string Description { get; set; }
        public bool Taxable { get; set; }
        public int UnitPrice { get; set; }
        public bool Active { get; set; }
        public string? SyncToken { get; set; }
    }

    public class AssetAccountRef
    {
        public string name { get; set; }
        
        public string value { get; set; }
    }

    public class ExpenseAccountRef
    {
        public string name { get; set; }
        
        public string value { get; set; }
    }

    public class IncomeAccountRef
    {
        public string name { get; set; }

        public string value { get; set; }
    }
}
