﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiteLagoon.Domain.Entities
{
    public class VillaNumber
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name ="Villa Number")]
        public int Villa_Number { get; set; }
        [ForeignKey("Villa")]
        public int VillaId { get; set; }
        //ValidateNever it is used for not mapping in database and UI part validation not use
        [ValidateNever]
        public Villa Villa { get; set; }
        public string? SpecialDetails { get; set; }
    }
}
