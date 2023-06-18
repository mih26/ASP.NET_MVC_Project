using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EM.Models.Annotations
{
    [MetadataType(typeof(EmployeeMetadata))]
    public partial class Employee { }
    public partial class EmployeeMetadata
    {
        public int EmployeeId { get; set; }
        [Required, StringLength(50)]
        public string EmployeeName { get; set; }
        [Required, DataType(DataType.Date)]
        public System.DateTime EmployeeDate { get; set; }
        [Required, StringLength(30)]
        public string EmployeeCategory { get; set; }
        [Required, DataType(DataType.Currency)]
        public decimal EmployeeRate { get; set; }
        public Nullable<bool> WorkFromHome { get; set; }
        [Required, StringLength(30)]
        public string Picture { get; set; }
    }
}