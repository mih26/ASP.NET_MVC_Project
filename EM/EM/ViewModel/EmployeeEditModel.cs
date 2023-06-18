using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EM.ViewModel
{
    public class EmployeeEditModel
        {
            public int EmployeeId { get; set; }
            [Required, StringLength(50)]
            public string EmployeeName { get; set; }
            [Required, DataType(DataType.Date)]
            public DateTime EmployeeDate { get; set; }
            [Required, StringLength(30)]
            public string EmployeeCategory { get; set; }
            [Required, DataType(DataType.Currency)]
            public decimal EmployeeRate { get; set; }
            public bool WorkFromHome { get; set; }

            public HttpPostedFileBase Picture { get; set; }
        }
}
