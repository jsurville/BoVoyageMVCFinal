using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BoVoyageMVC.Validators
{

    [AttributeUsage(AttributeTargets.Property)]
    public class DecimalAtribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                if (value is String) return (value.ToString().Length) > 0;

                if (value is int n) return n >= 0;
                if (value is decimal d) return d > 0.0m;
            }
            else
            {
                
                throw new ArgumentException("Le valeur doit être positible ");
            }
            return false;
        }
       
    }

}
       
   
